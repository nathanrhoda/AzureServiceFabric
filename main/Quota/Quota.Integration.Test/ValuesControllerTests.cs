﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Quota.Integration.Test
{
    [TestClass]
    public class ValuesControllerTests
    {

        [TestMethod]
        public void Get_WhereServiceIsAvailable_ReturnsValues()
        {
            var valueGatewayUrl = ConfigurationManager.AppSettings["valueGatewayUrl"];
            var client = new HttpClient();

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(valueGatewayUrl),
                Method = HttpMethod.Get
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = client.SendAsync(request).Result)
            {
                var actualResult = response.Content.ReadAsAsync<string[]>().Result;
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.IsTrue(actualResult.Length > 0);
            }
        }
    }
}
