using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Payment.Gateway.Data.Models
{
    public class RefundModel
    {
        [JsonProperty("vpc_Version")]
        public string VpcVersion { get; set; }

        [JsonProperty("vpc_Command")]
        public string VpcCommand { get; set; }

        [JsonProperty("vpc_Merchant")]
        public string VpcMerchant { get; set; }

        [JsonProperty("vpc_AccessCode")]
        public string VpcAccessCode { get; set; }

        [JsonProperty("vpc_MerchTxnRef")]
        public string VpcMerchTxnRef { get; set; }

        [JsonProperty("vpc_Amount")]
        public string VpcAmount { get; set; }

        [JsonProperty("vpc_TransNo")]
        public string VpcTransNo { get; set; } 

        [JsonProperty("vpc_User")]
        public string VpcUser { get; set; }

        [JsonProperty("vpc_Password")]
        public string VpcPassword { get; set; }
    }
}
