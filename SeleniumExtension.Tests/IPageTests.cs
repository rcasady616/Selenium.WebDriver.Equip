using NUnit.Framework;
using SeleniumExtension.Nunit;
using TestWebPages.UIFramework.Pages;

namespace SeleniumExtension.Tests
{
    [TestFixture]
    public class IPageTests : BaseTest
    {
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
