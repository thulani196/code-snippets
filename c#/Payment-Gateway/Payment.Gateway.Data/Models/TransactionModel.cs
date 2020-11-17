
namespace Payment.Gateway.Data.Models
{
    using Newtonsoft.Json;

    public class TransactionModel
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

        [JsonProperty("vpc_OrderInfo")]
        public string VpcOrderInfo { get; set; }

        [JsonProperty("vpc_Amount")]
        public string VpcAmount { get; set; }

        [JsonProperty("vpc_Currency")]
        public string VpcCurrency { get; set; }

        [JsonProperty("vpc_Card")]
        public string VpcCard { get; set; }

        [JsonProperty("vpc_CardNum")]
        public string VpcCardNum { get; set; }

        [JsonProperty("vpc_CardExp")]
        public string VpcCardExp { get; set; }
        
        [JsonProperty("vpc_Gateway")]
        public string VpcGateway { get; set; }
       
        [JsonProperty("vpc_ReturnURL")]
        public string VpcReturnUrl { get; set; }

        [JsonProperty("vpc_CardSecurityCode")]
        public string VpcCardSecurityCode { get; set; }
    }
}
