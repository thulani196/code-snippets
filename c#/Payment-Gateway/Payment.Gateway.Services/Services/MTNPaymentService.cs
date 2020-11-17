namespace Payment.Gateway.Logic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Payment.Gateway.Data.Models.MtnModels;
    using Payment.Gateway.Logic.Interfaces;
    using Payment.Gateway.Logic.Helpers;
    using Payment.Gateway.Data.Constants;
    using Newtonsoft.Json;
    using Microsoft.Extensions.Configuration;

    public class MtnPaymentService : IMtnService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<MtnPaymentService> _logger;
        private readonly IHelpers _helpers;
        private readonly ICacheManager _cacheManager;

        public MtnPaymentService(
            IHttpClientFactory clientFactory, 
            IHelpers helpers, 
            ILogger<MtnPaymentService> logger, ICacheManager cacheManager)
        {
            _clientFactory = clientFactory;
            _helpers = helpers;
            _logger = logger;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Implementation of RetrievePaymentDetailsAsync to retrieve a transaction from the MTN API.
        /// </summary>
        /// <param name="uuid">Transaction reference number</param>
        /// <returns>JSON Payload of transaction status (Successful or Failed) with all the other details.</returns>
        public async Task<string> RetrievePaymentDetailsAsync(string uuid)
        {
            var _client = _clientFactory.CreateClient(Constants.MtnRequestFields.MtnClientName);
            var token = await _cacheManager.GetOrSetTokenAsync(Constants.EasyCachingFields.CacheKey); 

            // Set Headers
            _client.DefaultRequestHeaders.Add(Constants.MtnRequestFields.authorizationKey, String.Format(Constants.MtnRequestFields.BearerKeyWord, token));
            _client.DefaultRequestHeaders.Add(Constants.MtnRequestFields.referenceId, uuid.ToString());
            _client.DefaultRequestHeaders.Add(Constants.MtnRequestFields.targetEnvironmentKey, Constants.MtnRequestFields.targetEnvironmentValue);

            try
            {
                HttpResponseMessage response = await _client.GetAsync(String.Format(Constants.MtnRequestFields.RetrievePaymentEndPoint, uuid));
                var responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }

            return null;
        }

        /// <summary>
        /// Implementation of SubmitPaymentAsync to process a payment.
        /// </summary>
        /// <param name="transaction">JSON Payload of transaction</param>
        /// <returns>JSON Payload of transaction status (Successful or Failed) with all the other details.</returns>
        public async Task<string> SubmitPaymentAsync(MTNTransactionModel transaction)
        {
            var _client = _clientFactory.CreateClient(Constants.MtnRequestFields.MtnClientName);
            // Generate Transaction UUID
            var uuid = await _helpers.CreateReferenceIDAsync(transaction.TransactionRecord.TransactionReferenceId);
            var token = await _cacheManager.GetOrSetTokenAsync(Constants.EasyCachingFields.CacheKey); 

            // If UUID Is greater than or less that 36 Characters long, return whatever value it holds
            if (uuid.Length > Constants.MtnRequestFields.uuidMaxLength || uuid.Length < Constants.MtnRequestFields.uuidMaxLength)
            {
                return uuid;
            }

            // Set Headers
            _client.DefaultRequestHeaders.Add(Constants.MtnRequestFields.authorizationKey, String.Format(Constants.MtnRequestFields.BearerKeyWord, token));
            _client.DefaultRequestHeaders.Add(Constants.MtnRequestFields.referenceId, uuid.ToString());
            _client.DefaultRequestHeaders.Add(Constants.MtnRequestFields.targetEnvironmentKey, Constants.MtnRequestFields.targetEnvironmentValue);

            try
            {
                var jsonString = JsonConvert.SerializeObject(transaction.PaymentRecord);

                HttpContent content = new StringContent(jsonString, Encoding.UTF8, Constants.MtnRequestFields.ContentType);
                HttpResponseMessage response = await _client.PostAsync(Constants.MtnRequestFields.MakePaymentEndPoint, content);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"Transaction Reference ID: {uuid}");
                    return await RetrievePaymentDetailsAsync(uuid.ToString());
                }

                var exception = new Exception($"Payment gateway responded with the following error: {response.StatusCode} | {response.ReasonPhrase}");
                _logger.LogError(exception, "Error while processing transaction");

                return null;
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message);
            }

            return null;
        }

    }
}
