using Quota.Common;

namespace CurrencyService.Model
{
    public class FeedProvider : IFeedProvider
    {
        public CurrencyFeed GetFeed()
        {
            var feed = APIUtilities.Get<CurrencyFeed>("http://www.apilayer.net/api/live?access_key=4633a6c0baf8136ca5b9d56a09cce755");
            return feed;
           
        }
    }
}
