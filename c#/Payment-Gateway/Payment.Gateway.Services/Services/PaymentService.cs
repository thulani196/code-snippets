namespace Payment.Gateway.Logic.Services
{
    using Data.Constants;
    using Data.Models;
    using Interfaces;
    using Logic.Helpers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// The PaymentService brokers calls to the Mastercard API for submitting various requests.
    /// </summary>
    public class PaymentService : IPaymentService
    {
        /// <summary>
        /// Defines the _clientFactory
        /// </summary>
        private readonly IHttpClientFactory _clientFactory;

        /// <summary>
        /// Defines the _logger
        /// </summary>
        private readonly ILogger<PaymentService> _logger;

        /// <summary>
        /// Defines the _config
        /// </summary>
        private readonly IConfiguration _config;

        /// <summary>
        /// Defines the _helpers
        /// </summary>
        private readonly IHelpers _helpers;

        /// <summary>
        /// Defines the accessCode
        /// </summary>
        private readonly string accessCode;

        /// <summary>
        /// Defines the operatorUser
        /// </summary>
        private readonly string operatorUser;

        /// <summary>
        /// Defines the operatorPassword
        /// </summary>
        private readonly string operatorPassword;
        private readonly HttpClient _client;

        /// <summary>
        /// Defines the merchant
        /// </summary>
        private readonly string merchant;

        /// <summary>
        /// Defines the vpcPaymentReturnUrl
        /// </summary>
        private readonly string vpcPaymentReturnUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentService"/> class.
        /// </summary>
        /// <param name="logger">The logger<see cref="ILogger{PaymentService}"/></param>
        /// <param name="configuration">The configuration<see cref="IConfiguration"/></param>
        /// <param name="clientFactory">The clientFactory<see cref="IHttpClientFactory"/></param>
        /// <param name="helpers">The helpers<see cref="Helpers"/></param>
        public PaymentService(ILogger<PaymentService> logger, IConfiguration configuration, IHttpClientFactory clientFactory, IHelpers helpers)
        {
            _logger = logger;
            _config = configuration;
            _clientFactory = clientFactory;
            _helpers = helpers;

            vpcPaymentReturnUrl = _config[Constants.AppSettings.VpcPaymentReturnUrl];
            merchant = _config[Constants.AppSettings.VpcMerchant];
            accessCode = _config[Constants.AppSettings.VpcAccessCode];
            operatorUser = _config[Constants.AppSettings.VpcOperatorName];
            operatorPassword = _config[Constants.AppSettings.VpcOperatorPassword];
        _client = _clientFactory.CreateClient(Constants.AppSettings.ChargeBackClient);
        }

        /// <summary>
        /// Generates a for with Hashed payment details
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task<string> MakePaymentAsync(TransactionModel transaction)
        {

            try
            {
                //// Add Variables to be sent to a sorted list
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_Version, Constants.VpcRequestFields.vpc_version);
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_Command, Constants.VpcRequestFields.vpc_command);
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_Merchant, merchant);
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_AccessCode, accessCode);
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_MerchTxnRef, transaction.VpcMerchTxnRef);
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_OrderInfo, transaction.VpcOrderInfo);
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_Amount, transaction.VpcAmount);
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_Currency, transaction.VpcCurrency);
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_Card, transaction.VpcCard);
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_CardNum, transaction.VpcCardNum);
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_CardExp, transaction.VpcCardExp);
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_Gateway, Constants.VpcRequestFields.vpc_gateway);
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_ReturnURL, vpcPaymentReturnUrl);
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_CardSecurityCode, transaction.VpcCardSecurityCode);

                var formData = await _helpers.GetHiddenRequestFormAsync();
                return formData;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                _logger.LogError(e.StackTrace);
                _logger.LogError(e.InnerException.Message);
                return null;
            }
        }

        /// <summary>
        /// Handles refunds of payments
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task<ResponseModel> RefundPaymentAsync(RefundModel transaction)
        {  
            try
            {
                //// Add Variables to be sent to a sorted list
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_Version, Constants.VpcRequestFields.vpc_version);
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_Command, Constants.VpcRefundFields.vpc_command);
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_Merchant, merchant);
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_AccessCode, accessCode);
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_MerchTxnRef, transaction.VpcMerchTxnRef);
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_Amount, transaction.VpcAmount);
                _helpers.AddDigitalOrderField(Constants.VpcRefundFields.vpc_User, operatorUser);
                _helpers.AddDigitalOrderField(Constants.VpcRefundFields.vpc_Password, operatorPassword);
                _helpers.AddDigitalOrderField(Constants.VpcRefundFields.vpc_TransNo, transaction.VpcTransNo);

                var request = new HttpRequestMessage(HttpMethod.Post, $"?{_helpers.GetRequestRaw()}");

                // User URL to send a POST request for a refund
                HttpResponseMessage response = await _client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var results = await response.Content.ReadAsStringAsync();
                    // Serialize response dictionary to String
                    var jsonString = JsonConvert.SerializeObject(_helpers.BuildObject(results), Newtonsoft.Json.Formatting.Indented);
                    return JsonConvert.DeserializeObject<ResponseModel>(jsonString);
                }
                var exception = new Exception($"Payment gateway responded with the following error: {response.StatusCode} | {response.ReasonPhrase}");
                _logger.LogError(exception, "Error while processing refund");
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                _logger.LogError(e.StackTrace);
                _logger.LogError(e.InnerException.Message);
                throw;
            }
        }

        /// <summary>
        /// This function handles the Void Capture Request and takes the VoidTransactionModel class as an argument
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task<ResponseModel> VoidPaymentAsync(VoidTransactionModel transaction)
        { 
            try
            {
                //// Add Variables to be sent to a sorted list
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_Version, Constants.VpcRequestFields.vpc_version);
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_Command, Constants.VpcVoidFields.vpc_Command);
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_Merchant, merchant);
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_AccessCode, accessCode);
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_MerchTxnRef, transaction.VpcMerchTxnRef);
                _helpers.AddDigitalOrderField(Constants.VpcRefundFields.vpc_User, operatorUser);
                _helpers.AddDigitalOrderField(Constants.VpcRefundFields.vpc_Password, operatorPassword);
                _helpers.AddDigitalOrderField(Constants.VpcRefundFields.vpc_TransNo, transaction.VpcTransNo);

                var request = new HttpRequestMessage(HttpMethod.Post, $"?{_helpers.GetRequestRaw()}");

                // User URL to send a POST request for a refund
                HttpResponseMessage response = await _client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var results =await response.Content.ReadAsStringAsync();

                    // Serialize response dictionary to String
                    var jsonString = JsonConvert.SerializeObject(_helpers.BuildObject(results), Newtonsoft.Json.Formatting.Indented);
                    return JsonConvert.DeserializeObject<ResponseModel>(jsonString);
                }
                var exception = new Exception($"Payment gateway responded with the following error: {response.StatusCode} | {response.ReasonPhrase}");
                _logger.LogError(exception, "Error while processing void");

                return null;
               
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                _logger.LogError(e.StackTrace);
                _logger.LogError(e.InnerException.Message);
                return null;
            }
        }

        /// <summary>
        /// Queries ECOBANK/MASTERCARD API For a specific transaction
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task<ResponseModel> QueryTransaction(QueryDRModel transaction)
        {
            try
            {
                //// Add Variables to be sent to a sorted list
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_Version, Constants.VpcRequestFields.vpc_version);
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_Command, Constants.VpcQueryDRFields.vpc_Command);
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_Merchant, merchant);
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_AccessCode, accessCode);
                _helpers.AddDigitalOrderField(Constants.VpcRequestFields.vpc_MerchTxnRef, transaction.VpcMerchTxnRef);
                _helpers.AddDigitalOrderField(Constants.VpcRefundFields.vpc_User, operatorUser);
                _helpers.AddDigitalOrderField(Constants.VpcRefundFields.vpc_Password, operatorPassword);

                var request = new HttpRequestMessage(HttpMethod.Post, $"?{_helpers.GetRequestRaw()}");
                HttpResponseMessage response = await _client.SendAsync(request);
                var results = await response.Content.ReadAsStringAsync();

                // Serialize response dictionary to String
                var jsonString = JsonConvert.SerializeObject(_helpers.BuildObject(results), Newtonsoft.Json.Formatting.Indented);
                return JsonConvert.DeserializeObject<ResponseModel>(jsonString);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                _logger.LogError(e.StackTrace);
                _logger.LogError(e.InnerException.Message);
                throw;
            }
        }
    }
}
