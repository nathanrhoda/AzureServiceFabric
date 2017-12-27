using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuotationService.Controllers;
using QuotationService.Model;

namespace QuotationService.Test
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

        [TestMethod, Ignore("Shifting focus to exposing Currency Rates via Quota.Gateway first to simplify problems that will need solving when quotation rules need to be implemented")]
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
    }
}
