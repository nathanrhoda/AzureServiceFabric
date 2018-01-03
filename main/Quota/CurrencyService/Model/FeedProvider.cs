using Newtonsoft.Json;
using Quota.Common;

namespace CurrencyService.Model
{
    public class FeedProvider : IFeedProvider
    {
        public CurrencyFeed GetFeed()
        {
            var feedString = APIUtilities.Get("http://www.apilayer.net/api/live?access_key=<Enter Code Here>");
            return JsonConvert.DeserializeObject<CurrencyFeed>(feedString);                       
        }
    }
}
