//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using ZigOps.PaymentProcessing.Data.Helpers;
using ZigOps.PaymentProcessing.Data.Models.Okra;
using ZigOps.PaymentProcessing.Data.Models.Okra.Database;
using ZigOps.PaymentProcessing.Service.Interfaces.Okra;

namespace ZigOps.PaymentProcessing.API.Functions.Okra
{
    public class CallbackTimerTrigger
    {
        private readonly IOkraService _directDebitService;
        private readonly IOkraDatabaseService _okraDatabaseService;
        public CallbackTimerTrigger(IOkraService directDebitService, IOkraDatabaseService okraDatabaseService)
        {
            _directDebitService = directDebitService;
            _okraDatabaseService = okraDatabaseService;
        }
        [FunctionName("CallbackTimerTrigger")]
        public async void Run([TimerTrigger("0 */5 * * * *")]TimerInfo timer, ILogger log)
        {
            log.LogInformation($"Okra CallbackTimerTrigger Timer trigger function executed at: {DateTime.Now}");
            try
            {   
                DirectDebits directDebits = await _okraDatabaseService.GetDirectDebitsByStatusAsync(
                    Constants.OkraOptions.STATUS,
                    Constants.TableDBConstant.OKRA_DIRECT_DEBITS_TABLE);
                string token = directDebits.Token;
                DirectDebitResponseModel response = JsonConvert.DeserializeObject<DirectDebitResponseModel>(directDebits.Response);
                string record = response.Data.Job.Record;
                log.LogInformation($"token {token}");
                log.LogInformation($"record {record}");
                await _directDebitService.Callback(record, token).ConfigureAwait(false);
                //Todo: create a queue message to update the payrun
            }
            catch (Exception e)
            {
                log.LogInformation($"Timer trigger function exception, message: {e.Message}");
            }
        }
    }
}
