using OpenQA.Selenium;

namespace SeleniumExtension
{
    public class BasePage
    {
        public IWebDriver Driver { get; set; }
     
        public BasePage(IWebDriver driver)
        {
            Driver = driver;
        }

        public BasePage()
        {
        }
    }
}
