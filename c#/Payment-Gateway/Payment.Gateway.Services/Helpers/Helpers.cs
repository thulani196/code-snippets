namespace Payment.Gateway.Logic.Helpers
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Payment.Gateway.Data.Constants;
    using Payment.Gateway.Data.Models.MtnModels;
    using Payment.Gateway.Logic.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Runtime.Caching;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Xml;

    /// <summary>
    /// Defines the <see cref="Helpers" />
    /// </summary>
    public class Helpers : IHelpers
    {
        /// <summary>
        /// Defines the _requestFields
        /// </summary>
        private readonly SortedList<string, string> _requestFields = new SortedList<string, string>(new StringComparer());

        /// <summary>
        /// Defines the _responseFields
        /// </summary>
        private readonly SortedList<string, string> _responseFields = new SortedList<string, string>(new StringComparer());

        /// <summary>
        /// Defines the _config
        /// </summary>
        private readonly IConfiguration _config;

        /// <summary>
        /// Defines the _clientFactory
        /// </summary>
        private readonly IHttpClientFactory _clientFactory;

        /// <summary>
        /// Defines the _log
        /// </summary>
        private readonly ILogger<Helpers> _log;

        /// <summary>
        /// Initializes a new instance of the <see cref="Helpers"/> class.
        /// </summary>
        /// <param name="configuration">The configuration<see cref="IConfiguration"/></param>
        /// <param name="clientFactory">The clientFactory<see cref="IHttpClientFactory"/></param>
        /// <param name="logger">The logger<see cref="ILogger{Helpers}"/></param>
        public Helpers(IConfiguration configuration, IHttpClientFactory clientFactory, ILogger<Helpers> logger)
        {
            _config = configuration;
            _clientFactory = clientFactory;
            _log = logger;
        }

        /// <summary>
        /// The BuildObject
        /// </summary>
        /// <param name="resultString">The resultString<see cref="string"/></param>
        /// <returns>The <see cref="Dictionary{string, dynamic}"/></returns>
        public Dictionary<string, dynamic> BuildObject(string resultString)
        {
            var responses = resultString.Split('&');
            var dataDictionary = new Dictionary<string, dynamic>();

            foreach (string responseField in responses)
            {
                var field = responseField.Split('=');
                dataDictionary.Add(field[0], HttpUtility.UrlDecode(field[1]));
            }

            return dataDictionary;
        }

        /// <summary>
        /// TODO:
        /// </summary>
        /// <returns></returns>
        public string GetRequestRaw()
        {
            var data = new StringBuilder();

            foreach (KeyValuePair<string, string> kvp in _requestFields)
            {
                if (!string.IsNullOrEmpty(kvp.Value))
                {
                    data.Append($"{kvp.Key}={HttpUtility.UrlEncode(kvp.Value, Encoding.GetEncoding("ISO-8859-1"))}&");
                }
            }

            data.Remove(data.Length - 1, 1); //remove trailing '&' from string
            return data.ToString();
        }

        /// <summary>
        /// creates payment submission form
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetHiddenRequestFormAsync()
        {
            //todo: refactor XML BUilder 
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(string.Format(Constants.StaticFormFields.FORM, _config[Constants.AppSettings.VpcPaymentServerUrl]));

            XmlNode root = doc.DocumentElement;
            foreach (KeyValuePair<string, string> kvp in _requestFields)
            {
                XmlElement elem = doc.CreateElement(Constants.StaticFormFields.INPUT);
                elem.SetAttribute(Constants.StaticFormFields.TYPE, Constants.StaticFormFields.TYPE_VALUE);
                elem.SetAttribute(Constants.StaticFormFields.NAME, kvp.Key);
                elem.SetAttribute(Constants.StaticFormFields.VALUE, kvp.Value);
                root.AppendChild(elem);
            }

            await AddSecureHashToXmlDocAsync(doc, root);

            return doc.OuterXml;
        }

        /// <summary>
        /// The AddSecureHashToXmlDocAsync
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public async Task AddSecureHashToXmlDocAsync(XmlDocument doc, XmlNode root)
        {
            // _logger.LogDebug("Add secureHash function triggered");

            XmlElement hash = doc.CreateElement(Constants.StaticFormFields.INPUT);
            hash.SetAttribute(Constants.StaticFormFields.TYPE, Constants.StaticFormFields.TYPE_VALUE);
            hash.SetAttribute(Constants.StaticFormFields.NAME, Constants.VpcRequestFields.vpc_SecureHash);
            hash.SetAttribute(Constants.StaticFormFields.VALUE, await CreateSha256SignatureAsync(true));
            root.AppendChild(hash);

            XmlElement hashType = doc.CreateElement(Constants.StaticFormFields.INPUT);
            hashType.SetAttribute(Constants.StaticFormFields.TYPE, Constants.StaticFormFields.TYPE_VALUE);
            hashType.SetAttribute(Constants.StaticFormFields.NAME, Constants.VpcRequestFields.vpc_SecureHashType);
            hashType.SetAttribute(Constants.StaticFormFields.VALUE, Constants.VpcRequestFields.vpc_SecureHashType_Value);
            root.AppendChild(hashType);
        }

        /// <summary>
        /// The CreateSha256SignatureAsync
        /// </summary>
        /// <param name="useRequest"></param>
        /// <returns></returns>
        public async Task<string> CreateSha256SignatureAsync(bool useRequest)
        {
            // Hex Decode the Secure Secret for use in using the HMAC SHA256 hasher
            // hex decoding eliminates this source of error as it is independent of the character encoding
            // hex decoding is precise in converting to a byte array and is the preferred form for representing binary values as hex strings. 
            var hexHash = ""; 

            await Task.Factory.StartNew(() =>
            {
                var convertedHash = new byte[_config[Constants.AppSettings.VpcSecureSecret].Length / 2];
                for (int i = 0; i < _config[Constants.AppSettings.VpcSecureSecret].Length / 2; i++)
                {
                    convertedHash[i] = (byte)Int32.Parse(_config[Constants.AppSettings.VpcSecureSecret].Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                }

                // Build string from collection in preparation to be hashed
                var sb = new StringBuilder();
                var list = (useRequest ? _requestFields : _responseFields);
                foreach (KeyValuePair<string, string> kvp in list)
                {
                    if (!string.IsNullOrEmpty(kvp.Value))
                    {
                        sb.Append($"{kvp.Key}={kvp.Value}&");
                    }
                } 
                sb.Remove(sb.Length - 1, 1); 
                // Create secureHash on string 
                using HMACSHA256 hasher = new HMACSHA256(convertedHash);
                var utf8Bytes = Encoding.UTF8.GetBytes(sb.ToString());
                var iso8859Bytes = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(Constants.VpcRequestValues.vpc_EncodingFormat), utf8Bytes);
                var hashValue = hasher.ComputeHash(iso8859Bytes);

                foreach (byte b in hashValue)
                {
                    hexHash += b.ToString("X2");
                }
            }); 
            return hexHash;
        }

        /// <summary>
        /// TODO:
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddDigitalOrderField(string key, string value)
        {
            _requestFields.Add(key, value);
        }

        /// <summary>
        /// Gets access token from MTN Momo API
        /// </summary>
        /// <typeparam name="TokenObject"></typeparam>
        /// <returns>Access Token</returns>
        public async Task<TokenObject> GetAccessTokenAsync<TokenObject>() where TokenObject : class
        {
            var _client = _clientFactory.CreateClient(Constants.MtnRequestFields.MtnClientName);

            _client.DefaultRequestHeaders.Add(Constants.MtnRequestFields.authorizationKey, String.Format(Constants.MtnRequestFields.BasicAuthKeyword, _config[Constants.AppSettings.MtnAuthString]));
            HttpContent content = new StringContent(string.Empty, Encoding.UTF8, Constants.MtnRequestFields.ContentType);

            try
            { 
                HttpResponseMessage response = await _client.PostAsync(Constants.MtnRequestFields.TokenEndPoint, content);
                var responseBody = await response.Content.ReadAsStringAsync();

                TokenObject token = JsonConvert.DeserializeObject<TokenObject>(responseBody) as TokenObject; 
                return token;
            }
            catch (Exception e)
            {
                _log.LogError(e, e.Message);
                throw;
            }
        }

        /// <summary>
        /// Generates and registers a transaction reference number with the MTN API
        /// </summary>
        /// <returns>Transaction reference ID or Error Response body</returns>
        public async Task<string> CreateReferenceIDAsync(string guid)
        { 
            var _client = _clientFactory.CreateClient(Constants.MtnRequestFields.MtnClientName);

            var payload = "{\"providerCallbackHost\": \"\"}";
            HttpContent content = new StringContent(payload, Encoding.UTF8, Constants.MtnRequestFields.ContentType);

            // Register new UUID
            _client.DefaultRequestHeaders.Add(Constants.MtnRequestFields.referenceId, guid);

            try
            {
                HttpResponseMessage response = await _client.PostAsync(Constants.MtnRequestFields.UserEndPoint, content);
                if (response.StatusCode.ToString().Equals("Created"))
                {
                    return guid;
                }
                else
                {
                    _log.LogError($"Could not Register reference ID: {response.StatusCode.ToString()} - {response.ReasonPhrase}");
                    _log.LogError($"ID Response: {await response.Content.ReadAsStringAsync()}");
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception e)
            {
                _log.LogError(e, e.Message);
                throw e;
            }
        }

    }
}
