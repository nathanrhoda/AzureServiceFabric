using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Quota.CommonUtils
{
    public static class ServiceFabricAPIUtility
    {
        public static async Task<HttpResponseMessage> Post(StringContent requestContent, ServiceFabricConfig config)
        {
            HttpClient httpClient = new HttpClient();
            var resolver = ServicePartitionResolver.GetDefault();
            var partitionKey = new ServicePartitionKey(-1);
            var cancellationToken = new System.Threading.CancellationToken();
            var p = await resolver.ResolveAsync(new Uri(config.ApplicationName + "/" + config.ServiceName), partitionKey, cancellationToken);

            JObject addresses = JObject.Parse(p.GetEndpoint().Address);
            string primaryReplicaAddress = (string)addresses["Endpoints"].First;
            var url = primaryReplicaAddress + config.RestUrl;
            return httpClient.PostAsync(url, requestContent).Result;
        }

        public static async Task<HttpResponseMessage> Get(ServiceFabricConfig config)
        {
            HttpClient httpClient = new HttpClient();
            var resolver = ServicePartitionResolver.GetDefault();
            var partitionKey = new ServicePartitionKey(-1);
            var cancellationToken = new System.Threading.CancellationToken();
            var p = await resolver.ResolveAsync(new Uri(config.ApplicationName + "/" + config.ServiceName), partitionKey, cancellationToken);

            JObject addresses = JObject.Parse(p.GetEndpoint().Address);
            string primaryReplicaAddress = (string)addresses["Endpoints"].First;
            var url = primaryReplicaAddress + config.RestUrl;
            return httpClient.GetAsync(url).Result;
        }

        public static async Task<HttpResponseMessage> GetReverseProxyAsync(ServiceFabricConfig config)
        {
            HttpClient httpClient = new HttpClient();

            string serviceUrl = config.ApplicationName + "/" + config.ServiceName;
            string proxyUrl = $"{config.BaseAddress}:{config.ReverseProxyPort}/{serviceUrl.Replace("fabric:/", "")}" + config.RestUrl;
            return httpClient.GetAsync(proxyUrl).Result;
        }
    }
}
