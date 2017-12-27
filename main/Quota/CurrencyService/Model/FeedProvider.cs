using Quota.Common;

namespace CurrencyService.Model
{
    public class FeedProvider : IFeedProvider
    {
        public CurrencyFeed GetFeed()
        {
            var feed = APIUtilities.Get<CurrencyFeed>(http://www.apilayer.net/api/live?access_key=<ENTER YOUR ACCESS KEY>");
            return feed;
           
        }
    }
}
