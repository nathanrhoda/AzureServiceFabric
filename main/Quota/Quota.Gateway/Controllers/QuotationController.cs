using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Quota.CommonUtils;
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
                string applicationName = serviceContext.CodePackageActivationContext.ApplicationName;
                string serviceName = configSettings.CurrencyServiceName;
                string restUrl = "/api/Currencies";
                int reverseProxyPort = configSettings.ReverseProxyPort;
                var config = ServiceFabricConfig.Initialize(applicationName, serviceName, restUrl, reverseProxyPort);
              
                HttpResponseMessage response = ServiceFabricAPIUtility.GetReverseProxyAsync(config).Result;
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
                string applicationName = serviceContext.CodePackageActivationContext.ApplicationName;
                string serviceName = configSettings.CurrencyServiceName;
                string restUrl = "/api/Currencies/Heartbeat";
                int reverseProxyPort = configSettings.ReverseProxyPort;
                var config = ServiceFabricConfig.Initialize(applicationName, serviceName, restUrl, reverseProxyPort);

                HttpResponseMessage response = ServiceFabricAPIUtility.GetReverseProxyAsync(config).Result;
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
                string applicationName = serviceContext.CodePackageActivationContext.ApplicationName;
                string serviceName = this.configSettings.QuotationServiceName;                
                string restUrl = "/api/quotes";
                var config = ServiceFabricConfig.Initialize(applicationName, serviceName, restUrl);

                HttpResponseMessage response = ServiceFabricAPIUtility.Post(requestContent, config).Result;
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return this.StatusCode((int)response.StatusCode);
                }

                return Ok(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
