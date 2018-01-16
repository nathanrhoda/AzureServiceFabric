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
            Id = Guid.NewGuid();
            Items = new List<OrderItem>();
        }

        public Quote(Guid guid)
        {
            Id = guid;
            Items = new List<OrderItem>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string ContactNumber { get; set; }

        public List<OrderItem> Items;            
    }
}
