using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace QuoteService.Model
{
    public class QuoteRequest
    {        
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string ContactNumber { get; set; }

        public List<QuoteItem> Items { get; set; }

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