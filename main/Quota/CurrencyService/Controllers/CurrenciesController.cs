using CurrencyService.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CurrencyService.Controllers
{
    [Route("api/[controller]")]
    public class CurrenciesController : Controller
    {

        private IFeedProvider _provider;
        public virtual IFeedProvider Provider
        {
            get
            {
                if (_provider == null)
                {
                    _provider = new FeedProvider();
                }

                return _provider;
            }
        }

        [HttpGet]
        public IActionResult GetFeed()
        {            
            var feed = Provider.GetFeed();            

            return new JsonResult(feed);
        }

        [HttpGet("HeartBeat")]
        public IActionResult Heartbeat()
        {
            return new JsonResult("Im alive");
        }
    }
}
