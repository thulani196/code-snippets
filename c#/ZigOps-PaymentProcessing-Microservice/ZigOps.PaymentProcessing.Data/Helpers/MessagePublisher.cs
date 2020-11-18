//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using System.Threading.Tasks;
using ZigOps.PaymentProcessing.Data.Interface;

namespace ZigOps.PaymentProcessing.Data.Helpers
{
    /// <summary>
    /// A helper class for creating messages for a queue
    /// </summary>
    public class MessagePublisher : IMessagePublisher
    {
        private readonly IConfiguration _configuration;

        public MessagePublisher(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// This method will publish a message to a specified queue
        /// </summary>
        /// <param name="queue">Name of the queue to receive the message</param>
        /// <param name="rawMessage">string message for the queue</param>
        /// <returns>Task</returns>
        public async Task Publish(string queue, string rawMessage)
        {
            QueueClient client = new QueueClient(_configuration["ServiceBusConnectionString"], queue);
            var plainTextBytes = Encoding.UTF8.GetBytes(rawMessage);
            string message = Convert.ToBase64String(plainTextBytes);
            await client.SendMessageAsync(message).ConfigureAwait(false);
        }
    }
}
