using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Quota.CommonUtils;
using QuoteService.Model;
using QuoteService.Repository;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;

namespace QuoteService
{
    internal sealed class QuoteService : StatefulService
    {
        public QuoteService(StatefulServiceContext context)
            : base(context)
        { }

        public async Task AddQuote(Quote quote)
        {
            var quotes = await StateManager.GetOrAddAsync<IReliableDictionary<Guid, Quote>>("quotes");

            using (var tx = StateManager.CreateTransaction())
            {
                await quotes.AddOrUpdateAsync(tx, quote.Id, quote, (id, value) => quote);

                await tx.CommitAsync();
            }
        }

        public async Task<IEnumerable<Quote>> Get()
        {
            var quotes = await StateManager.GetOrAddAsync<IReliableDictionary<Guid, Quote>>("quotes");
            var result = new List<Quote>();

            using (var tx = StateManager.CreateTransaction())
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
            var quotes = await StateManager.GetOrAddAsync<IReliableDictionary<Guid, Quote>>("quotes");

            using (var tx = StateManager.CreateTransaction())
            {
                ConditionalValue<Quote> quote = await quotes.TryGetValueAsync(tx, id);

                return quote.HasValue ? quote.Value : null;
            }
        }

        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners() =>
           new ServiceReplicaListener[]
           {
                ServiceReplicaListenerFactory.CreateListener(typeof(Startup), StateManager, (serviceContext, message) => ServiceEventSource.Current.ServiceMessage(serviceContext, message))
           };

        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            var sonyHeadphones = new Product
            {
                Id = Guid.NewGuid(),
                Description = "Sony Headphones",
                Price = 10,
                UnitType = "Per Item"
            };

            var plantronicsHeadphones = new Product
            {
                Id = Guid.NewGuid(),
                Description = "Plantronic Bluetooth Headphones",
                Price = 50,
                UnitType = "Per Item"
            };

            var plantronicsHeadphones2 = new Product
            {
                Id = Guid.NewGuid(),
                Description = "AAPlantronic Bluetooth Headphones",
                Price = 50,
                UnitType = "Per Item"
            };

            QuoteItem quoteItem1 = new QuoteItem
            {
                Item = sonyHeadphones,
                Quantity = 100
            };

            QuoteItem quoteItem2 = new QuoteItem
            {
                Item = plantronicsHeadphones,
                Quantity = 200
            };

            QuoteItem quoteItem3 = new QuoteItem
            {
                Item = sonyHeadphones,
                Quantity = 400
            };

            Quote quote1 = new Quote
            {
                Id = Guid.NewGuid(),
                Items = { quoteItem1, quoteItem2 }
            };

            Quote quote2 = new Quote
            {
                Id = Guid.NewGuid(),
                Items = { quoteItem3 }
            };

            var repo = new QuoteRepository(StateManager);
            await repo.AddQuote(quote1);
            await repo.AddQuote(quote2);
        }
    }
}
