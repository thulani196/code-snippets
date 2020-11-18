//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using ZigOps.PaymentProcessing.Data.Helpers;
using ZigOps.PaymentProcessing.Service.Interfaces.TransferWise;

namespace ZigOps.PaymentProcessing.API.Functions.TransferWise
{
    public class CreateProfileHttpTrigger
    {
        /// <summary>
        /// Http function for creating user profiles i.e business profile and personal profile
        /// </summary>
        private readonly ITransferWiseService _transferWiseService;
        private readonly ILogger _logger;

        public CreateProfileHttpTrigger(ITransferWiseService transferWiseService,
            ILogger<CreateProfileHttpTrigger> logger)
        {
            _transferWiseService = transferWiseService;
            _logger = logger;
        }

        [FunctionName("CreateProfileHttpTrigger")]
        public async Task<IActionResult> CreateProfile(
            [HttpTrigger(AuthorizationLevel.Function, "get", 
            Route = Constants.ApiEndpoints.TRANSFERWISE_CREATE_PROFILE)] 
            HttpRequest req)
        {
            try
            {
                var headers = req.Headers;
                var tokens = headers.TryGetValue("Authorization", out var _tokens);
                var token = _tokens.First().Substring("Bearer ".Length);
                var response = await _transferWiseService.CreateProfile(token).ConfigureAwait(false);
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
