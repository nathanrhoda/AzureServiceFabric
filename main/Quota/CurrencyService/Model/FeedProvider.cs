using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CurrencyService.Model
{
    public class FeedProvider : IFeedProvider
    {
        public CurrencyFeed GetFeed()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://www.apilayer.net/api/live?access_key=<ENTER YOUR ACCESS KEY>"),
                Method = HttpMethod.Get
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = client.GetAsync(request.RequestUri).Result)
            {
                var stringResponse = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<CurrencyFeed>(stringResponse);
            }          
        }
    }
}
