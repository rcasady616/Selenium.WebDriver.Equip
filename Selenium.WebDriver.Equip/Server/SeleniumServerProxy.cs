using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using Selenium.WebDriver.Equip.Settings;
using System.ComponentModel;
using Equip.Java;

namespace Selenium.WebDriver.Equip.Server
{
    /// <summary>
    /// 
    /// </summary>
    public class SeleniumServerProxy : SeleniumServerProxyBase, ISeleniumServer
    {
        public Process ServerProcess;
        private static string commandUrl = "http://{0}:{1}/selenium-server/driver/?cmd=";
        public string CommandUrl
        {
            get
            {
                if (string.IsNullOrEmpty(HostName))
                    throw new ArgumentNullException("SeleniumServer HostName");
                if (string.IsNullOrEmpty(Port))
                    throw new ArgumentNullException("SeleniumServer Port");
                return string.Format(commandUrl, HostName, Port);
            }
        }
        private static string registerUrl = "http://{0}:{1}/grid/register";
        public string RegisterUrl
        {
            get
            {
                if (string.IsNullOrEmpty(HostName))
                    throw new ArgumentNullException("SeleniumServer HostName");
                if (string.IsNullOrEmpty(Port))
                    throw new ArgumentNullException("SeleniumServer Port");
                return string.Format(registerUrl, HostName, Port);
            }
        }

        public SeleniumServerProxy()
        {

        }

        public SeleniumServerProxy(SeleniumServerSettings settings)
            : base(settings)
        {
        }

        public void Start(string configurationArgs = "")
        {
            configurationArgs = string.Format("-port {0} {1}", Port, configurationArgs);
            //if (string.IsNullOrEmpty(Java.GetJreVersion()))
            //    throw new Exception("Java JRE not installed, go to https://www.java.com/en/download/");
            ServerProcess = Process.Start("java", string.Format("-jar \"{0}\" {1}", StandAlonePath, configurationArgs));
            if (!WaitUntilRunning())
                throw new Exception("Server didnt start as expected");
        }

        public bool Stop()
        {
            ServerProcess.Kill();
            return true;
            //todo fix none of the url's stop selenium server are routing correctly in selenium server3.0
            HttpWebResponse response;
            try
            {
                response = SeleniumCommand("shutDownSeleniumServer");
            }
            catch (Exception)
            {
                return false;
            }
            GetResponseAsString(response).Contains("OKOK");
            return !WaitUntilRunning();
        }

        //public StatusDto GetStatus()
        //{
        //    var client = new StatusProxy(string.Format("http://{0}:{1}/wd/hub/", HostName, Port));
        //    return client.GetStatus();
        //}

        //public SessionDTO GetSession()
        //{
        //    var client = new SessionProxy(string.Format("http://{0}:{1}/wd/hub/", HostName, Port));
        //    return client.GetSession();
        //}

        public void GetConfig()
        {

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
            while (!IsSeleniumServerRunning() && ctr < 20)
            {
                Thread.Sleep(500);
                ctr++;
            }
            return IsSeleniumServerRunning();
        }

        public bool IsSeleniumServerRunning()
        {
            try
            {
                return (SeleniumCommand("").StatusCode == HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private HttpWebResponse SeleniumCommand(string command)
        {
            string urlString = string.Format(CommandUrl + command);
            return GetHttpWebResponse(urlString);
        }

    }
}
