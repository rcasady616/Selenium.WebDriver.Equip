using OpenQA.Selenium;
using SeleniumExtension;

namespace TheInternet.UIFramework.Pages
{
    public class HomePage : BasePage, IPage
    {
        public static string Url = "";

        #region Static By Selectors

        public static By ByAbTestingLink = By.LinkText("A/B Testing");
        public static By ByBasicAuthLink = By.LinkText("Basic Auth");
        public static By ByDragAndDropLink = By.LinkText("Drag and Drop");
        public static By BySortableDataTablesLink = By.LinkText("Sortable Data Tables");

        #endregion

        public HomePage(IWebDriver driver)
            : base(driver)
        {

        }

        public HomePage()
        {
        }

        public ABTestingPage ClickAbTesting()
        {
            return Driver.FindElement(ByAbTestingLink).ClickWaitForPage<ABTestingPage>(Driver);
        }

        public DragAndDropPage ClickDragAndDrop()
        {
            return Driver.FindElement(ByDragAndDropLink).ClickWaitForPage<DragAndDropPage>(Driver);
        }

        public SortableDataTablesPage ClickSortableDataTables()
        {
            return Driver.FindElement(BySortableDataTablesLink).ClickWaitForPage<SortableDataTablesPage>(Driver);
        }

        public bool IsPageLoaded()
        {
            return Driver.WaitUntilExists(ByAbTestingLink);
        }
    }
}
