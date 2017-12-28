using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CurrencyService.Controllers;
using Moq;
using CurrencyService.Model;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyService.Test
{
    [TestClass]
    public class CurrenciesControllerTests
    {
        Mock<CurrenciesController> _mockController = new Mock<CurrenciesController>();
        Mock<IFeedProvider> _mockProvider = new Mock<IFeedProvider>();

        [TestMethod]
        public void GetFeeds_WhereValidJsonReceived_ReturnsPopulatedCurrencyFeedObject()
        {
            var json = File.ReadAllText(@".\\Mock\\currency.json");
            var feedObject = JsonConvert.DeserializeObject<CurrencyFeed>(json);
            _mockProvider.Setup(x => x.GetFeed()).Returns(feedObject);

            _mockController.SetupGet(x => x.Provider).Returns(_mockProvider.Object);
            var controller = _mockController.Object;
            var response = controller.GetFeed() as JsonResult;
            var feed = (CurrencyFeed)response.Value;
            
            Assert.IsNotNull(feed);
        }

        [TestMethod]
        public void GetFeeds_WhereInvalidJsonReceived_ReturnsPopulatedCurrencyFeedObject()
        {
            CurrencyFeed nullFeed = null;
            _mockProvider.Setup(x => x.GetFeed()).Returns(nullFeed);
            _mockController.SetupGet(x => x.Provider).Returns(_mockProvider.Object);
            var controller = _mockController.Object;
            var response = controller.GetFeed() as JsonResult;
            var feed = (CurrencyFeed)response.Value;
            
            Assert.IsNull(feed);
        }
    }
}
