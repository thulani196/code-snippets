using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Text;
using System.Threading.Tasks;
using ZigOps.PaymentProcessing.Data.Interface;
using ZigOps.PaymentProcessing.Data.Models;

namespace ZigOps.PaymentProcessing.Data.Helpers
{
    public class SendEmail : ISendEmail
    {
        //Email sender class
        private readonly ILogger _logger;

        public SendEmail(ILogger<SendEmail> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Creates and sends an email asynchronously
        /// </summary>
        /// <param name="email">Email model</param>
        /// <returns>Task</returns>
        public async Task SendEmailAsync(EmailModel email)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(Constants.SMTPSettings.SenderName, Constants.SMTPSettings.SenderEmail));
                message.To.Add(new MailboxAddress(email.RecipientName, email.Recipient));
                message.Subject = email.Subject;
                message.Body = new TextPart("html")
                {
                    Text = email.MessageBody
                };
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await client.ConnectAsync(Constants.SMTPSettings.Server);
                    byte[] data = Convert.FromBase64String(Constants.SMTPSettings.Chocklate);
                    string chocklate = Encoding.UTF8.GetString(data);
                    await client.AuthenticateAsync(Constants.SMTPSettings.Username, chocklate);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
    }
}
