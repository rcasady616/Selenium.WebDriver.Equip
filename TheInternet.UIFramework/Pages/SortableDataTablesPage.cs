using OpenQA.Selenium;
using SeleniumExtension;
using SeleniumExtension.Elements;

namespace TheInternet.UIFramework.Pages
{
    public class SortableDataTablesPage : BasePage, IPage
    {
        public static string Url = "/tables";

        #region Static By Selectors

        public static By ByTitleHeader = By.ClassName("h3");
        public static By ByExample1Table = By.Id("table1");
        public static By ByExample2Table = By.Id("table2");

        #endregion

        public void SortTable()
        {
            var t = new Table(Driver.FindElement(ByExample1Table));
            var start = t.GetCell(2, 2);
            t.GetCell(4, 1).Click();
            var end = t.GetCell(2, 2);

        }

        public SortableDataTablesPage(IWebDriver driver)
            : base(driver)
        {

        }

        public SortableDataTablesPage()
        {
        }

        public bool IsPageLoaded()
        {
            return Driver.WaitUntilExists(ByExample1Table);
        }
    }
}
