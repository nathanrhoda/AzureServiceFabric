using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuotationService.Model
{
    public class QuoteItem
    {    
        public Product Item { get; set; }
        public double Quantity { get; set; }
    }
}
