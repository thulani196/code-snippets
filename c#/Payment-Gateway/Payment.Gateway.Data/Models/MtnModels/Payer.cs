using Newtonsoft.Json;

namespace Payment.Gateway.Data.Models.MtnModels
{
    public class Payer
    {
        [JsonProperty("partyIdType")]
        public string PartyIdType { get; set; }

        [JsonProperty("partyId")]
        public string PartyId { get; set; }
    }
}