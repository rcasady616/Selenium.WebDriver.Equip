using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using TestWebPages.UIFramework.Pages;

namespace SeleniumExtension.Tests
{
    [TestFixture]
    public class ISearchContextExtentionTests
    {
        private IWebDriver _driver;
        private AjaxyControlPage page;

        [SetUp]
        public void SetupTest()
        {
            string url = string.Format(@"file:///{0}../../../..{1}", Directory.GetCurrentDirectory(), AjaxyControlPage.Url);
            _driver = WebDriverFactory.GetBrowser(url);
            page = new AjaxyControlPage(_driver);
            Assert.AreEqual(true, page.IsPageLoaded());
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
        public void TestFindElementExtention()
        {
            Assert.AreEqual(_driver.FindElement(By.Id("red")), _driver.FindElement("red"));
        }

        [TestCase(true, "red")]
        [TestCase(false, "NeverGonnaGetItNeverGonnaGetIt")]
        public void TestElementExists(bool expected, string id)
        {
            Assert.AreEqual(expected, _driver.ElementExists(By.Id(id)));
        }
    }
}
