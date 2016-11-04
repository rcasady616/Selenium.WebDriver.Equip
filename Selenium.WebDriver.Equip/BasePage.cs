using OpenQA.Selenium;

namespace Selenium.WebDriver.Equip
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
