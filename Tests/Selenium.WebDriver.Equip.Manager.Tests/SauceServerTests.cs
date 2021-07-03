using NUnit.Framework;
using OpenQA.Selenium;
using Selenium.WebDriver.Equip.Tests;

namespace Selenium.WebDriver.Equip.Manager.Tests
{
    [TestFixture]
    public class SauceServerTests<TDriver> : BaseFixture<TDriver> where TDriver : IWebDriver, new()
    {
        [Test]
        public void GetDriver()
        {
            var url = "http://rickcasady.blogspot.com/";
            Driver.Navigate().GoToUrl(url);
            Assert.AreEqual(typeof(TDriver), Driver.GetType());
            Assert.AreEqual(url, Driver.Url);
        }
    }
}
