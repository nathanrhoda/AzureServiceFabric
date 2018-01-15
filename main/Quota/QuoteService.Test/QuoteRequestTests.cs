using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuoteService.Model;

namespace QuoteService.Test
{
    [TestClass]
    public class QuoteRequestTests
    {
        [TestMethod]
        public void IsValid_WhereValidRequestSupplied_ReturnsTrue()
        {
            var validRequest = new QuoteRequest
            {
                Name = "Quote",
                Surname = "Request",
                Email = "quote@request.com",
                ContactNumber = "111111"
            };

            Assert.IsTrue(validRequest.IsValid());
        }


        [TestMethod]
        public void IsValid_WhereInvalidRequestSupplied_ReturnsFalse()
        {
            var validRequest = new QuoteRequest
            {
                Email = "quote@request.com"
            };

            Assert.IsFalse(validRequest.IsValid());
        }

        ///TODO Specific rules for email and cellphone validation
    }
}
