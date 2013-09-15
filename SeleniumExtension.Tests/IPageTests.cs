using System.IO;
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
            string url = string.Format(@"file:///{0}../../../..{1}", Directory.GetCurrentDirectory(), AjaxyControlPage.Url);
            Driver.Navigate().GoToUrl(url);
            Assert.AreEqual(true, new AjaxyControlPage(Driver).IsPageLoaded());
        }

        [Test]
        public void TestIsPageLoadedFalse()
        {
            string url = string.Format(@"file:///{0}../../../../TestWebPages/PageA.htm", Directory.GetCurrentDirectory());
            Driver.Navigate().GoToUrl(url);
            Assert.AreEqual(false, new AjaxyControlPage(Driver).IsPageLoaded());
        }
    }
}
