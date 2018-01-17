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
        public void Generate_WhereQuoteRequestIsValid_ReturnsGuid()
        {
            var mockStateManager = GetStateManager();
            var request = new QuoteRequest
            {
                Name = "Woof",
                Surname = "bark",
                Email = "woof.bark@dog.com",
                ContactNumber = "k9",               
            };

            var controller = new QuotesController(mockStateManager);
            var msg = controller.Generate(request);

            Assert.AreNotEqual("Failure", msg);
        }

        [TestMethod]
        public void Generate_WhereQuoteRequestIsNull_ReturnsFailureMessage()
        {
            var mockStateManager = GetStateManager();
            QuoteRequest request = null;

            var controller = new QuotesController(mockStateManager);
            var msg = controller.Generate(request);

            Assert.AreEqual("Failure", msg);
        }

        [TestMethod]
        public void Generate_WhereQuoteRequestIsInvalid_ReturnsFailureMessage()
        {
            var mockStateManager = GetStateManager();
            var request = new QuoteRequest
            {
                Name = "Woof",
                Surname = "bark",
                Email = "",
                ContactNumber = "k9",
            };

            var controller = new QuotesController(mockStateManager);
            var msg = controller.Generate(request);

            Assert.AreEqual("Failure", msg);
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
