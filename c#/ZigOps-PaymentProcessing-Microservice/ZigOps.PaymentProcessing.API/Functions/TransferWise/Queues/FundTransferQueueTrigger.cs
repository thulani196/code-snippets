using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using ZigOps.PaymentProcessing.Service.Interfaces.TransferWise;

namespace ZigOps.PaymentProcessing.API.Functions.TransferWise
{
    public class FundTransferQueueTrigger
    {
        //initiates the process of funding a transfer
        private readonly ITransferWiseService _transferWiseService;

        public FundTransferQueueTrigger(ITransferWiseService transferWiseService)
        {
            _transferWiseService = transferWiseService;
        }

        [FunctionName("FundTransferQueueTrigger")]
        public async void FundTransfer([ServiceBusTrigger("transferwise-fund-transfer", Connection = "ServiceBusConnectionString")]string messageBody, ILogger log)
        { 
            try
            {
                await _transferWiseService.FundRequest(messageBody).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                log.LogInformation($"FundTransferQueueTrigger Exception message: {e.Message}");
                throw;
            }
        }
    }
}
