using QuoteService.Model;
using System;

namespace QuoteService.Test.TestObjects
{
    public static class QuoteBuilder
    {
        public static Quote Quote1 = new Quote
        {
            Id = Guid.NewGuid(),
            Name = "Quote1"
        };

        public static Quote Quote2 = new Quote
        {
            Id = Guid.NewGuid(),
            Name = "Quote2"
        };

        public static QuoteRequest ValidQuoteRequest = new QuoteRequest
        {
            Name = "Woof",
            Surname = "bark",
            Email = "woof.bark@dog.com",
            ContactNumber = "k9",
        };

        public static QuoteRequest InvalidQuoteRequest = new QuoteRequest
        {
            Name = "Invalid Request",           
        };


        public static QuoteRequest NullQuoteRequest = null;

    }
}
