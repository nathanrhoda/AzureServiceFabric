using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteService.Model
{
    public class Quote
    {
        public Quote()
        {
            Items = new List<QuoteItem>();
        }

        public Guid Id { get; set; }
        public List<QuoteItem> Items;            
    }
}
