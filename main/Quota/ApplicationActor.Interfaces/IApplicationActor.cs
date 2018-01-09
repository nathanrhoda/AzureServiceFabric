using Microsoft.ServiceFabric.Actors;
using System.Threading.Tasks;

namespace ApplicationActor.Interfaces
{
    public interface IApplicationActor : IActor
    {
        Task<string> GetHelloWorldAsync();
    }
}
