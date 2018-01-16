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
            
    }
}
