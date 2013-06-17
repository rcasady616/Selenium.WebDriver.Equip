using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using TestWebPages.UIFramework.Pages;

namespace SeleniumExtention.Tests
{
    [TestFixture]
    public class IsearchContextExtentionTests
    {
        private IWebDriver _driver;
        [SetUp]
        public void SetupTest()
        {
            _driver = IWebDriverFactory.GetBrowser(string.Format(@"file:///{0}../../../../TestWebPages/PageA.htm", Directory.GetCurrentDirectory()));
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
            Assert.AreEqual(_driver.FindElement(By.Id("text1")), _driver.FindElement("text1"));
        }

        [Test]
        public void TestWaitUntilExists()
        {
            string url = string.Format(@"file:///{0}../../../..{1}", Directory.GetCurrentDirectory(), AjaxyControlPage.Url);
            _driver.Navigate().GoToUrl(url);
            var page = new AjaxyControlPage(_driver);
            Assert.AreEqual(true, page.IsPageLoaded());
            page.GreenRadio.Click();
            page.NewLabelText.SendKeys("TestIsPageLoaded");
            page.SubmitButton.Click();
            Assert.AreEqual(true, _driver.WaitUntilExists(AjaxyControlPage.ByLabelsDiv));
        }
    }
}
