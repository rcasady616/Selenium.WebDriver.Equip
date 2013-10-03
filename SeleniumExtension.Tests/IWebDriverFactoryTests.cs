using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;

namespace SeleniumExtension.Tests
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
            _driver = WebDriverFactory.GetBrowser<FirefoxDriver>("http://rickcasady.blogspot.com/");
            Assert.AreEqual(typeof(FirefoxDriver), _driver.GetType());
        }

        [Test]
        public void GetInternetExplorerBrowserTest()
        {
            _driver = WebDriverFactory.GetBrowser<InternetExplorerDriver>("http://rickcasady.blogspot.com/");
            Assert.AreEqual(typeof(InternetExplorerDriver), _driver.GetType());
        }

        [Test]
        public void GetChromeBrowserTest()
        {
            _driver = WebDriverFactory.GetBrowser<ChromeDriver>("http://rickcasady.blogspot.com/");
            Assert.AreEqual(typeof(ChromeDriver), _driver.GetType());
        }

        [Test]
        public void GetSafariBrowserTest()
        {
            _driver = WebDriverFactory.GetBrowser<SafariDriver>("http://rickcasady.blogspot.com/");
            Assert.AreEqual(typeof(SafariDriver), _driver.GetType());
        }

    }
}
