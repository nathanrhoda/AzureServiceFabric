using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quota.Common;
using System.Configuration;

namespace Quota.Integration.Test
{
    [TestClass]
    public class ValuesControllerTests
    {
        [TestMethod]
        public void Get_WhereValuesGatewayIsAvailable_ReturnsValues()
        {
            var valueGatewayUrl = ConfigurationManager.AppSettings["valueGatewayUrl"];
            var url = ConfigurationManager.AppSettings["currenciesUrl"];
            var values = APIUtilities.Get<string[]>(valueGatewayUrl);
            Assert.IsNotNull(values);
        }
    }
}
