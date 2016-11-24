using OpenQA.Selenium;
using Selenium.WebDriver.Equip;

namespace TestWebPages.UIFramework.Pages
{
    public class TablesPage : BasePage, IPage
    {
        public static string Url = "http://rickcasady.com/SeleniumExtentions/v1.0/TestWebPages/Tables.html";

        #region Static By Selectors

        public static By ByTableOne = By.Id("1");
        public static By ByTableTwo = By.Id("2");

        #endregion

        public TablesPage(IWebDriver driver)
            : base(driver)
        {

        }

        public TablesPage()
        {
        }

        public bool IsPageLoaded()
        {
            return Driver.WaitUntilVisible(ByTableOne);
        }
    }
}
