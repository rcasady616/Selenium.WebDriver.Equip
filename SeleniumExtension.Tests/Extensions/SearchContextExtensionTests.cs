using NUnit.Framework;
using OpenQA.Selenium;
using TestWebPages.UIFramework.Pages;

namespace SeleniumExtension.Tests.Extensions
{
    [TestFixture]
    [Category("Extension")]
    public class SearchContextExtentionTests : BaseTest
    {
        public AjaxyControlPage Page;

        [SetUp]
        public void SetupSearchContextExtentionTests()
        {
            Driver.Navigate().GoToUrl(AjaxyControlPage.Url);
            Page = new AjaxyControlPage(Driver);
            Assert.AreEqual(true, Page.IsPageLoaded());
        }

        [Category("unit")]
        [TestCase(true, "red")]
        [TestCase(false, "NeverGonnaGetItNeverGonnaGetIt")]
        public void TestElementExists(bool expected, string id)
        {
            Assert.AreEqual(expected, Driver.ElementExists(By.Id(id)));
        }
    }
}
