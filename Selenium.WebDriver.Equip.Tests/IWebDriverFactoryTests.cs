using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace Selenium.WebDriver.Equip.Tests
{
    [TestFixture]
    [Category(TestCategories.LocalDriver)]
    public class IWebDriverFactoryTests
    {
        private IWebDriver _driver;
        [SetUp]
        public void SetupTest()
        {
        }

        [TearDown]
        public void TearDown()
        {
            if (_driver != null)
            {
                _driver.Close();
                _driver.Quit();
            }
        }

        [Test]
        public void GetFirefoxBrowser64()
        {

            Assume.That(_driver.DownloadGeckoDriver());
            _driver = WebDriverFactory.GetBrowser<FirefoxDriver>("http://rickcasady.blogspot.com/");
            Assert.AreEqual(typeof(FirefoxDriver), _driver.GetType());
        }

        [Test]
        public void GetInternetExplorerBrowser()
        {
            Assume.That(_driver.GetNuGetIEDriver());
            _driver = WebDriverFactory.GetBrowser<InternetExplorerDriver>("http://rickcasady.blogspot.com/");
            Assert.AreEqual(typeof(InternetExplorerDriver), _driver.GetType());
        }

        [Test]
        public void GetChromeBrowser()
        {
            Assume.That(_driver.GetNuGetChromeDriver());
            _driver = WebDriverFactory.GetBrowser<ChromeDriver>("http://rickcasady.blogspot.com/");
            Assert.AreEqual(typeof(ChromeDriver), _driver.GetType());
        }

        [Test]
        public void GetChromeBrowserWithOptions()
        {
            var options = new ChromeOptions();
            options.AddArgument("-incognito");

            Assume.That(_driver.GetNuGetChromeDriver());
            _driver = WebDriverFactory.GetBrowser<ChromeDriver, ChromeOptions>(options, "http://rickcasady.blogspot.com/");
           
        }
    }
}
