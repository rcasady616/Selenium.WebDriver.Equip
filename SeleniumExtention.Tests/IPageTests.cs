using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using TestWebPages.UIFramework.Pages;

namespace SeleniumExtention.Tests
{
    [TestFixture]
    public class IPageTests
    {
        private IWebDriver _driver;
        [SetUp]
        public void SetupTest()
        {
            _driver = IWebDriverFactory.GetBrowser();
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
        public void TestIsPageLoaded()
        {
            string url = string.Format(@"file:///{0}../../../..{1}", Directory.GetCurrentDirectory(), AjaxyControlPage.Url);
            _driver.Navigate().GoToUrl(url);
            Assert.AreEqual(true, new AjaxyControlPage(_driver).IsPageLoaded());
        }

        [Test]
        public void TestIsPageLoadedFalse()
        {
            string url = string.Format(@"file:///{0}../../../../TestWebPages/PageA.htm", Directory.GetCurrentDirectory());
            _driver.Navigate().GoToUrl(url);
            Assert.AreEqual(false, new AjaxyControlPage(_driver).IsPageLoaded());
        }
    }
}
