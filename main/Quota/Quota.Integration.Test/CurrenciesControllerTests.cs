using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;

namespace Quota.Integration.Test
{
    [TestClass]
    public class CurrenciesControllerTests
    {
        [TestMethod]
        public void GetFeed_WhereServiceIsAvailable_ReturnsValues()
        {            
            var client = new HttpClient(); // no HttpServer

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost:9058/api/currencies"),
                Method = HttpMethod.Get
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = client.SendAsync(request).Result)
            {         
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);                
            }
        }
    }
}
