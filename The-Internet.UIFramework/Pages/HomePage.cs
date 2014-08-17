using OpenQA.Selenium;
using SeleniumExtension;

namespace The_Internet.UIFramework.Pages
{
    public class HomePage : BasePage, IPage
    {
        public static string Url = "";
        
        #region Static By Selectors

        public static By ByABTestingLink = By.LinkText("A/B Testing");
        public static By ByBasicAuthLink = By.LinkText("Basic Auth");

        #endregion
        public bool IsPageLoaded()
        {
            return Driver.WaitUntilExists(ByABTestingLink);
        }
    }
}
