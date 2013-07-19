using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;

namespace SeleniumExtention.Tests
{
    [TestFixture]
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
        public void GetFirefoxBrowserTest()
        {
            _driver = IWebDriverFactory.GetBrowser<FirefoxDriver>("http://rickcasady.blogspot.com/");
            Assert.AreEqual(typeof(FirefoxDriver), _driver.GetType());
        }

        [Test]
        public void GetInternetExplorerBrowserTest()
        {
            _driver = IWebDriverFactory.GetBrowser<InternetExplorerDriver>("http://rickcasady.blogspot.com/");
            Assert.AreEqual(typeof(InternetExplorerDriver), _driver.GetType());
        }

        [Test]
        public void GetChromeBrowserTest()
        {
            _driver = IWebDriverFactory.GetBrowser<ChromeDriver>("http://rickcasady.blogspot.com/");
            Assert.AreEqual(typeof(ChromeDriver), _driver.GetType());
        }

        [Test]
        public void GetSafariBrowserTest()
        {
            _driver = IWebDriverFactory.GetBrowser<SafariDriver>("http://rickcasady.blogspot.com/");
            Assert.AreEqual(typeof(SafariDriver), _driver.GetType());
        }

        [Test]
        public void GetRemoteWebDriverFirefoxTest()
        {
            _driver = IWebDriverFactory.GetRemoteWebDriver(null, "http://rickcasady.blogspot.com/");
            Assert.AreEqual(typeof(RemoteWebDriver), _driver.GetType());
        }
    }
}
