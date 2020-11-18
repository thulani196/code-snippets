//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Newtonsoft.Json;

namespace ZigOps.PaymentProcessing.Data.Models.TransferWise
{
    public class CreateTransferRequestModel
    {
       
        [JsonProperty("targetAccount")]
        public int TargetAccount { get; set; }

        [JsonProperty("quote")]
        public int Quote { get; set; }

        [JsonProperty("customerTransactionId")]
        public string CustomerTransactionId { get; set; }

        [JsonProperty("details")]
        public Details Detail { get; set; }

        public class Details
        {
            [JsonProperty("reference")]
            public string Reference { get; set; }

            [JsonProperty("transferPurpose")]
            public string TransferPurpose { get; set; }

            [JsonProperty("sourceOfFunds")]
            public string SourceOfFunds { get; set; }
        }
    }
}
