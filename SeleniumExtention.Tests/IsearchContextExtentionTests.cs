using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestWebPages.UIFramework.Pages;

namespace SeleniumExtention.Tests
{
    [TestFixture]
    public class IsearchContextExtentionTests
    {
        private IWebDriver _driver;
        private AjaxyControlPage page;

        [SetUp]
        public void SetupTest()
        {
            string url = string.Format(@"file:///{0}../../../..{1}", Directory.GetCurrentDirectory(), AjaxyControlPage.Url);
            _driver = IWebDriverFactory.GetBrowser(url);
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
        [TestCase(false, "NeverGonnaGetNeverGonnaGet")]
        public void TestWaitUntilExists(bool expected, string id)
        {
            Assert.AreEqual(expected, _driver.WaitUntilExists(By.Id(id)));
        }

        public void TestWaitUntilExists()
        {
            page.GreenRadio.Click();
            page.NewLabelText.SendKeys("TestIsPageLoaded");
            page.SubmitButton.Click();
            Assert.AreEqual(true, _driver.WaitUntilExists(AjaxyControlPage.ByLabelsDiv));
        }

        [TestCase(true, "red")]
        [TestCase(false, "NeverGonnaGetNeverGonnaGet")]
        public void TestWaitUntilVisible(bool expected, string id)
        {
            Assert.AreEqual(expected, _driver.WaitUntilVisible(By.Id(id)));
        }

        [Test]
        public void TestWaitUntilVisible()
        {
            page.GreenRadio.Click();
            page.NewLabelText.SendKeys("TestIsPageLoaded");
            page.SubmitButton.Click();
            Assert.AreEqual(true, _driver.WaitUntilVisible(AjaxyControlPage.ByLabelsDiv));
        }

        [TestCase(true, "red")]
        [TestCase(false, "NeverGonnaGetNeverGonnaGet")]
        public void TestElementExists(bool expected, string id)
        {
            Assert.AreEqual(expected, _driver.ElementExists(By.Id(id)));
        }
    }
}
