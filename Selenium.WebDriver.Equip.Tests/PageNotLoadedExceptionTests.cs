using NUnit.Framework;
using TestWebPages.UIFramework.Pages;
namespace Selenium.WebDriver.Equip.Tests
{
    [TestFixture]
    public class PageNotLoadedExceptionTests
    {
        [Test]
        [Category(TestCategories.HeadLess)]
        public void PageNotLoadedExceptionPageName()
        {
            var ajaxyControlPage = new AjaxyControlPage();
            var pageException = Assert.Throws<PageNotLoadedException>(() => { throw new PageNotLoadedException(ajaxyControlPage); });
            Assert.AreEqual("Page name: TestWebPages.UIFramework.Pages.AjaxyControlPage", pageException.Message);
        }
    }
}
