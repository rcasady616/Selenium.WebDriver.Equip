using SeleniumExtention;

namespace OpenQA.Selenium
{
    public static class IWebElementExtention
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

        /// <summary>
        /// Gets the value of the <see cref="HtmlTagAttribute"/> 
        /// </summary>
        /// <param name="htmlTagAttribute">The <see cref="HtmlTagAttribute"/> to get the value of</param>
        /// <returns>The value of the attribute</returns>
        public static string GetAttribute(this IWebElement iWebElement, HtmlTagAttribute htmlTagAttribute)
        {
            return iWebElement.GetAttribute(htmlTagAttribute.ToString());
        }
    }
}
