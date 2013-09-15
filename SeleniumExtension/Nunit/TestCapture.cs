using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using OpenQA.Selenium;

namespace SeleniumExtension.Nunit
{
    public class TestCapture
    {
        private IWebDriver _browser = null;

        public TestCapture(IWebDriver iWebDriver)
        {
            _browser = iWebDriver;
        }

        public void CaptureWebPage(string type = "Failed")
        {
            string fileName = string.Format(@"{0}\{1}.{2}.", Directory.GetCurrentDirectory(), DateTime.Now.Ticks, type);
            PageSource(fileName);
            ScreenShot(fileName);
        }

        public void PageSource(string fileName)
        {
            string htmlFile = string.Format(@"{0}.html", fileName);
            using (var sw = new StreamWriter(htmlFile, false))
                sw.Write(_browser.PageSource);
        }

        public void ScreenShot(string fileName)
        {
            var tempDriver = (ITakesScreenshot)_browser;
            Screenshot screenShot = tempDriver.GetScreenshot();
            screenShot.SaveAsFile(fileName + "jpeg", ImageFormat.Jpeg);
        }

        private static string GetCleanTestName(string fullName)
        {
            if (fullName.Contains("("))
                fullName = fullName.Substring(0, fullName.LastIndexOf("("));
            var justName = fullName.Split('.').Last();
            return justName;
        }
    }
}
