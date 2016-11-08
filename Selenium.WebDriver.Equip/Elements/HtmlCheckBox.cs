using OpenQA.Selenium;

namespace Selenium.WebDriver.Equip.Elements
{
    /// <summary>
    /// HTML CheckBox element
    /// </summary>
    public class HtmlCheckBox : BaseElement, IHtmlElement
    {
        /// <summary>
        /// Gets a value indicating whether or not this element is selected.
        /// </summary>
        public bool Selected { get { return WrappedElement.Selected; } }

        /// <summary>
        /// 
        /// </summary>
        public HtmlCheckBox()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iWebElement"></param>
        public HtmlCheckBox(IWebElement iWebElement)
        {
            WrappedElement = iWebElement;
        }

        /// <summary>
        /// Check a <see cref="HtmlCheckBox"/> if unchecked, do nothing if already checked
        /// </summary>
        public void Check()
        {
            WrappedElement.Element<HtmlCheckBox>().Set(true);
        }

        /// <summary>
        /// unCheck a <see cref="HtmlCheckBox"/> if checked, do noting if alrady unchecked
        /// </summary>
        public void UnCheck()
        {
            WrappedElement.Element<HtmlCheckBox>().Set(false);
        }

        /// <summary>
        /// Set a <see cref="HtmlCheckBox"/> to a specific value
        /// </summary>
        /// <param name="selected"></param>
        public void Set(bool selected)
        {
            if (selected != WrappedElement.Selected)
                WrappedElement.Click();
        }
    }
}
