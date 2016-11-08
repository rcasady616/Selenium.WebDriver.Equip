using OpenQA.Selenium;

namespace Selenium.WebDriver.Equip.Elements
{
    /// <summary>
    /// HTML List element
    /// </summary>
    public class HtmlList : BaseElement, IHtmlElement
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iWebElement"></param>
        public HtmlList(IWebElement iWebElement)
        {
            WrappedElement = iWebElement;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ListItemPresent(string name)
        {
            return GetItem(name) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IWebElement GetItem(string name)
        {
            return WrappedElement.FindElement(By.CssSelector(string.Format("li[text='{0}']", name)));
        }
    }
}
