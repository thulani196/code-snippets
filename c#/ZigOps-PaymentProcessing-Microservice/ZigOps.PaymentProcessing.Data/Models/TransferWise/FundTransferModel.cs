//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Newtonsoft.Json;

namespace ZigOps.PaymentProcessing.Data.Models.TransferWise
{
    public class FundTransferModel
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("profile")]
        public string Profile { get; set; }

        [JsonProperty("Transfer")]
        public int Transfer { get; set; }
    }
}
