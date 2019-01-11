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
            _driver = WebDriverFactory.GetBrowser<ChromeDriver, ChromeOptions>(options);
            var html_content = @"
            <html>
              <div id='result'></div>
            </html>";
            _driver.Navigate().GoToUrl("data:text/html;charset=utf-8," + html_content);

            _driver.Scripts().ExecuteScript(@"function main() {
              var fs = window.RequestFileSystem || window.webkitRequestFileSystem;
              if (!fs) {
                result.textContent = 'check failed ? ';
                return;
                    }
              fs(window.TEMPORARY, 100, function(fs)
                    {
                        result.textContent = 'it does not seem like you are in incognito mode';
                    }, function(err)
                    {
                        result.textContent = 'it seems like you are in incognito mode';
                    });
            }
            main();");

            StringAssert.Contains("it seems like you are in incognito mode", _driver.PageSource);
        }
    }
}
