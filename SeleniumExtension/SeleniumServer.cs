using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using Selenium;

namespace SeleniumExtension
{
    public class SeleniumServer
    {
        public event SettingsLoadedEventHandler SettingsLoaded;

        public static void Start(string seleniumServerFilePath)
        {
            if (!File.Exists(seleniumServerFilePath))
                throw new Exception(string.Format("Could not find selenium-server, file name: {0}", seleniumServerFilePath));
            Process.Start("Java.exe", string.Format("-jar \"{0}\" ", seleniumServerFilePath));
            if (!WaitUntilSeleniumServerRunning())
                throw new Exception("");
        }

        public static void Start()
        {
            //var se = new SeleniumSettings();

            //se.SettingChanging += new SettingChangingEventHandler(MyCustomSettings_SettingChanging);
            Start(@"C:\Users\rcasady\Downloads\selenium-server-standalone-2.33.0.jar");//se.SeleniumServerStandAlonePath);
            //if (!isSeleniumServerRunning())
            //{
            //    var se = new SeleniumSettings();
            //    var ss = new DefaultSelenium(se.SeleniumHost, se.SeleniumPort, se.BrowserName, se.BrowserUrl);
            //    ss.Start();
            //}
        }

        public static bool Stop()
        {
            HttpWebResponse response = SeleniumCommand("shutDownSeleniumServer");
            return GetResponseAsString(response).Contains("OKOK");
        }

        public static bool WaitUntilSeleniumServerRunning()
        {
            int ctr = 0;
            while (!isSeleniumServerRunning() && ctr < 10)
            {
                Thread.Sleep(2000);
                ctr++;
            }
            return isSeleniumServerRunning();
        }

        public static bool isSeleniumServerRunning()
        {
            try
            {
                return (SeleniumCommand("testComplete").StatusCode == HttpStatusCode.OK);
            }
            catch (NullReferenceException ex)
            {
                return false;
            }
        }

        private static HttpWebResponse SeleniumCommand(string command)
        {
            var se = new SeleniumSettings();
            string urlString = string.Format("http://{0}:{1}/selenium-server/driver/?cmd={2}", se.SeleniumHost, se.SeleniumPort, command);
            try
            {
                var commandUrl = new Uri(urlString);
                var request = (HttpWebRequest)WebRequest.Create(commandUrl);
                return (HttpWebResponse)request.GetResponse();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
            return null;
        }

        private static string GetResponseAsString(HttpWebResponse response)
        {
            using (Stream dataStream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(dataStream))
                return reader.ReadToEnd();

        }
    }
}
