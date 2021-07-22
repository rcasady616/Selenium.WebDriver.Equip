using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Selenium.WebDriver.Equip.Tests;
using Selenium.WebDriver.Equip.WebDriver;

namespace Selenium.WebDriver.Equip.Manager.Tests
{
    [Category(TestCategories.LocalOnly)]
    public class SauceServerTests<TDriver> : BaseFixture<TDriver> where TDriver : IWebDriver, new()
    {
        public SauceServerTests(OSType os) : base(os)
        {
        }

        [Test]
        public void GetDriver()
        {
            var url = "http://rickcasady.blogspot.com/";
            Driver.Navigate().GoToUrl(url);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(typeof(RemoteWebDriver), Driver.GetType());
                Assert.AreEqual(url, Driver.Url);
            });
        }
    }
}
