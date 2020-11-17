using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Payment.Gateway.Data.Models.MtnModels
{
    public class TokenObject
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }
    }
}
