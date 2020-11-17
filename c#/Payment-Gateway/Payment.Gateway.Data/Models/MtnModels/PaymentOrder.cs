using Newtonsoft.Json;
using Payment.Gateway.Data.Models.MtnModels;

namespace Payment.Gateway.Logic.Interfaces
{
    public class PaymentOrder
    {
        [JsonProperty("OrderId")]
        public string OrderId { get; set; }

        [JsonProperty("UserId")]
        public string UserId { get; set; }

        [JsonProperty("financialTransactionId")]
        public string FinancialTransactionId { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("externalId")]
        public string ExternalId { get; set; }

        [JsonProperty("payerMessage")]
        public string PayerMessage { get; set; }

        [JsonProperty("payeeNote")]
        public string PayeeNote { get; set; }

        [JsonProperty("payer")]
        public Payer Payer { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}