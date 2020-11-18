//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Newtonsoft.Json;

namespace ZigOps.PaymentProcessing.Data.Models.Okra
{
    /// <summary>
    /// Defines the response payload from Okra Direct Debit Endpoint.
    /// </summary>
    public class DirectDebitResponseModel
    {
        
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public Payload Data { get; set; }

        public class Payload
        {
            [JsonProperty("job")]
            public Job Job { get; set; }

            [JsonProperty("callback_url")]
            public string CallbackUrl { get; set; }
        }

        public class Job
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("amount")]
            public string Amount { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("customer")]
            public string Customer { get; set; }

            [JsonProperty("currency")]
            public string Currency { get; set; }

            [JsonProperty("account_to_credit")]
            public string AccountToCredit { get; set; }

            [JsonProperty("account_to_debit")]
            public string AccountToDebit { get; set; }

            [JsonProperty("owner")]
            public string Owner { get; set; }

            [JsonProperty("record")]
            public string Record { get; set; }

            [JsonProperty("ref")]
            public string Ref { get; set; }

            [JsonProperty("provider")]
            public string Provider { get; set; }

            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("env")]
            public string Env { get; set; }
        }
    }
}
