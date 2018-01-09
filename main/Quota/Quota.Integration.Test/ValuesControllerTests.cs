using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quota.CommonUtils;
using System.Configuration;

namespace Quota.Integration.Test
{
    [TestClass]
    public class ValuesControllerTests
    {
        [TestMethod]
        public void Get_WhereValuesGatewayIsAvailable_ReturnsValues()
        {            
            var url = ConfigurationManager.AppSettings["valueGatewayUrl"];
            var values = APIUtilities.Get(url);
            Assert.IsTrue(values.Length  > 0);
        }
    }
}
