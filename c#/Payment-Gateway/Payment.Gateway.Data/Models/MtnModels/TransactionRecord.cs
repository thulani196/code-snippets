using Newtonsoft.Json;

namespace Payment.Gateway.Data.Models.MtnModels
{
    public class TransactionRecord
    {
        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }
        [JsonProperty("TransactionReferenceId")]
        public string TransactionReferenceId { get; set; }
    }
}