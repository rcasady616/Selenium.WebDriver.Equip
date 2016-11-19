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
            Alert();
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
            _browser.TakeScreenShot(fileName + ".jpeg", ImageFormat.Jpeg);
        }

        public void Alert()
        {
            if (_browser.WaitUntilAlertExists(0))
                using (var sw = new StreamWriter(string.Format(@"{0}.Alert", fileName), false))
                    sw.Write(_browser.Alert().Text);
        }

    }
}
