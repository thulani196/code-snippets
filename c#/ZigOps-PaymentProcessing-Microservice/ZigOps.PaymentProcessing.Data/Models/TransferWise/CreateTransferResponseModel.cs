//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Newtonsoft.Json;

namespace ZigOps.PaymentProcessing.Data.Models.TransferWise
{
    public class CreateTransferResponseModel
    {
        
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("user")]
        public int User { get; set; }

        [JsonProperty("targetAccount")]
        public int TargetAccount { get; set; }

        [JsonProperty("sourceAccount")]
        public object SourceAccount { get; set; }

        [JsonProperty("quote")]
        public int Quote { get; set; }

        [JsonProperty("quoteUuid")]
        public string QuoteUuid { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("rate")]
        public double Rate { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("business")]
        public int Business { get; set; }

        [JsonProperty("transferRequest")]
        public object TransferRequest { get; set; }

        [JsonProperty("details")]
        public Details Detail { get; set; }

        [JsonProperty("hasActiveIssues")]
        public bool HasActiveIssues { get; set; }

        [JsonProperty("sourceCurrency")]
        public string SourceCurrency { get; set; }

        [JsonProperty("sourceValue")]
        public double SourceValue { get; set; }

        [JsonProperty("targetCurrency")]
        public string TargetCurrency { get; set; }

        [JsonProperty("targetValue")]
        public double TargetValue { get; set; }

        [JsonProperty("customerTransactionId")]
        public string CustomerTransactionId { get; set; }

        public class Details
        {
            [JsonProperty("reference")]
            public string Reference { get; set; }
        }
    }
}
