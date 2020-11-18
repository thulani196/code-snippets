//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using ZigOps.PaymentProcessing.Service.Interfaces.TransferWise;

namespace ZigOps.PaymentProcessing.API.Functions.TransferWise
{
    public class CreateTransferQueueTrigger
    {
        /// <summary>
        /// Initiates the process of creating a transfer
        /// </summary>
        private readonly ITransferWiseService _transferWiseService;
        public CreateTransferQueueTrigger(ITransferWiseService transferWiseService)
        {
            _transferWiseService = transferWiseService;
        }
        [FunctionName("CreateTransferQueueTrigger")]
        public async void CreateTransfer([ServiceBusTrigger("transferwise-create-transfer", Connection = "ServiceBusConnectionString")]string messageBody, ILogger log)
        {
            try
            {
                await _transferWiseService.TransferRequest(messageBody).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                log.LogInformation($"CreateTransferQueueTrigger Exception message: {e.Message}");
                throw;
            }
        }
    }
}
