
namespace Payment.Gateway.Data.Models
{
    using Newtonsoft.Json;

    public class ResponseModel
    {
        [JsonProperty("vpc_Amount")]
        public string VpcAmount { get; set; }

        [JsonProperty("vpc_BatchNo")]
        public string VpcBatchNo { get; set; }

        [JsonProperty("vpc_Command")]
        public string VpcCommand { get; set; }

        [JsonProperty("vpc_Locale")]
        public string VpcLocale { get; set; }

        [JsonProperty("vpc_MerchTxnRef")]
        public string VpcMerchTxnRef { get; set; }

        [JsonProperty("vpc_Merchant")]
        public string VpcMerchant { get; set; }

        [JsonProperty("vpc_Message")]
        public string VpcMessage { get; set; }

        [JsonProperty("vpc_TransactionNo")]
        public string VpcTransactionNo { get; set; }

        [JsonProperty("vpc_TxnResponseCode")]
        public string VpcTxnResponseCode { get; set; }

        [JsonProperty("vpc_Version")]
        public string VpcVersion { get; set; }

        [JsonProperty("vpc_Card")]
        public string VpcCard { get; set; }

        [JsonProperty("vpc_ReceiptNo")]
        public string VpcReceiptNo { get; set; }

        [JsonProperty("vpc_RefundedAmount")]
        public string VpcRefundedAmount { get; set; }

        [JsonProperty("vpc_ShopTransactionNo")]
        public string VpcShopTransactionNo { get; set; }

        [JsonProperty("vpc_AcqResponseCode")]
        public string VpcAcqResponseCode { get; set; }
    }
}
