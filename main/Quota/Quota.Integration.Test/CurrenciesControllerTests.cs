using CurrencyService.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quota.Common;
using System.Configuration;

namespace Quota.Integration.Test
{
    [TestClass]
    public class CurrenciesControllerTests
    {
        [TestMethod]
        public void GetFeed_WhereServiceIsAvailable_ReturnsValues()
        {
            var url = ConfigurationManager.AppSettings["currenciesUrl"];
            var feed = APIUtilities.Get<CurrencyFeed>(url);
            Assert.IsNotNull(feed);
        }
    }
}
