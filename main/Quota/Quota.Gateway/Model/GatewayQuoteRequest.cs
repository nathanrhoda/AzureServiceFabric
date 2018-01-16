using System.Collections.Generic;

namespace Quota.Gateway.Model
{
    public class GatewayQuoteRequest
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string ContactNumber { get; set; }

        public List<OrderItem> Items { get; set; }

        public bool IsValid()
        {
            if (Name == null ||
                Surname == null ||
                Email == null ||
                ContactNumber == null)
            {
                return false;
            }

            return true;
        }
    }
}
