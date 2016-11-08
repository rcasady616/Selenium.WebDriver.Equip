using System;
using System.Collections.Generic;
using OpenQA.Selenium.Internal;
using Selenium.WebDriver.Equip;
using Selenium.WebDriver.Equip.Elements;

namespace OpenQA.Selenium
{
    public static class WebElementExtention
    {
        #region Mock properties extention

        /// <summary>
        /// Gets the class name of this <see cref="IWebElement"/>
        /// </summary>
        /// <returns>The class name of the <see cref="IWebElement"/> on the current context</returns>
        public static string ClassName(this IWebElement iWebElement)
        {
            return iWebElement.GetAttribute(HtmlTagAttribute.Class);
        }

        /// <summary>
        /// Gets the id of this <see cref="IWebElement"/>
        /// </summary>
        /// <returns>The id of the <see cref="IWebElement"/> on the current context</returns>
        public static string Id(this IWebElement iWebElement)
        {
            return iWebElement.GetAttribute(HtmlTagAttribute.Id);
        }

        /// <summary>
        /// Gets the name of this <see cref="IWebElement"/>
        /// </summary>
        /// <returns>The name of the <see cref="IWebElement"/> on the current context</returns>
        public static string Name(this IWebElement iWebElement)
        {
            return iWebElement.GetAttribute(HtmlTagAttribute.Name);
        }

        /// <summary>
        /// Gets the next sibling of this <see cref="IWebElement"/>
        /// </summary>
        /// <returns>The next sibling <see cref="IWebElement"/> on the current context</returns>
        public static IWebElement NextSibling(this IWebElement iWebElement)
        {
            return iWebElement.FindElement(By.XPath("./following-sibling::*"));
        }

        /// <summary>
        /// Gets the parent of this <see cref="IWebElement"/>
        /// </summary>
        /// <returns>The parent <see cref="IWebElement"/> on the current context</returns>
        public static IWebElement Parent(this IWebElement iWebElement)
        {
            return iWebElement.FindElement(By.XPath("./parent::*"));
        }

        /// <summary>
        /// Gets the previous sibling of this <see cref="IWebElement"/>
        /// </summary>
        /// <returns>The previous sibling <see cref="IWebElement"/> on the current context</returns>
        public static IWebElement PreviousSibling(this IWebElement iWebElement)
        {
            return iWebElement.FindElement(By.XPath("./preceding-sibling::*[1]"));
        }

        /// <summary>
        /// Gets the style of this <see cref="IWebElement"/>
        /// </summary>
        /// <returns>The style of the <see cref="IWebElement"/> on the current context</returns>
        public static string Style(this IWebElement iWebElement)
        {
            return iWebElement.GetAttribute(HtmlTagAttribute.Style);
        }

        /// <summary>
        /// Gets the title of this <see cref="IWebElement"/>
        /// </summary>
        /// <returns>The title of the <see cref="IWebElement"/> on the current context</returns>
        public static string Title(this IWebElement iWebElement)
        {
            return iWebElement.GetAttribute(HtmlTagAttribute.Title);
        }

        /// <summary>
        /// Gets the type of this <see cref="IWebElement"/>
        /// </summary>
        /// <returns>The type of the <see cref="IWebElement"/> on the current context</returns>
        public static string Type(this IWebElement iWebElement)
        {
            return iWebElement.GetAttribute(HtmlTagAttribute.Type);
        }

        /// <summary>
        /// Gets the value of this <see cref="IWebElement"/>
        /// </summary>
        /// <returns>The value of the <see cref="IWebElement"/> on the current context</returns>
        public static string Value(this IWebElement iWebElement)
        {
            return iWebElement.GetAttribute(HtmlTagAttribute.Value);
        }

        #endregion

        #region Clicks

        /// <summary>
        /// Clicks the current <see cref="IWebElement"/> and waits for the Post to start
        /// </summary>
        /// <param name="iWebDriver">The <see cref="IWebDriver"/> to be used for waiting</param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to exist</param>
        /// <returns></returns>
        /// <returns><see langword="true"/> if the DOM is cleared or empty; otherwise, <see langword="false"/></returns>
        public static bool ClickWaitUntilPost(this IWebElement iWebElement, IWebDriver iWebDriver, int maxWaitTimeInSeconds = 10)
        {
            By wait = By.CssSelector(string.Format("[{0}=\"true\"]", HtmlTagAttribute.WaitForPost));
            iWebElement.SetAttribute(HtmlTagAttribute.WaitForPost, "true");
            iWebElement.Click();
            return iWebDriver.WaitUntilNotExists(wait, maxWaitTimeInSeconds);
        }

        /// <summary>
        /// Clicks the current <see cref="IWebElement"/> and waits for the <see cref="IPage"/> to load
        /// </summary>
        /// <typeparam name="TPage">The type <see cref="IPage"/></typeparam>
        /// <param name="driver">The <see cref="IWebDriver"/> to be used for creating an instance of the <see cref="IPage"/></param>
        /// <returns><see langword="true"/> if the <see cref="IPage"/> has loaded; otherwise, <see langword="false"/></returns>
        public static TPage ClickWaitForPage<TPage>(this IWebElement iWebElement, IWebDriver driver) where TPage : IPage, new()
        {
            TPage page = (TPage)Activator.CreateInstance(typeof(TPage), driver);
            iWebElement.Click();
            if (!page.IsPageLoaded())
                throw new PageNotLoadedException(string.Format("Page name: {0}", page.ToString()));
            return page;
        }

        /// <summary>
        /// Click the current <see cref="IWebElement"/> and then waits for the another <see cref="IWebElement"/> to be visible in the page DOM
        /// </summary>
        /// <param name="iWebDriver">to use for waiting</param>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/> to wait for</param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to exist</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> is visible; otherwise, <see langword="false"/></returns>
        public static bool ClickWaitUnilVisable(this IWebElement iWebElement, IWebDriver iWebDriver, By locator, int maxWaitTimeInSeconds = 10)
        {
            iWebElement.Click();
            return iWebDriver.WaitUntilVisible(locator, maxWaitTimeInSeconds);
        }

        /// <summary>
        /// Click the current <see cref="IWebElement"/> and then waits for the another <see cref="IWebElement"/> to be visible in the page DOM
        /// </summary>
        /// <param name="iWebDriver">to use for waiting</param>
        /// <param name="bys">The <see cref="HtmlList"/>&lt;<see cref="By"/>&gt; locator of the <see cref="IWebElement"/>s to wait for</param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to exist</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/>s are visible; otherwise, <see langword="false"/></returns>
        public static bool ClickWaitUnilVisables(this IWebElement iWebElement, IWebDriver driver, List<By> bys, int maxWaitTimeInSeconds = 10)
        {
            iWebElement.Click();
            foreach (var by in bys)
            {
                if (!driver.WaitUntilVisible(by, maxWaitTimeInSeconds))
                    return false;
            }
            return true;
        }

        #endregion

        /// <summary>
        /// Gets the value of the <see cref="HtmlTagAttribute"/> 
        /// </summary>
        /// <param name="htmlTagAttribute">The <see cref="HtmlTagAttribute"/> to get the value of</param>
        /// <returns>The value of the attribute</returns>
        public static string GetAttribute(this IWebElement iWebElement, HtmlTagAttribute htmlTagAttribute)
        {
            return iWebElement.GetAttribute(htmlTagAttribute.ToString());
        }

        /// <summary>
        /// Sets the value of the <see cref="HtmlTagAttribute"/> 
        /// </summary>
        /// <param name="attributeName">The name of the attribute to set the value of</param>
        public static void SetAttribute(this IWebElement element, string attributeName, string value)
        {
            IWrapsDriver wrappedElement = element as IWrapsDriver;
            if (wrappedElement == null)
                throw new ArgumentException("element", "Element must wrap a web driver");

            IWebDriver driver = wrappedElement.WrappedDriver;
            IJavaScriptExecutor javascript = driver as IJavaScriptExecutor;
            if (javascript == null)
                throw new ArgumentException("element", "Element must wrap a web driver that supports javascript execution");

            javascript.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2])", element, attributeName, value);
        }

        public static T Element<T>(this IWebElement iWebElement) where T : IHtmlElement, new()
        {
            var htmlElement = (T)Activator.CreateInstance(typeof(T), iWebElement);
            return htmlElement;
        }

        #region experimental

        public static bool ClickWaitForCondition<T>(this IWebElement iWebElement, IWebDriver driver, Func<IWebDriver, T> condition)
        {
            iWebElement.Click();
            return driver.DriverWaitUntil(condition);
        }

        public static bool ClickWaitForConditions<T>(this IWebElement iWebElement, IWebDriver driver, List<Func<IWebDriver, T>> conditions)
        {
            iWebElement.Click();
            foreach (var condition in conditions)
            {
                if (!driver.DriverWaitUntil(condition))
                    return false;
            }
            return true;
        }

        public static string CreateLocatorName(this IWebElement iWebElement)
        {
            if (!string.IsNullOrEmpty(iWebElement.Id()))
                return iWebElement.Id();
            switch (iWebElement.TagName)
            {
                case HtmlTags.A:
                    if (!string.IsNullOrEmpty(iWebElement.Text))
                        return iWebElement.Text.Replace(" ", null);
                    break;
                default:
                    return null;
            }
            return null;
        }

        public static string CreateCssSelector(this IWebElement iWebElement)
        {
            string cssString = null;
            if (!string.IsNullOrEmpty(iWebElement.Id()))
            {
                cssString = string.Format("#{0}", iWebElement.Id());
            }
            switch (iWebElement.TagName)
            {
                case HtmlTags.A:
                    if (!string.IsNullOrEmpty(iWebElement.Text))
                        cssString = string.Format("a[href='{0}']", iWebElement.GetAttribute(HtmlTagAttribute.Href));
                    break;
                case HtmlTags.Input:
                    switch (iWebElement.Type())
                    {
                        case "text":
                            break;
                        default:
                            cssString = null;
                            break;
                    }
                    //checkbox
                    //radio
                    //password
                    break;
                case HtmlTags.Button:
                    break;
                case HtmlTags.Select:
                    break;
                case HtmlTags.Option:
                    break;
                default:
                    cssString = null;
                    break;
            }
            return cssString;
        }

        #endregion
    }
}
