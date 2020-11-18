//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Newtonsoft.Json;
using System;

namespace ZigOps.PaymentProcessing.Data.Models
{
    public class NotificationModel
    {
        [JsonProperty("messageId")]
        public string MessageId { get; set; } = Guid.NewGuid().ToString();

        [JsonProperty("date")]
        public DateTime Date { get; set; } = DateTime.Now;

        [JsonProperty("publisher")]
        public string Publisher { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
