using RestSharp;
using WebDriverProxy.DTO;

namespace WebDriverProxy.Proxies
{
    public class StatusProxy
    {
        public string EndpointUrl { get; set; }

        public StatusProxy(string apiEndpointUrl)
        {
            EndpointUrl = apiEndpointUrl;
        }

        public StatusDto GetStatus()
        {
            var client = new RestClient(EndpointUrl);
            var request = new RestRequest("status", Method.GET) { RequestFormat = DataFormat.Json };
            var response = client.Execute<StatusDto>(request);
            return response.Data;
        }
    }
}
