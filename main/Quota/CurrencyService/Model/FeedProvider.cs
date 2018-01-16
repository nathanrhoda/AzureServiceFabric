using Newtonsoft.Json;
using Quota.CommonUtils;

namespace CurrencyService.Model
{
    public class FeedProvider : IFeedProvider
    {
        public CurrencyFeed GetFeed()
        {
            var feedString = APIUtilities.Get("http://www.apilayer.net/api/live?access_key=4633a6c0baf8136ca5b9d56a09cce755");
            return JsonConvert.DeserializeObject<CurrencyFeed>(feedString);                       
        }
    }
}
