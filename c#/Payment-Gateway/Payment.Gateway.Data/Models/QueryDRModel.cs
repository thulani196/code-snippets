using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Payment.Gateway.Data.Models
{
    public class QueryDRModel
    {
        [JsonProperty("vpc_MerchTxnRef")]
        public string VpcMerchTxnRef { get; set; }
    }
}
