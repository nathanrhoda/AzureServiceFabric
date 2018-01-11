namespace Quota.CommonUtils
{
    public class ServiceFabricConfig
    {
        public static ServiceFabricConfig Initialize(string applicationName, string serviceName, string url, int reverseProxyPort = 0, string baseAddress = "http://localhost")
        {
            return new ServiceFabricConfig
                (
                    applicationName,
                    serviceName, 
                    url, 
                    reverseProxyPort,
                    baseAddress
                );
        }

        private ServiceFabricConfig(string applicationName, string serviceName, string restUrl, int reverseProxyPort = 0, string baseAddress = "http://localhost")
        {
            BaseAddress = baseAddress;
            ApplicationName = applicationName;
            ServiceName = serviceName;
            RestUrl = restUrl;
            ReverseProxyPort = reverseProxyPort;
        }

        public string BaseAddress { get; private set; }

        public string ApplicationName { get; private set; }

        public string ServiceName { get; private set; }

        public string RestUrl { get; private set; }

        public int ReverseProxyPort { get; private set; }

        public string serviceUrl()
        {
            string serviceUrl = ApplicationName + "/" + ServiceName;
            if (ReverseProxyPort == 0)
            {
                return $"{BaseAddress}/{serviceUrl.Replace("fabric:/", "")}" + RestUrl;
            }

            return $"{BaseAddress}:{ReverseProxyPort}/{serviceUrl.Replace("fabric:/", "")}" + RestUrl;
        }
    }
}
