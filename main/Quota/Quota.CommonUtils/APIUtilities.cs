using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Quota.CommonUtils
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

        public static string Post<T>(T request, string url)
        {
            string stringData = JsonConvert.SerializeObject(request);
            var requestContent = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            var client = new HttpClient();

            using (var response = client.PostAsync(url, requestContent).Result)                
            {
                var stringResponse = response.Content.ReadAsStringAsync().Result;
                return stringResponse;
            }
        }
    }
}
