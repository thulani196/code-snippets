//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Newtonsoft.Json;

namespace ZigOps.PaymentProcessing.Data.Models.TransferWise
{
    /// <summary>
    /// Defines the payload the direct debit queue must contain for TransferWise.
    /// </summary>
    public class TransferWiseQueueModel : GenericQueueModel
    {
        [JsonProperty("profile")]
        public string Profile { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("transfer_purpose")]
        public string TransferPurpose { get; set; }

        [JsonProperty("source_of_funds")]
        public string SourceOfFunds { get; set; }

        [JsonProperty("source_currency")]
        public string SourceCurrency { get; set; }

        [JsonProperty("rate_type")]
        public string RateType { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("fund_transfer_type")]
        public string FundTransferType { get; set; }
    }
}
