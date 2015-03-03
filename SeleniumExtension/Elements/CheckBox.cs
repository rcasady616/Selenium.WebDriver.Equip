using OpenQA.Selenium;

namespace SeleniumExtension.Elements
{
    /// <summary>
    /// HTML CheckBox element
    /// </summary>
    public class CheckBox : BaseElement, IHtmlElement
    {
        /// <summary>
        /// Gets a value indicating whether or not this element is selected.
        /// </summary>
        public bool Selected { get { return WrappedElement.Selected; } }

        /// <summary>
        /// 
        /// </summary>
        public CheckBox()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iWebElement"></param>
        public CheckBox(IWebElement iWebElement)
        {
            WrappedElement = iWebElement;
        }

        /// <summary>
        /// Check a <see cref="CheckBox"/> if unchecked, do nothing if already checked
        /// </summary>
        public void Check()
        {
            WrappedElement.Element<CheckBox>().Set(true);
        }

        /// <summary>
        /// unCheck a <see cref="CheckBox"/> if checked, do noting if alrady unchecked
        /// </summary>
        public void UnCheck()
        {
            WrappedElement.Element<CheckBox>().Set(false);
        }

        /// <summary>
        /// Set a <see cref="CheckBox"/> to a specific value
        /// </summary>
        /// <param name="selected"></param>
        public void Set(bool selected)
        {
            if (selected != WrappedElement.Selected)
                WrappedElement.Click();
        }
    }
}
