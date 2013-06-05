using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace SeleniumExtention
{
    public class IWebDriverFactory
    {
        public static TBrowser GetBrowser<TBrowser>(string url = null) where TBrowser : IWebDriver, new()
        {
            var browser = (TBrowser)Activator.CreateInstance(typeof(TBrowser));

            if (url != null)
                browser.Navigate().GoToUrl(url);
            return browser;
        }

        public static IWebDriver GetBrowser(string url = null)
        {
            return GetBrowser<FirefoxDriver>(url);
        }
    }
}
