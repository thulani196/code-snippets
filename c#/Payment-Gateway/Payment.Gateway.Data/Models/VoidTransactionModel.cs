
namespace Payment.Gateway.Data.Models
{
    using Newtonsoft.Json;

    public class VoidTransactionModel
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

        [JsonProperty("vpc_TransNo")]
        public string VpcTransNo { get; set; }

        [JsonProperty("vpc_User")]
        public string VpcUser { get; set; }

        [JsonProperty("vpc_Password")]
        public string VpcPassword { get; set; }
    }
}
