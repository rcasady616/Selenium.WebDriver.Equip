using System;
using SeleniumExtention;

namespace OpenQA.Selenium
{
    public static class IWebElementExtention 
    {
        #region Mock properties extention

        public static bool Exists(this IWebElement iWebElement)
        {
            try
            {
                if (iWebElement != null)
                    return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string ClassName(this IWebElement iWebElement)
        {
            return iWebElement.GetAttribute(HtmlTagAttribute.Class);
        }

        public static string Id(this IWebElement iWebElement)
        {
            return iWebElement.GetAttribute(HtmlTagAttribute.Id);
        }

        public static string Name(this IWebElement iWebElement)
        {
            return iWebElement.GetAttribute(HtmlTagAttribute.Name);
        }

        public static IWebElement NextSibling(this IWebElement iWebElement)
        {
            return iWebElement.FindElement(By.XPath("./following-sibling::*"));
        }

        public static IWebElement Parent(this IWebElement iWebElement)
        {
            return iWebElement.FindElement(By.XPath("./parent::*"));
        }

        public static IWebElement PreviousSibling(this IWebElement iWebElement)
        {
            return iWebElement.FindElement(By.XPath("./preceding-sibling::*[1]"));
        }

        public static string Style(this IWebElement iWebElement)
        {
            return iWebElement.GetAttribute(HtmlTagAttribute.Style);
        }

        public static string Title(this IWebElement iWebElement)
        {
            return iWebElement.GetAttribute(HtmlTagAttribute.Title);
        }

        public static string Type(this IWebElement iWebElement)
        {
            return iWebElement.GetAttribute(HtmlTagAttribute.Type);
        }

        public static string Value(this IWebElement iWebElement)
        {
            return iWebElement.GetAttribute(HtmlTagAttribute.Value);
        }

        #endregion
        
    }
}
