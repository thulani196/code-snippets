//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Newtonsoft.Json;

namespace ZigOps.PaymentProcessing.Data.Models.Okra
{
    /// <summary>
    /// Defines the Okra request payload.
    /// </summary>
    public class DirectDebitRequestModel
    {
        [JsonProperty("provider")]
        public string Provider { get; set; }

        [JsonProperty("account_to_debit")]
        public string AccountToDebit { get; set; }

        [JsonProperty("account_to_credit")]
        public string AccountToCredit { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("garnish")]
        public bool Garnish { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("testing")]
        public bool Testing { get; set; }
    }
}
