using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quota.Gateway.Model;
using System;
using System.Fabric;
using System.Net.Http;
using System.Threading.Tasks;

namespace Quota.Gateway.Controllers
{
    [Route("Gateway/[controller]")]
    public class QuotationController : Controller
    {
        private readonly ConfigSettings configSettings;
        private readonly StatelessServiceContext serviceContext;
        private readonly HttpClient httpClient;
        private readonly FabricClient fabricClient;

        public QuotationController(ConfigSettings configSettings, StatelessServiceContext serviceContext, HttpClient httpClient, FabricClient fabricClient)
        {
            this.serviceContext = serviceContext;
            this.configSettings = configSettings;
            this.httpClient = httpClient;
            this.fabricClient = fabricClient;
        }


        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                string restUrl = "/api/Currencies";
                return await GetToServiceFabric(this.configSettings.CurrencyServiceName, restUrl, this.configSettings.ReverseProxyPort);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        [HttpGet("CurrencyHealth")]
        public async Task<IActionResult> QuotaHealthCheck()
        {

            try
            {
                string restUrl = "/api/Currencies/Heartbeat";
                return await GetToServiceFabric(this.configSettings.CurrencyServiceName, restUrl, this.configSettings.ReverseProxyPort);               
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] GatewayQuoteRequest request)
        {
            try
            {
                string stringData = JsonConvert.SerializeObject(request);
                var requestContent = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                string restUrlPath = "/api/quotes";
                return await PostToServiceFabric(requestContent, this.configSettings.QuotationServiceName, restUrlPath);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        private async Task<IActionResult> PostToServiceFabric(StringContent requestContent, string serviceName, string restUrl)
        {
            var resolver = ServicePartitionResolver.GetDefault();
            var partitionKey = new ServicePartitionKey(-1);
            var cancellationToken = new System.Threading.CancellationToken();
            var p = await resolver.ResolveAsync(new Uri(serviceContext.CodePackageActivationContext.ApplicationName + "/" + serviceName), partitionKey, cancellationToken);

            JObject addresses = JObject.Parse(p.GetEndpoint().Address);
            string primaryReplicaAddress = (string)addresses["Endpoints"].First;
            var url = primaryReplicaAddress + restUrl;
            var response = httpClient.PostAsync(url, requestContent).Result;
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return this.StatusCode((int)response.StatusCode);
            }

            return Ok(await response.Content.ReadAsStringAsync());
        }

        private async Task<IActionResult> GetToServiceFabric(string serviceName, string restUrl, int reverseProxyPort)
        {
            string serviceUrl = this.serviceContext.CodePackageActivationContext.ApplicationName + "/" + serviceName;

            string proxyUrl =
                  $"http://localhost:{reverseProxyPort}/{serviceUrl.Replace("fabric:/", "")}" + restUrl;
            HttpResponseMessage response = await this.httpClient.GetAsync(proxyUrl);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return this.StatusCode((int)response.StatusCode);
            }

            return this.Ok(await response.Content.ReadAsStringAsync());
        }
    }
}
