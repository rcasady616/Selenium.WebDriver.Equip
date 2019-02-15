using NUnit.Framework;
using OpenQA.Selenium;
using Selenium.WebDriver.Equip.PageObjectGenerator;
using System.Collections.Generic;

namespace Selenium.WebDriver.Equip.Tests
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
            var url = Driver.Url;
            var handel = Driver.CurrentWindowHandle;
            var links = GetLinks();
            var pogs = new List<VirtualPage>();
            foreach (var link in links)
            {
                Assume.That(url == Driver.Url);
                if (Driver.ElementExists(link.Locator))
                {
                    if (Driver.FindElement(link.Locator).Displayed)
                    {
                        var before = Driver.WindowHandles.Count;
                        Driver.FindElement(link.Locator).Click();
                        var after = Driver.WindowHandles.Count;

                        //var sol = new LinkChecker(Driver).GetLinks();
                        pogs.Add(Driver.PageObjectGenerator().GeneratePage());

                        if (before != after)
                        {
                            handel = Driver.CurrentWindowHandle;
                            // new window or tab
                            Driver = Driver.PopBrowser();
                            Driver.Close();
                            Driver = Driver.SwitchTo().Window(handel);
                            //continue;
                        }
                        // StringAssert.Contains(link.Href, Driver.Url);
                        if (url != Driver.Url)
                            Driver.Navigate().Back();
                    }
                }
            }
        }

        public static string MakeMap(IWebElement iWebElement)
        {
            return iWebElement.CreateCssSelectorString();
        }
    }
}
