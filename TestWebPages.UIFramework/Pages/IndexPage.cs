using OpenQA.Selenium;
using Selenium.WebDriver.Equip;
using TestWebPages.UIFramework.Pages;

namespace Medrio.QA.UITestFramework.Pages
{
    public class IndexPage : BasePage, IPage
    {
        public static string Url = "http://rickcasady.com/SeleniumExtentions/v1.0/TestWebPages/Index.html";

        #region Static By Selectors

        public static By ByAjaxyControlLink = By.LinkText("AjaxyControl Page");
        public static By ByPageALink = By.LinkText("PageA");
        public static By ByAjaxyControlNewWindowLink = By.LinkText("AjaxyControl Page New window");
        
        #endregion

        #region IWebElement properties

        public IWebElement AjaxyControlLink { get { return Driver.FindElement(ByAjaxyControlLink); } }
        public IWebElement PageALink { get { return Driver.FindElement(ByPageALink); } }
        public IWebElement AjaxyControlNewWindowLink { get { return Driver.FindElement(ByAjaxyControlNewWindowLink); } }

        #endregion

        #region constructors

        public IndexPage(IWebDriver driver)
            : base(driver)
        {

        }

        public IndexPage()
        {
        }

        #endregion

        #region public methods

        public bool IsPageLoaded()
        {
            return Driver.WaitUntilExists(ByAjaxyControlLink);
        }

        #endregion
    }
}
