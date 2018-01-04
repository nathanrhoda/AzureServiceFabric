using System;

namespace QuotationService.Model
{
    public class QuoteRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string CategoryOfGoods { get; set; }
        public string ContainerSize { get; set; }
        public string UnitOfGoods { get; set; }
        public int CostPriceOfGoods { get; set; }

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