using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuoteService.Model;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using System.Threading;

namespace QuoteService.Repository
{
    public class QuoteRepository : IQuoteRepository
    {
        private IReliableStateManager _stateManager;
        private object cancellationToken;

        public QuoteRepository(IReliableStateManager stateManager)
        {
            _stateManager = stateManager;
        }

        public async Task AddQuote(Quote quote)
        {
            var quotes = await _stateManager.GetOrAddAsync<IReliableDictionary<Guid, Quote>>("quotes");

            using (var tx = _stateManager.CreateTransaction())
            {
                await quotes.AddOrUpdateAsync(tx, quote.Id, quote, (id, value) => quote);

                await tx.CommitAsync();
            }
        }

        public async Task Delete(Guid id)
        {
            var quotes = await _stateManager.GetOrAddAsync<IReliableDictionary<Guid, Quote>>("quotes");

            using (var tx = _stateManager.CreateTransaction())
            {
                await quotes.TryRemoveAsync(tx, id);

                await tx.CommitAsync();
            }
        }

        public async Task<IEnumerable<Quote>> Get()
        {
            var quotes = await _stateManager.GetOrAddAsync<IReliableDictionary<Guid, Quote>>("quotes");
            var result = new List<Quote>();

            using (var tx = _stateManager.CreateTransaction())
            {
                var allQuotes = await quotes.CreateEnumerableAsync(tx, EnumerationMode.Unordered);

                using (var enumerator = allQuotes.GetAsyncEnumerator())
                {
                    while (await enumerator.MoveNextAsync(CancellationToken.None))
                    {
                        KeyValuePair<Guid, Quote> current = enumerator.Current;
                        result.Add(current.Value);
                    }
                }
            }

            return result;
        }

        public async Task<Quote> Get(Guid id)
        {
            var quotes = await _stateManager.GetOrAddAsync<IReliableDictionary<Guid, Quote>>("quotes");

            using (var tx = _stateManager.CreateTransaction())
            {
                ConditionalValue<Quote> quote = await quotes.TryGetValueAsync(tx, id);

                return quote.HasValue ? quote.Value : null;
            }
        }

        public async Task Put(Guid guid, Quote quote)
        {
            var quotes = await _stateManager.GetOrAddAsync<IReliableDictionary<Guid, Quote>>("quotes");

            using (var tx = _stateManager.CreateTransaction())
            {
                await quotes.AddOrUpdateAsync(tx, guid, quote, (id, value) => quote);

                await tx.CommitAsync();
            }
        }
    }
}
