using OpenQA.Selenium;

namespace Selenium.WebDriver.Equip.Elements
{
    /// <summary>
    /// Contains the basic functionality that an HTML Element needs
    /// </summary>
    public class BaseElement
    {
        /// <summary>
        /// Wraps the actual <see cref="IWebElement"/>
        /// </summary>
        public IWebElement WrappedElement { get; set; }
    }
}