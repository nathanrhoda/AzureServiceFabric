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
                string currencyServiceUrl = this.serviceContext.CodePackageActivationContext.ApplicationName + "/" + this.configSettings.CurrencyServiceName;

                string proxyUrl =
                      $"http://localhost:{this.configSettings.ReverseProxyPort}/{currencyServiceUrl.Replace("fabric:/", "")}/api/Currencies";
                HttpResponseMessage response = await this.httpClient.GetAsync(proxyUrl);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return this.StatusCode((int)response.StatusCode);
                }

                return this.Ok(await response.Content.ReadAsStringAsync());
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
                string currencyServiceUrl = this.serviceContext.CodePackageActivationContext.ApplicationName + "/" + this.configSettings.CurrencyServiceName;

                string proxyUrl =
                      $"http://localhost:{this.configSettings.ReverseProxyPort}/{currencyServiceUrl.Replace("fabric:/", "")}/api/Currencies/Heartbeat";
                HttpResponseMessage response = await this.httpClient.GetAsync(proxyUrl);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return this.StatusCode((int)response.StatusCode);
                }

                return this.Ok(await response.Content.ReadAsStringAsync());
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

                HttpRequestMessage msg = new HttpRequestMessage();

                var resolver = ServicePartitionResolver.GetDefault();
                var partitionKey = new ServicePartitionKey(-1);
                var cancellationToken = new System.Threading.CancellationToken();
                var p = await resolver.ResolveAsync(new Uri(serviceContext.CodePackageActivationContext.ApplicationName + "/" + this.configSettings.QuotationServiceName), partitionKey, cancellationToken);

                JObject addresses = JObject.Parse(p.GetEndpoint().Address);
                string primaryReplicaAddress = (string)addresses["Endpoints"].First;
                var url = primaryReplicaAddress + "/api/quotes";
                var response = httpClient.PostAsync(url, requestContent).Result;

                return this.Ok(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {

                throw e;
            }

        }

    }
}
