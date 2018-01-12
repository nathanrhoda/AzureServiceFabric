using QuotationService.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuotationService
{
    public interface IQuotationService
    {
        Task<Quote> Get(Guid id);
        Task CreateQuote(Quote  quote);        
    }
}
