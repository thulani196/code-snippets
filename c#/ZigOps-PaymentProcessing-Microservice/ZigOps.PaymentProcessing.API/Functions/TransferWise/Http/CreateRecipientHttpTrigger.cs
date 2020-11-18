//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ZigOps.PaymentProcessing.Data.Helpers;
using ZigOps.PaymentProcessing.Data.Models.TransferWise;
using ZigOps.PaymentProcessing.Service.Interfaces.TransferWise;

namespace ZigOps.PaymentProcessing.API.Functions.TransferWise
{
    public class CreateRecipientHttpTrigger
    {
        /// <summary>
        /// Http function for creating recipient accounts to credit
        /// </summary>
        private readonly ITransferWiseService _transferWiseService;
        private readonly ILogger _logger;

        public CreateRecipientHttpTrigger(ITransferWiseService transferWiseService, ILogger<CreateProfileHttpTrigger> logger)
        {
            _transferWiseService = transferWiseService;
            _logger = logger;
        }

        [FunctionName("CreateRecipientHttpTrigger")]
        public async Task<IActionResult> CreateRecipient(
            [HttpTrigger(AuthorizationLevel.Function, "post", 
            Route = Constants.ApiEndpoints.TRANSFERWISE_CREATE_RECIPIENT)] 
            HttpRequest req)
        {
            try
            {
                var headers = req.Headers;
                var tokens = headers.TryGetValue("Authorization", out var _tokens);
                var token = _tokens.First().Substring("Bearer ".Length);
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<CreateRecipientRequestModel>(requestBody);
                var response = await _transferWiseService.CreateRecipient(data, token).ConfigureAwait(false);
                return new OkObjectResult(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return (ActionResult)new BadRequestObjectResult(new
                {
                    success = false,
                    message = e.Message
                });
            }
        }
    }
}
