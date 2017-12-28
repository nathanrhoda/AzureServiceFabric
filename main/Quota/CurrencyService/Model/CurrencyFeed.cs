using System.Collections.Generic;

namespace CurrencyService.Model
{    
    public class CurrencyFeed
    {
        public string Success { get; set; }

        public string Terms { get; set; }

        public string Privacy { get; set; }

        public string Timestamp { get; set; }

        public string Source { get; set; }

        public Dictionary<string, string> Quotes { get; set; }
        
    }
}
