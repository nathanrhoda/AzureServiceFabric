using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using Quota.CommonUtils;

namespace Quota.Integration.Test
{
    [TestClass]
    public class QuotationControllerTests
    {
        [TestMethod]
        public void GetAll_WhereQuoationsExists_ReturnsMoreThanZero()
        {
            var url = ConfigurationManager.AppSettings["quotesGatewayUrl"];
            var quotes = APIUtilities.Get(url);

            Assert.IsTrue(quotes.Length > 0);
        }
    }
}
