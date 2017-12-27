using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CurrencyService.Model
{
    [DataContract]
    public class CurrencyFeed
    {
        [DataMember]
        public string Success { get; set; }

        [DataMember]
        public string Terms { get; set; }

        [DataMember]
        public string Privacy { get; set; }

        [DataMember]
        public string Timestamp { get; set; }

        [DataMember]
        public string Source { get; set; }

        [DataMember]
        public Dictionary<string, string> Quotes { get; set; }
        
    }
}
