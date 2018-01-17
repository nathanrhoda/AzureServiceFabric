using QuoteService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteService.Repository
{
    public interface IQuoteRepository
    {
        Task<IEnumerable<Quote>> Get();
        Task<Quote> Get(Guid id);
        Task AddOrUpdate(Quote quote);
        Task Delete(Guid id);
    }
}
