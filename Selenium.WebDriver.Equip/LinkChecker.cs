using OpenQA.Selenium;
using Selenium.WebDriver.Equip.PageObjectGenerator;
using System.Collections.Generic;

namespace Selenium.WebDriver.Equip
{

    public class LinkChecker
    {
        public IWebDriver Driver;

        public LinkChecker(IWebDriver driver)
        {
            Driver = driver;
        }

        public IEnumerable<VirtualElement> GetLinks()
        {
            return Driver.PageObjectGenerator().GeneratePage().GetLinks();
        }

        public void HammerLinks()
        {
            var links = GetLinks();
            foreach (var link in links)
            {
                Driver.FindElement(link.Locator).Click();
                Driver.Navigate().Back();
            }
        }

        public static string MakeMap(IWebElement iWebElement)
        {
            return iWebElement.CreateCssSelectorString();

        }

       
    }

}
