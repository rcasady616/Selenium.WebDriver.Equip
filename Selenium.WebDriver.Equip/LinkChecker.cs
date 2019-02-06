using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace Selenium.WebDriver.Equip.Tools
{

    public class LinkChecker
    {
        public IWebDriver Driver;

        public LinkChecker(IWebDriver driver)
        {
            Driver = driver;
        }

        public ReadOnlyCollection<IWebElement> GetLinks()
        {
            return Driver.FindElements(By.TagName("a"));
        }

        public void HammerLinks()
        {
            var links = GetLinks();
            foreach (var link in links)
            {
                var t = link.Id();
                var name = link.CreateCssSelectorString();
            }
        }

        public static string MakeMap(IWebElement iWebElement)
        {
            return iWebElement.CreateCssSelectorString();

        }

       
    }

}
