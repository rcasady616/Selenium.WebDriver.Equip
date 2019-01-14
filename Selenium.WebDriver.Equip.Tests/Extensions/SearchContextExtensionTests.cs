using System.Diagnostics;
using NUnit.Framework;
using OpenQA.Selenium;
using TestWebPages.UIFramework.Pages;

namespace Selenium.WebDriver.Equip.Tests.Extensions
{
    [Parallelizable]
    [TestFixture]
    [Category(TestCategories.Extension)]
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
            var sw = new Stopwatch();
            sw.Start();
            var actual = Driver.ElementExists(By.Id(id));
            sw.Stop();
            Assert.Less(sw.Elapsed.Seconds, 1);
            Assert.AreEqual(expected, actual);
        }
    }
}
