//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using ZigOps.PaymentProcessing.Data.Interface;
using ZigOps.PaymentProcessing.Data.Interface.Okra;
using ZigOps.PaymentProcessing.Data.Models;
using ZigOps.PaymentProcessing.Data.Models.Okra;
using ZigOps.PaymentProcessing.Data.Models.Okra.Database;
using Constants = ZigOps.PaymentProcessing.Data.Helpers.Constants;

namespace ZigOps.PaymentProcessing.Data.Repositories.Okra
{
    public class OkraRepository : IOkraRepository
    {
        private readonly ILogger<OkraRepository> _logger;
        private readonly IOkraDatabaseRepository _okraDatabaseRepository;
        public OkraRepository(ILogger<OkraRepository> logger, IOkraDatabaseRepository okraDatabaseRepository) 
        {
            _logger = logger;
            _okraDatabaseRepository = okraDatabaseRepository;
        }
        public async Task CreateDirectDebit(IGenerateRequest _request, OkraQueueModel data, string token)
        {
            try
            {
                foreach (GenericQueueModel.Employee employee in data.Employees)
                {
                    DirectDebitRequestModel request = new DirectDebitRequestModel()
                    {
                        Provider = "Okra",
                        AccountToDebit = employee.SourceAccount,
                        AccountToCredit = employee.TargetAccount,
                        Amount = employee.Amount.ToString(),
                        Garnish = Constants.OkraOptions.GARNISH,
                        Success = Constants.OkraOptions.SUCCESS,
                        Testing = Constants.OkraOptions.TESTING
                    };
                    string requestPayload = JsonConvert.SerializeObject(request);
                    var response = await _request.POSTAsync(
                        Constants.OkraEndpoints.OKRA_SANDBOX_PROCESS_DIRECT_DEBIT,
                        new StringContent(requestPayload), token);
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var responsePayload = JsonConvert.DeserializeObject<DirectDebitResponseModel>(jsonString);
                    //log response
                    DirectDebits directDebits = new DirectDebits
                    {
                        Request = requestPayload,
                        Response = jsonString,
                        Token = token
                    };
                    await _okraDatabaseRepository.CreateDirectDebitsAsync(
                        directDebits,
                        Constants.TableDBConstant.OKRA_DIRECT_DEBITS_TABLE);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
        public async Task<object> Callback(IGenerateRequest _request, string record, string token)
        {
            try
            {
                var uriBuilder = new UriBuilder(Constants.OkraEndpoints.OKRA_SANDBOX_CALLBACK_URL);
                var parameters = HttpUtility.ParseQueryString(string.Empty);
                parameters["record"] = record;
                parameters["method"] = "DEBIT";
                uriBuilder.Query = parameters.ToString();
                Uri finalUrl = uriBuilder.Uri;
                string uri = finalUrl.ToString();
                var response = await _request.GETAsync(uri, token);
                var jsonString = await response.Content.ReadAsStringAsync();
                return jsonString;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
    }
}
