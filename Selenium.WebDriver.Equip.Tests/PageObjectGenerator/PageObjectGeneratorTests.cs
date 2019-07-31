using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using TestWebPages.UIFramework.Pages;

namespace Selenium.WebDriver.Equip.Tests.PageObjectGenerator
{
    [TestFixture]
    public class PageObjectGeneratorTests : BaseTest
    {
        public IndexPage IndexPage;
        public string IndexUrl = "http://rickcasady.com/SeleniumExtentions/v1.0/TestWebPages/index.html";
        public string ThrowAlertUrl = "http://rickcasady.com/SeleniumExtentions/v1.0/TestWebPages/ThrowAlert.html";

        [SetUp]
        public void SetupWebDriverExtensionTests()
        {
            Driver.Navigate().GoToUrl(IndexUrl);
            IndexPage = new IndexPage(Driver);
            Assert.AreEqual(true, IndexPage.IsPageLoaded());
        }

        [Test]
        public void TestPageObjectGeneratorLink()
        {
            var virualIndexPage = Driver.PageObjectGenerator().GeneratePage();
            var pageALink = virualIndexPage.Descendants().Where(element => element.LocatorText == "PageA").First();
            Assert.AreEqual("a", pageALink.TagName);
            Assert.AreEqual("PageA", Driver.FindElement(pageALink.Locator).Text);
        }

        [Test]
        public void TestGetLinks()
        {
            Driver.Navigate().GoToUrl(@"C:\Users\Rick\Documents\GitHub2\Selenium.WeDriver.Equip\TestWebPages\PageA.htm");//http://saeedgatson.com/");
            var virualIndexPage = Driver.PageObjectGenerator().GeneratePage();

            var virtualLinks = virualIndexPage.GetDistinctIds();

            Assert.AreEqual(3, virtualLinks.Count());
            var actuals = new List<string> { "PageALink", "AjaxyControlPageLink","AjaxyControlPageNewwindowLink" };
            foreach (var link in virtualLinks)
            {
                CollectionAssert.Contains(actuals, link.Name);
            }
        }
    }
}
