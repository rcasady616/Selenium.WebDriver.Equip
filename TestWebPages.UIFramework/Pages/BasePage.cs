using NUnit.Framework;
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

        [TearDown]
        public void TearDown()
        {
            if (Driver != null)
            {
                Driver.Close();
                Driver.Quit();
            }
        }
        public IWebDriver Driver { get; set; }
    }
}
