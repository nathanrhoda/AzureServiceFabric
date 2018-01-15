using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuoteService.Controllers;
using QuoteService.Model;

namespace QuoteService.Test
{
    [TestClass]
    public class QuoteControllerTests
    {
        [TestMethod]
        public void Get_WhereValidDataIsSupplied_ReturnsQuotationDetailsCapturedSuccessfullyMessage()
        {
            var controller = new QuotesController();
            var quotes = controller.Get();
            Assert.IsNotNull(quotes);
        }

        [TestMethod]
        public void Generate_WhereQuoteRequestSupplied_ReturnsSuccessMessage()
        {
            var request = new QuoteRequest
            {
                Name = "",
                Surname = "",
                Email = "",
                ContactNumber = "",
                CategoryOfGoods = "",
                ContainerSize = "",
                UnitOfGoods = "Amount/SquareMeter/liter/kg",
                CostPriceOfGoods = 0
            };

            var controller = new QuotesController();
            var msg = controller.Generate(request);

            Assert.AreEqual("Success", msg);
        }

        [TestMethod]
        public void Generate_WhereQuoteRequestSuppliedIsNull_ReturnsFailureMessage()
        {
            QuoteRequest request = null;

            var controller = new QuotesController();
            var msg = controller.Generate(request);

            Assert.AreEqual("Failure", msg);
        }
    }
}
