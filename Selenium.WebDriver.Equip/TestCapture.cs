using System;
using System.Drawing.Imaging;
using System.IO;
using OpenQA.Selenium;

namespace Selenium.WebDriver.Equip
{
    public class TestCapture
    {
        private IWebDriver _browser = null;
        private string fileName;
        public TestCapture(IWebDriver iWebDriver, string type = "Failed")
        {
            fileName = string.Format(@"{0}\{1}.{2}", Directory.GetCurrentDirectory(), DateTime.Now.Ticks, type);
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
            string htmlFile = string.Format(@"{0}.html", fileName);
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
                using (var sw = new StreamWriter(string.Format(@"{0}.{1}.log", fileName, log), false))
                    foreach (var logentry in _browser.Manage().Logs.GetLog(log))
                        sw.WriteLine(logentry);
        }
    }
}
