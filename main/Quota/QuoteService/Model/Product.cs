using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteService.Model
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string UnitType { get; set; }
        public double Price { get; set; }
       
    }
}
