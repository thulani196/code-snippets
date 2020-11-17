using Newtonsoft.Json;

namespace Payment.Gateway.Data.Models.MtnModels
{
    public class MTNTransactionModel
    {
        [JsonProperty("TransactionRecord")]
        public TransactionRecord TransactionRecord { get; set; }

        [JsonProperty("TaymentRecord")]
        public PaymentRecord PaymentRecord { get; set; }
    }
}
