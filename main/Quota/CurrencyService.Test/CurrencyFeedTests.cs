using System;
using System.IO;

using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CurrencyService.Model;

namespace CurrencyService.Test
{
    [TestClass]
    public class CurrencyFeedTests
    {        
        [TestMethod]
        public void Convert_WhereValidJSonIsReceived_ReturnsConvertedAndPopulatedCurrencyFeedObject()
        {
            CurrencyFeed feedObject = null;
            string json = File.ReadAllText(@".\\Mock\\currency.json");
            feedObject = JsonConvert.DeserializeObject<CurrencyFeed>(json);

            Assert.IsNotNull(feedObject);
            Assert.IsTrue(feedObject.Quotes.Count > 0);
        }
    }
}
