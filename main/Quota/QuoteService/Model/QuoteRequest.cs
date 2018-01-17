using System;
using System.Collections.Generic;

namespace QuoteService.Model
{
    public class QuoteRequest
    {
        public QuoteRequest()
        {
            Items = new List<OrderItem>();
        }
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string ContactNumber { get; set; }

        public List<OrderItem> Items { get; set; }

        public bool IsValid()
        {
            if (String.IsNullOrEmpty(Name) ||
                String.IsNullOrEmpty(Surname) ||
                String.IsNullOrEmpty(Email) ||
                String.IsNullOrEmpty(ContactNumber))
            {
                return false;
            }

            return true;
        }
    }
}