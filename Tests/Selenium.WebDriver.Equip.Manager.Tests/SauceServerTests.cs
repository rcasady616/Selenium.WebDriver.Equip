using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using Selenium.WebDriver.Equip.Tests;
using Selenium.WebDriver.Equip.WebDriver;

namespace Selenium.WebDriver.Equip.Manager.Tests
{
    [TestFixture]
    [TestFixture(typeof(ChromeDriver), OSType.LINUX)]
    [TestFixture(typeof(ChromeDriver), OSType.MAC)]
    [TestFixture(typeof(ChromeDriver), OSType.WINDOWS)]
    [TestFixture(typeof(FirefoxDriver), OSType.LINUX)]
    [TestFixture(typeof(FirefoxDriver), OSType.MAC)]
    [TestFixture(typeof(FirefoxDriver), OSType.WINDOWS)]
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
