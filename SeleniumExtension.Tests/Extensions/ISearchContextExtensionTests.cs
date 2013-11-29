using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using TestWebPages.UIFramework.Pages;

namespace SeleniumExtension.Tests
{
    [TestFixture]
    public class ISearchContextExtentionTests : BasePage
    {
        private AjaxyControlPage page;

        [SetUp]
        public void SetupTest()
        {
            string url = string.Format(@"file:///{0}../../../..{1}", Directory.GetCurrentDirectory(), AjaxyControlPage.Url);
            Driver = WebDriverFactory.GetBrowser(url);
            page = new AjaxyControlPage(Driver);
            Assert.AreEqual(true, page.IsPageLoaded());
        }

        [Test]
        public void TestFindElementExtention()
        {
            Assert.AreEqual(Driver.FindElement(By.Id("red")), Driver.FindElement("red"));
        }

        [TestCase(true, "red")]
        [TestCase(false, "NeverGonnaGetItNeverGonnaGetIt")]
        public void TestElementExists(bool expected, string id)
        {
            Assert.AreEqual(expected, Driver.ElementExists(By.Id(id)));
        }

        [TestCase(true, "red")]
        [TestCase(false, "NeverGonnaGetItNeverGonnaGetIt")]
        public void TestWaitUntilExists(bool expected, string id)
        {
            var body = Driver.FindElement(By.TagName("body"));
            Assert.AreEqual(expected, body.WaitUntilExists(By.Id(id)));
        }

        [TestCase(false, "red")]
        [TestCase(true, "NeverGonnaGetItNeverGonnaGetIt")]
        public void TestIWebElemnetWaitUntilNotExists(bool expected, string id)
        {
            var body = Driver.FindElement(By.TagName("body"));
            Assert.AreEqual(expected, body.WaitUntilNotExists(By.Id(id)));
        }

        [TestCase(false, "red")]
        [TestCase(true, "NeverGonnaGetItNeverGonnaGetIt")]
        public void TestIWebDriverWaitUntilNotExists(bool expected, string id)
        {
            Assert.AreEqual(expected, Driver.WaitUntilNotExists(By.Id(id)));
        }

        [Test]
        public void Testy()
        {
            Driver.Navigate().GoToUrl(string.Format(@"file:///{0}../../../..{1}", Directory.GetCurrentDirectory(), FileUploadPage.Url));
            var fPage = new FileUploadPage(Driver);
            Assert.That(fPage.IsPageLoaded());
            fPage.UploadFile(@"C:\Users\rcasady\Documents\automation.gif");
            Driver.FindElement(FileUploadPage.ByFile);
            //_driver.Manage().Window.
        }
        
    }
}
