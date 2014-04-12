using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using TestWebPages.UIFramework.Pages;

namespace SeleniumExtension.Tests
{
    [TestFixture]
    public class ISearchContextExtentionTests : BasePage
    {
        public AjaxyControlPage Page;

        [SetUp]
        public void SetupTest()
        {
            Driver.Navigate().GoToUrl(AjaxyControlPage.Url);
            Page = new AjaxyControlPage(Driver);
            Assert.AreEqual(true, Page.IsPageLoaded());
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
    }
}
