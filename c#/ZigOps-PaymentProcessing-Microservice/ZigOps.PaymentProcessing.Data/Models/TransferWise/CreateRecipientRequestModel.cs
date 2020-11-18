//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Newtonsoft.Json;

namespace ZigOps.PaymentProcessing.Data.Models.TransferWise
{
    public class CreateRecipientRequestModel
    {
       
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("profile")]
        public int Profile { get; set; }

        [JsonProperty("accountHolderName")]
        public string AccountHolderName { get; set; }

        [JsonProperty("legalType")]
        public string LegalType { get; set; }

        [JsonProperty("details")]
        public Details Detail { get; set; }

        public class Details
        {
            [JsonProperty("sortCode")]
            public string SortCode { get; set; }

            [JsonProperty("accountNumber")]
            public string AccountNumber { get; set; }
        }
    }
}
