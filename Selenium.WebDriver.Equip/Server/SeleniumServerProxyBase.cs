using System;
using System.IO;
using System.Net;
using Selenium.WebDriver.Equip.Settings;

namespace Selenium.WebDriver.Equip.Server
{
    public class SeleniumServerProxyBase
    {
        private string standAlonePath;
        public string HostName { get; set; }
        public string Port { get; set; }
        public string StandAlonePath
        {
            get
            {
                if (string.IsNullOrEmpty(standAlonePath))
                    throw new ArgumentNullException("SeleniumServer StandAlonePath");
                if (!File.Exists(standAlonePath))
                    throw new FileNotFoundException(string.Format("Could not find selenium-server, file name: {0}", standAlonePath));

                return standAlonePath;
            }
            set { standAlonePath = value; }
        }

        public SeleniumServerProxyBase()
        { }

        public SeleniumServerProxyBase(SeleniumServerSettings settings)
        {
            HostName = settings.HostName;
            Port = settings.Port;
            StandAlonePath = settings.StandAlonePath;
        }

        protected string GetResponseAsString(HttpWebResponse response)
        {
            using (var dataStream = response.GetResponseStream())
            {
                using (var reader = new StreamReader(dataStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        protected static HttpWebResponse GetHttpWebResponse(string urlString)
        {
            HttpWebResponse response;
            var commandUri = new Uri(urlString);
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(commandUri);
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception)
            {
                throw;
            }
           
            return response;
        }
    }
}