using CurrencyService.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.IO;

namespace CurrencyService.Test
{
    [TestClass]
    public class CurrencyFeedTests
    {        
        [TestMethod]
        public void CurrencyFeed_WhereJsonStringSupplier_ReturnsDeserializedCurrencyFeedObject()
        {
            CurrencyFeed feedObject = null;
            string json = File.ReadAllText(@".\\Mock\\currency.json");
            feedObject = JsonConvert.DeserializeObject<CurrencyFeed>(json);

            Assert.IsNotNull(feedObject);
            Assert.IsTrue(feedObject.Quotes.Count > 0);
        }                
    }
}
