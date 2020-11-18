//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using ZigOps.PaymentProcessing.Data.Interface;
using ZigOps.PaymentProcessing.Data.Models;

namespace ZigOps.PaymentProcessing.Data.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly ISendEmail _sendEmail;
        private readonly ILogger<EmailRepository> _logger;
        public EmailRepository(ISendEmail sendEmail, ILogger<EmailRepository> logger)
        {
            _sendEmail = sendEmail;
            _logger = logger;
        }
        public async Task Send(EmailModel email)
        {
            try
            {
                await _sendEmail.SendEmailAsync(email);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
    }
}
