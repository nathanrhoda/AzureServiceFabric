using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Quota.Common
{
    public class APIUtilities
    {
        public static string Get(string url)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = client.GetAsync(request.RequestUri).Result)
            {
                var stringResponse = response.Content.ReadAsStringAsync().Result;
                return stringResponse;                
            }
        }
    }
}
