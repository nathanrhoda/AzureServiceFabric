using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuotationService.Model
{
    public class Quote
    {
        public Guid Id { get; set; }
        public List<QuoteItem> Items;            
    }
}
