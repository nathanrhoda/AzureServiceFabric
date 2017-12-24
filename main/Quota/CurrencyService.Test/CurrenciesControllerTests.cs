using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CurrencyService.Controllers;
using Moq;
using CurrencyService.Model;
using System.IO;

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
            var _json = File.ReadAllText(@".\\Mock\\currency.json");
            _mockProvider.Setup(x => x.GetFeed()).Returns(_json);
            _mockController.SetupGet(x => x.Provider).Returns(_mockProvider.Object);
            var controller = _mockController.Object;            
            var response = controller.GetFeed();

            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void GetFeeds_WhereInvalidJsonReceived_ReturnsPopulatedCurrencyFeedObject()
        {
            _mockProvider.Setup(x => x.GetFeed()).Returns("");
            _mockController.SetupGet(x => x.Provider).Returns(_mockProvider.Object);
            var controller = _mockController.Object;
            var response = controller.GetFeed();

            Assert.IsNull(response);
        }
    }
}
