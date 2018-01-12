using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Quota.CommonUtils;
using System.Collections.Generic;
using System.Fabric;
using System.Threading.Tasks;
using System.Threading;
using QuotationService.Model;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Data;
using System;

namespace QuotationService
{
    /// <summary>
    /// The FabricRuntime creates an instance of this class for each service type instance. 
    /// </summary>
    internal sealed class QuotationService : StatefulService, IQuotationService
    {
        public QuotationService(StatefulServiceContext context)
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

        ///// <summary>
        ///// Optional override to create listeners (like tcp, http) for this service instance.
        ///// </summary>
        ///// <returns>The collection of listeners.</returns>
        //protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        //{
        //    return new[] { new ServiceReplicaListener(context => this.CreateInternalListener(context)) };
        //}

        //private ICommunicationListener CreateInternalListener(ServiceContext context)
        //{
        //    // Partition replica's URL is the node's IP, port, PartitionId, ReplicaId, Guid
        //    EndpointResourceDescription internalEndpoint = context.CodePackageActivationContext.GetEndpoint("QuoteServiceEndpoint");

        //    // Multiple replicas of this service may be hosted on the same machine,
        //    // so this address needs to be unique to the replica which is why we have partition ID + replica ID in the URL.
        //    // HttpListener can listen on multiple addresses on the same port as long as the URL prefix is unique.
        //    // The extra GUID is there for an advanced case where secondary replicas also listen for read-only requests.
        //    // When that's the case, we want to make sure that a new unique address is used when transitioning from primary to secondary
        //    // to force clients to re-resolve the address.
        //    // '+' is used as the address here so that the replica listens on all available hosts (IP, FQDM, localhost, etc.)

        //    string uriPrefix = String.Format(
        //        "{0}://+:{1}/{2}/{3}-{4}/",
        //        internalEndpoint.Protocol,
        //        internalEndpoint.Port,
        //        context.PartitionId,
        //        context.ReplicaOrInstanceId,
        //        Guid.NewGuid());

        //    string nodeIP = FabricRuntime.GetNodeContext().IPAddressOrFQDN;

        //    // The published URL is slightly different from the listening URL prefix.
        //    // The listening URL is given to HttpListener.
        //    // The published URL is the URL that is published to the Service Fabric Naming Service,
        //    // which is used for service discovery. Clients will ask for this address through that discovery service.
        //    // The address that clients get needs to have the actual IP or FQDN of the node in order to connect,
        //    // so we need to replace '+' with the node's IP0000 or FQDN.
        //    string uriPublished = uriPrefix.Replace("+", nodeIP);
        //    return new HttpCommunicationListener(uriPrefix, uriPublished, this.ProcessInternalRequest);
        //}

        //private async Task ProcessInternalRequest(HttpListenerContext context, CancellationToken cancelRequest)
        //{
        //    using (HttpListenerResponse response = context.Response)
        //    {

        //        byte[] outBytes = Encoding.UTF8.GetBytes("Woof");
        //        response.OutputStream.Write(outBytes, 0, outBytes.Length);

        //    }
        //}
    }
}
