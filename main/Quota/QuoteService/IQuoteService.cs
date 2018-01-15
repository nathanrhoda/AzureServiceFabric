using QuoteService.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuoteService
{
    public interface IQuoteService
    {
        Task<Quote> Get(Guid id);
        Task CreateQuote(Quote  quote);        
    }
}
