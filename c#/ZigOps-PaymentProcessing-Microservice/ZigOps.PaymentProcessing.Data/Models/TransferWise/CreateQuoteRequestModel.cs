//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Microsoft.Azure.Cosmos.Table;
using Newtonsoft.Json;

namespace ZigOps.PaymentProcessing.Data.Models.TransferWise
{
    public class CreateQuoteRequestModel
    {
        [JsonProperty("profile")]
        public int Profile { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("target")]
        public string Target { get; set; }

        [JsonProperty("rateType")]
        public string RateType { get; set; }

        [JsonProperty("targetAmount")]
        public double TargetAmount { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
