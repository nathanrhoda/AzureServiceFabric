using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Services.Communication.AspNetCore;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using System;
using System.Fabric;
using System.IO;

namespace Quota.Common
{
    public static class ServiceReplicaListenerFactory
    {
        public static ServiceReplicaListener CreateListener(Type startupType, IReliableStateManager stateManager, Action<StatefulServiceContext, string> loggingCallback)
        {
            return new ServiceReplicaListener(serviceContext =>
            {
                return new KestrelCommunicationListener(serviceContext, (url, listener) =>
                {
                    loggingCallback(serviceContext, $"Starting Kestrel on {url}");

                    return new WebHostBuilder().UseKestrel()
                                .ConfigureServices(services =>
                                {
                                    services.AddSingleton(serviceContext);
                                    services.AddSingleton(stateManager);
                                })
                                .UseContentRoot(Directory.GetCurrentDirectory())
                                .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.UseUniqueServiceUrl)
                                .UseStartup(startupType)
                                .UseUrls(url)
                                .Build();
                });
            });
        }

        public static ServiceInstanceListener CreateExternalListener(Type startupType, Action<StatelessServiceContext, string> loggingCallback)
        {
            return new ServiceInstanceListener(serviceContext =>
            {
                return new HttpSysCommunicationListener(serviceContext, "ServiceEndpoint", (url, listener) =>
                {
                    loggingCallback(serviceContext, $"Starting HttpSys listener on {url}");

                    return new WebHostBuilder().UseHttpSys()
                                .ConfigureServices(
                                    services => services
                                        .AddSingleton<StatelessServiceContext>(serviceContext))
                                .UseContentRoot(Directory.GetCurrentDirectory())
                                .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.None)
                                .UseStartup(startupType)
                                .UseUrls(url)
                                .Build();
                });
            });
        }
    }
}
