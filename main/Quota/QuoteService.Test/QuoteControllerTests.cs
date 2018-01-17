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
        public void Post_WhereQuoteRequestIsValid_ReturnsGuid()
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
            var msg = controller.Post(request);

            Assert.AreNotEqual("Invalid Request", msg);
        }

        [TestMethod]
        public void Post_WhereQuoteRequestIsNull_ReturnsFailureMessage()
        {
            var mockStateManager = GetStateManager();
            QuoteRequest request = null;

            var controller = new QuotesController(mockStateManager);
            var msg = controller.Post(request);

            Assert.AreEqual("Invalid Request", msg);
        }

        [TestMethod]
        public void Post_WhereQuoteRequestIsInvalid_ReturnsFailureMessage()
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
            var msg = controller.Post(request);

            Assert.AreEqual("Invalid Request", msg);
        }

        [TestMethod]
        public void Delete_WhereQuoteGuidExists_ReturnsSuccessMessage()
        {
            var quote1 = QuoteBuilder.Quote1;
            var mockStateManager = GetStateManager();

            mockStateManager.Add("quotes", quote1);
            var controller = new QuotesController(mockStateManager);
            var msg = controller.Delete(quote1.Id);

            Assert.AreEqual("Success", msg);
        }

        [TestMethod]
        public void Delete_WhereQuoteGuidDoesNotExists_ReturnsSuccessMessage()
        {
            var guid = Guid.NewGuid();
            var mockStateManager = GetStateManager();

            var controller = new QuotesController(mockStateManager);
            var msg = controller.Delete(guid);

            Assert.IsNull(null);
        }

        [TestMethod]
        public void Put_WhereQuoteGuidIsReplaced_ReturnsReplacedQuote()
        {
            var quote1 = QuoteBuilder.Quote1;
            var mockStateManager = GetStateManager();
            mockStateManager.Add("quotes", quote1);

            var controller = new QuotesController(mockStateManager);

            var quote = controller.Get(quote1.Id);
            Assert.AreEqual("Quote1", quote.Name);
            
            var request = new QuoteRequest
            {                
                Name = "Updated",
                Surname = "Updated",
                Email = "Updated",
                ContactNumber = "Updated",
            };
                                              
            var msg = controller.Put(quote1.Id, request);

            Assert.AreEqual("Success", msg);

            var returnedQuote = controller.Get(quote1.Id);
            Assert.AreEqual(request.Name, returnedQuote.Name);
        }


    }
}
