using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Payment.Gateway.Data.Models.MtnModels
{
    public class TransactionReference
    {
        [JsonProperty("TransactionReferenceID")]
        public string TransactionReferenceID { get; set; }
    }
}
