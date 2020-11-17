using Newtonsoft.Json;

namespace Payment.Gateway.Data.Models.MtnModels
{
    public class PaymentRecord
    {
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
    }
}