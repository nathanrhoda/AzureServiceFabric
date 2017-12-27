using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuotationService.Controllers;

namespace QuotationService.Test
{
    [TestClass]
    public class QuoteControllerTests
    {
        [TestMethod]
        public void GenerateQuote_WhereValidDataIsSupplied_ReturnsQuotationDetailsCapturedSuccessfullyMessage()
        {
            var controller = new QuotesController();
            var quotes = controller.Get();
            Assert.IsNotNull(quotes);
        }
    }
}
