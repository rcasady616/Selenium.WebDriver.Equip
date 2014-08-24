using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using SeleniumExtension.Settings;
using WebDriverProxy.DTO;

namespace SeleniumExtension.Server
{
    public class SeleniumServerHubProxy : SeleniumServerProxyBase, ISeleniumServer
    {
        public string ShutdownUrl = "http://{0}:{1}/lifecycle-manager?action=shutdown";
        private static string consoleUrl = "http://{0}:{1}/grid/console";

        public SeleniumServerHubProxy(SeleniumServerSettings settings)
            : base(settings)
        {
        }

        public void Start(string configurationArgs = "")
        {
            configurationArgs = string.Format("-port {0} -role hub {1}", Port, configurationArgs);
            Process.Start("java", string.Format("-jar \"{0}\" {1}", StandAlonePath, configurationArgs));
            if (!WaitUntilRunning())
                throw new Exception("Server didnt start as expected");
        }

        public bool Stop()
        {
            GetHttpWebResponse(string.Format(ShutdownUrl, HostName, Port));
            return !WaitUntilRunning();
        }

        public bool WaitUntilStopped()
        {
            int ctr = 0;
            while (IsSeleniumServerRunning() && ctr < 10)
            {
                Thread.Sleep(500);
                ctr++;
            }
            return !IsSeleniumServerRunning();
        }

        public bool WaitUntilRunning()
        {
            int ctr = 0;
            while (!IsSeleniumServerRunning() && ctr < 10)
            {
                Thread.Sleep(500);
                ctr++;
            }
            return IsSeleniumServerRunning();
        }

        public bool IsSeleniumServerRunning()
        {
            var urlString = string.Format(consoleUrl, HostName, Port);
            try
            {
                return (GetHttpWebResponse(urlString).StatusCode == HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
