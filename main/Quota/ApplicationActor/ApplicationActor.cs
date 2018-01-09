using ApplicationActor.Interfaces;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationActor
{
    [StatePersistence(StatePersistence.Persisted)]
    internal class ApplicationActor : Actor, IApplicationActor
    {

        public ApplicationActor(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        public async Task<string> GetHelloWorldAsync()
        {
            return await Task.FromResult("Hello from my reliable actor!");
        }
    }
}
