using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;

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
    }
}
