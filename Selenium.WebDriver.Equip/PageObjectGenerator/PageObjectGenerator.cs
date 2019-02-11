using OpenQA.Selenium;

namespace Selenium.WebDriver.Equip.PageObjectGenerator
{
    public class PageObjectGenerator
    {
        public string PageSource { set; get; }
        public string Name { set; get; }
        public string Url { set; get; }

        public PageObjectGenerator(IWebDriver driver)
        {
            PageSource = driver.PageSource;
            Url = driver.Url;
        }

        public PageObjectGenerator(IWebDriver driver, string name)
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
