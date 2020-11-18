//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using ZigOps.PaymentProcessing.Data.Models.Okra;
using ZigOps.PaymentProcessing.Service.Interfaces.Okra;

namespace ZigOps.PaymentProcessing.API
{
    public class PaymentRequestQueueTrigger
    {
        private readonly IOkraService _directDebitService;

        public PaymentRequestQueueTrigger(IOkraService directDebitService)
        {
            _directDebitService = directDebitService;
        }

        [FunctionName("OkraPayment")]
        public async void CreatePayment([ServiceBusTrigger("okra-direct-debit", Connection = "ServiceBusConnectionString")]string messageBody, ILogger log)
        {
            try
            { 
                var data = JsonConvert.DeserializeObject<OkraQueueModel>(messageBody);
                string token = data.Token;
                await _directDebitService.CreateDirectDebit(data, token).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                log.LogInformation($"Queue trigger function error: {e.Message}");
                throw;
            }
        }
    }
}
