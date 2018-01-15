using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Quota.CommonUtils;
using System.Collections.Generic;
using System.Fabric;
using System.Threading.Tasks;
using System.Threading;
using QuoteService.Model;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Data;
using System;

namespace QuoteService
{
    /// <summary>
    /// The FabricRuntime creates an instance of this class for each service type instance. 
    /// </summary>
    internal sealed class QuoteService : StatefulService, IQuoteService
    {
        public QuoteService(StatefulServiceContext context)
            : base(context)
        { }

        public async Task CreateQuote(Quote quote)
        {
            var quotes = await StateManager.GetOrAddAsync<IReliableDictionary<Guid, Quote>>("quote");

            using (var tx = StateManager.CreateTransaction())
            {
                await quotes.AddOrUpdateAsync(tx, quote.Id, quote, (id, value) => quote);

                await tx.CommitAsync();
            }
        }

        public async Task<Quote> Get(Guid id)
        {
            var quotes = await StateManager.GetOrAddAsync<IReliableDictionary<Guid, Quote>>("quote");

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
    }
}
