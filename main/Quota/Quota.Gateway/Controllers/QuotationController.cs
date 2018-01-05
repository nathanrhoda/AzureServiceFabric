using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quota.Common;
using Quota.Gateway.Model;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Fabric.Description;
using System.IO;
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
        public async Task<IActionResult> PostAsync(GatewayQuoteRequest request)
        {

            try
            {
                var resolver = ServicePartitionResolver.GetDefault();
                var partitionKey = new ServicePartitionKey(-1);
                var cancellationToken = new System.Threading.CancellationToken();
                var p = await resolver.ResolveAsync(new Uri("fabric:/Quota/QuotationService"), partitionKey, cancellationToken);
                var reader = new StreamReader(this.Request.Body);
                var body = reader.ReadToEnd();
                var content = new StringContent(body, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

                var http = new HttpClient();
                JObject addresses = JObject.Parse(p.GetEndpoint().Address);
                string primaryReplicaAddress = (string)addresses["Endpoints"].First;
                var url = primaryReplicaAddress + "/api/quotes";
                var response = await http.PostAsync(url, content);
         
                //EndpointResourceDescription inputEndpoint = serviceContext.CodePackageActivationContext.GetEndpoint("ServiceEndpoint");
                //string uriPrefix = String.Format("{0}://+:{1}/quotationservice/", inputEndpoint.Protocol, inputEndpoint.Port);

                //string stringData = JsonConvert.SerializeObject(request);
                //string uriPublished = uriPrefix.Replace("+", FabricRuntime.GetNodeContext().IPAddressOrFQDN);


                //var requestContent = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

                //string quotationServiceUrl = this.serviceContext.CodePackageActivationContext.ApplicationName + "/" + this.configSettings.QuotationServiceName;

                //string proxyUrl =
                //      $"http://localhost:{this.configSettings.ReverseProxyPort}/{quotationServiceUrl.Replace("fabric:/", "")}/api/quotes";
                //HttpResponseMessage response = this.httpClient.PostAsync(proxyUrl, requestContent).Result;

                //if (response.StatusCode != System.Net.HttpStatusCode.OK)
                //{
                //    return this.StatusCode((int)response.StatusCode);
                //}

                return this.Ok(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {

                throw e;
            }

        }

    }
}
