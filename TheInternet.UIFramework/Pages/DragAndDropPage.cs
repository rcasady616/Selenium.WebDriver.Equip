using OpenQA.Selenium;
using Selenium.WebDriver.Equip;

namespace TheInternet.UIFramework.Pages
{
    public class DragAndDropPage : BasePage, IPage
    {
        public static string Url = "/drag_and_drop";

        #region Static By Selectors

        public static By ByTitleHeader = By.ClassName("h3");
        public static By ByColumnADiv = By.ClassName("column-a");
        public static By ByColumnBDiv = By.ClassName("column-b");

        #endregion

        public DragAndDropPage(IWebDriver driver)
            : base(driver)
        {

        }

        public DragAndDropPage()
        {
        }

        public bool IsPageLoaded()
        {
            return Driver.WaitUntilExists(ByColumnBDiv);
        }
    }
}
