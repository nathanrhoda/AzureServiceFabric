using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quota.Gateway.Model
{
    public class OrderItem
    {
        public string productGuid { get; set; }
        public double Quantity { get; set; }
    }
}
