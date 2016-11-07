using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Selenium.WebDriver.Proxy.DTO;

namespace Selenium.WebDriver.Proxy.Proxies
{
    public class SessionProxy
    {
        public string EndpointUrl { get; set; }

        public SessionProxy(string apiEndpointUrl)
        {
            EndpointUrl = apiEndpointUrl;
        }

        public SessionDTO GetSession()
        {
            var client = new RestClient(EndpointUrl);
            var request = new RestRequest("session", Method.GET) { RequestFormat = DataFormat.Json };
            var response = client.Execute<SessionDTO>(request);
            return response.Data;
        }
    }
}
