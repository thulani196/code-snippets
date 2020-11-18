//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using ZigOps.PaymentProcessing.Data.Models.TransferWise;
using ZigOps.PaymentProcessing.Service.Interfaces.TransferWise;

namespace ZigOps.PaymentProcessing.API.Functions.TransferWise
{
    public class PaymentRequestQueueTrigger
    {
        /// <summary>
        /// Receives the payment processing request payload for processing
        /// </summary>
        private readonly ITransferWiseService _transferWiseService;

        public PaymentRequestQueueTrigger(ITransferWiseService transferWiseService)
        {
            _transferWiseService = transferWiseService;
        }

        [FunctionName("TransferWisePayment")]
        public async void CreatePayment([ServiceBusTrigger("transferwise-direct-debits", Connection = "ServiceBusConnectionString")]string messageBody, ILogger log)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<TransferWiseQueueModel>(messageBody);
                string token = data.Token;
                await _transferWiseService.PaymentRequest(data, token).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                log.LogInformation($"TransferWisePayment Exception message: {e.Message}");
                throw;
            }
        }
    }
}
