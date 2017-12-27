using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using CurrencyService.Model;
using Newtonsoft.Json;
using System.Configuration;

namespace Quota.Integration.Test
{
    [TestClass]
    public class CurrenciesControllerTests
    {
        [TestMethod]
        public void GetFeed_WhereServiceIsAvailable_ReturnsValues()
        {
            var url = ConfigurationManager.AppSettings["currenciesUrl"];
            var client = new HttpClient(); 

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = client.SendAsync(request).Result)
            {         
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

                var feed =  response.Content.ReadAsStringAsync().Result;
                var currencyFeed = JsonConvert.DeserializeObject<CurrencyFeed>(feed);
                Assert.IsNotNull(currencyFeed);
            }
        }
    }
}
