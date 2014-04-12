using OpenQA.Selenium;

namespace TestWebPages.UIFramework.Pages
{
    public class BasePage
    {
        public BasePage(IWebDriver driver)
        {
            Driver = driver;
        }

        public BasePage()
        {
        }

        public IWebDriver Driver { get; set; }
    }
}
