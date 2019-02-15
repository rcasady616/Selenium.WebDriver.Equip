using OpenQA.Selenium;

namespace Selenium.WebDriver.Equip.PageObjectGenerator
{
    public class VirtualLocator
    {
        public string Name { get; set; }
        public LocatorType LocatorType { get; set; }
        public By Locator { get; set; }
        public string LocatorText { get; set; }
    }

    public enum LocatorType
    {
        Css,
        Id,
        LinkText,
        XPath
    }
}
