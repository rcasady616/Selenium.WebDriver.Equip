using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;

namespace SeleniumExtension.Server
{
    /// <summary>
    /// 
    /// </summary>
    public class SeleniumServer
    {
        public event SettingsLoadedEventHandler SettingsLoaded;

        public static void Start(string seleniumServerFilePath)
        {
            if (!File.Exists(seleniumServerFilePath))
                throw new FileNotFoundException(string.Format("Could not find selenium-server, file name: {0}", seleniumServerFilePath));
            Process.Start("java", string.Format("-jar \"{0}\"", seleniumServerFilePath));
            if (!WaitUntilSeleniumServerRunning())
                throw new Exception("Server didnt start as expected");
        }

        public static void Start()
        {
            //var se = new SeleniumSettings();

            //se.SettingChanging += new SettingChangingEventHandler(MyCustomSettings_SettingChanging);
            Start(@"C:\Users\rcasady\Downloads\selenium-server-standalone-2.42.2.jar");//se.SeleniumServerStandAlonePath);
            //if (!isSeleniumServerRunning())
            //{
            //    var se = new SeleniumSettings();
            //    var ss = new DefaultSelenium(se.SeleniumHost, se.SeleniumPort, se.BrowserName, se.BrowserUrl);
            //    ss.Start();
            //}
        }

        public static bool Stop()
        {
            HttpWebResponse response;
            try
            {
                response = SeleniumCommand("shutDownSeleniumServer");
            }
            catch (Exception)
            {
                return false;
            }
            return GetResponseAsString(response).Contains("OKOK");
        }

        public static bool WaitUntilSeleniumServerRunning()
        {
            int ctr = 0;
            while (!IsSeleniumServerRunning() && ctr < 10)
            {
                Thread.Sleep(2000);
                ctr++;
            }
            return IsSeleniumServerRunning();
        }

        public static bool IsSeleniumServerRunning()
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

        private static HttpWebResponse SeleniumCommand(string command)
        {
            var se = new SeleniumSettings();
            string urlString = string.Format("http://{0}:{1}/selenium-server/driver/?cmd={2}", se.SeleniumHost, se.SeleniumPort, command);
            HttpWebResponse response;
            try
            {
                var commandUrl = new Uri(urlString);
                var request = (HttpWebRequest)WebRequest.Create(commandUrl);
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception e)
            {
                throw;
            }
            return response;
        }

        private static string GetResponseAsString(HttpWebResponse response)
        {
            using (var dataStream = response.GetResponseStream())
            {
                using (var reader = new StreamReader(dataStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
