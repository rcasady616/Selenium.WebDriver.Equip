using OpenQA.Selenium;

namespace Selenium.WebDriver.Equip.PageObjectGenerator
{
    public class POG
    {
        public string PageSource { set; get; }
        public string Name { set; get; }
        public string Url { set; get; }

        public POG(IWebDriver driver)
        {
            PageSource = driver.PageSource;
            Url = driver.Url;
        }

        public POG(IWebDriver driver, string name)
        {
            PageSource = driver.PageSource;
            Name = name;
            Url = driver.Url;
        }

        public VirtualPage GeneratePage()
        {
            return new VirtualPage(PageSource);
        }
    }
}
