//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Microsoft.Azure.Cosmos.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ZigOps.PaymentProcessing.Data.Models.TransferWise
{
    public class CreateQuoteResponseModel
    {
        
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("target")]
        public string Target { get; set; }

        [JsonProperty("sourceAmount")]
        public double SourceAmount { get; set; }

        [JsonProperty("targetAmount")]
        public double TargetAmount { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("rate")]
        public double Rate { get; set; }

        [JsonProperty("createdTime")]
        public DateTime CreatedTime { get; set; }

        [JsonProperty("createdByUserId")]
        public int CreatedByUserId { get; set; }

        [JsonProperty("profile")]
        public int Profile { get; set; }

        [JsonProperty("business")]
        public int Business { get; set; }

        [JsonProperty("rateType")]
        public string RateType { get; set; }

        [JsonProperty("deliveryEstimate")]
        public DateTime DeliveryEstimate { get; set; }

        [JsonProperty("fee")]
        public double Fee { get; set; }

        [JsonProperty("feeDetails")]
        public FeeDetails FeeDetail { get; set; }

        [JsonProperty("allowedProfileTypes")]
        public List<string> AllowedProfileTypes { get; set; }

        [JsonProperty("guaranteedTargetAmount")]
        public bool GuaranteedTargetAmount { get; set; }

        [JsonProperty("ofSourceAmount")]
        public bool OfSourceAmount { get; set; }

        public class FeeDetails : TableEntity
        {
            [JsonProperty("transferwise")]
            public double Transferwise { get; set; }

            [JsonProperty("payIn")]
            public double PayIn { get; set; }

            [JsonProperty("discount")]
            public double Discount { get; set; }

            [JsonProperty("priceSetId")]
            public int PriceSetId { get; set; }

            [JsonProperty("partner")]
            public int Partner { get; set; }
        }
    }
}
