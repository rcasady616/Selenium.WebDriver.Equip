using OpenQA.Selenium;
using Selenium.WebDriver.Equip;

namespace TheInternet.UIFramework.Pages
{
    public class ABTestingPage : BasePage, IPage
    {
        public static string Url = "/abtest";

        #region Static By Selectors

        public static By ByABTestControlHeader = By.ClassName("h3");

        #endregion

        public ABTestingPage(IWebDriver driver)
            : base(driver)
        {

        }

        public ABTestingPage()
        {
        }

        public bool IsPageLoaded()
        {
            return Driver.WaitUntilExists(ByABTestControlHeader);
        }
    }
}
