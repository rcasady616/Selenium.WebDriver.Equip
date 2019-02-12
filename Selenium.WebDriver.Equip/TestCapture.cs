using OpenQA.Selenium;
using System;
using System.IO;
using System.Reflection;

namespace Selenium.WebDriver.Equip
{
    public class TestCapture
    {
        private IWebDriver _browser = null;
        private string fileName;
        public TestCapture(IWebDriver iWebDriver, string type = "Failed")
        {
            fileName = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\{DateTime.Now.Ticks}.{type}";
            _browser = iWebDriver;
        }

        public void CaptureWebPage()
        {
            WebDriverLogsLogs();
            PageSource();
            ScreenShot();
        }

        public void PageSource()
        {
            string htmlFile = $"{fileName}.html";
            using (var sw = new StreamWriter(htmlFile, false))
                sw.Write(_browser.PageSource);
        }

        public void ScreenShot()
        {
            _browser.TakeScreenShot(fileName + ".jpeg", ScreenshotImageFormat.Jpeg);
        }

        public void WebDriverLogsLogs()
        {
            foreach (var log in _browser.Manage().Logs.AvailableLogTypes)
                using (var sw = new StreamWriter($"{fileName}.{log}.log", false))
                    foreach (var logentry in _browser.Manage().Logs.GetLog(log))
                        sw.WriteLine(logentry);
        }
    }
}
