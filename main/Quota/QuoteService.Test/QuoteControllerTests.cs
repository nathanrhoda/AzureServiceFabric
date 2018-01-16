using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuoteService.Controllers;
using QuoteService.Model;
using QuoteService.Test.Mock;
using QuoteService.Test.TestObjects;

namespace QuoteService.Test
{
    [TestClass]
    public class QuoteControllerTests
    {
        private MockReliableStateManager GetStateManager()
        {
            return new MockReliableStateManager();
        }

        [TestMethod]
        public void Get_WhereServiceWorkingAsExpected_ReturnsListOfQuotes()
        {
            var quote1 = QuoteBuilder.Quote1;
            var quote2 = QuoteBuilder.Quote2;
            var mockStateManager = GetStateManager();
            
            mockStateManager.Add("quotes", quote1);
            mockStateManager.Add("quotes", quote2);
            var controller = new QuotesController(mockStateManager);
            var quotes = controller.Get();

            Assert.IsTrue(quotes.Any(x => x.Name.Equals(quote1.Name)));
            Assert.IsTrue(quotes.Any(x => x.Name.Equals(quote2.Name)));         
        }

        [TestMethod]
        public void Generate_WhereQuoteRequestSupplied_ReturnsSuccessMessage()
        {
            var mockStateManager = GetStateManager();
            var request = new QuoteRequest
            {
                Name = "",
                Surname = "",
                Email = "",
                ContactNumber = "",               
            };

            var controller = new QuotesController(mockStateManager);
            var msg = controller.Generate(request);

            Assert.AreEqual("Success", msg);
        }

        [TestMethod]
        public void Generate_WhereQuoteRequestSuppliedIsNull_ReturnsFailureMessage()
        {
            var mockStateManager = GetStateManager();
            QuoteRequest request = null;

            var controller = new QuotesController(mockStateManager);
            var msg = controller.Generate(request);

            Assert.AreEqual("Failure", msg);
        }
    }
}
