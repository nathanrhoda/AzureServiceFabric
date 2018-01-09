﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using QuotationService.Model;
using Quota.CommonUtils;

namespace Quota.Integration.Test
{
    [TestClass]
    public class QuotesControllerTests
    {
        [TestMethod]
        public void PostGenerate_WhereServiceIsAvailable_ReturnsValues()
        {
            var request = new QuoteRequest
            {
                Name="MONO"
            };

            var url = ConfigurationManager.AppSettings["quotesGatewayUrl"];
            var feed = APIUtilities.Post(request, url);
            Assert.IsTrue(feed.Length > 0);
        }
    }
}
