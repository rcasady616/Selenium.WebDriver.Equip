using NUnit.Framework;
using TestWebPages.UIFramework.Pages;

namespace Selenium.WebDriver.Equip.Tests
{
    [TestFixture]
    public class IPageTests : BaseTest
    {
        public string pageAUrl = "http://rickcasady.com/SeleniumExtentions/v1.0/TestWebPages/PageA.htm";

        [Test]
        public void TestIsPageLoaded()
        {
            Driver.Navigate().GoToUrl(AjaxyControlPage.Url);
            Assert.AreEqual(true, new AjaxyControlPage(Driver).IsPageLoaded());
        }

        [Test]
        public void TestIsPageLoadedFalse()
        {
            Driver.Navigate().GoToUrl(pageAUrl);
            Assert.AreEqual(false, new AjaxyControlPage(Driver).IsPageLoaded());
        }
    }
}
