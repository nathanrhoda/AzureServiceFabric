using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CurrencyService.Model
{
    [DataContract]
    public class CurrencyFeed
    {

        [JsonProperty("success")]
        public string Success { get; set; }

        [JsonProperty("terms")]
        public string Terms { get; set; }

        [JsonProperty("privacy")]
        public string Privacy { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("quotes")]
        public Dictionary<string, string> Quotes { get; set; }
    }
}
