using NUnit.Framework;
using OpenQA.Selenium;

namespace Selenium.WebDriver.Equip.Tests.WebDriver
{
    [Category(TestCategories.LocalOnly)]
    public class WebDriverTests<TDriver> : BaseTests<TDriver> where TDriver : IWebDriver, new()
    {
        [Test]
        public void GetDriverGetBrowser()
        {
            var url = "http://rickcasady.blogspot.com/";

            Driver.Navigate().GoToUrl(url);
            Assert.AreEqual(typeof(TDriver), Driver.GetType());
            Assert.AreEqual(url, Driver.Url);
        }
    }
}
