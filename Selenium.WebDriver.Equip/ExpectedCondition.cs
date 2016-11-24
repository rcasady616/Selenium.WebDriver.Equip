using System;
using System.Threading;
using OpenQA.Selenium;

namespace Selenium.WebDriver.Equip
{
    /// <summary>
    /// Defines Expected conditions of a <see cref="IWebElement"> 
    /// </summary>
    public static class ExpectedCondition
    {

        /// <summary>
        /// An expectation for checking that an element is not present on the DOM of a page.
        /// This method returns faster than <see cref="ElementExists"/> when the <see cref="IWebElement"/> is expected to not be present
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement">IWebElements</see> is not present; otherwise, <see langword="false"/></returns>
        public static Func<ISearchContext, bool> ElementNotExists(By locator)
        {
            return (searchContext) =>
                {
                    try
                    {
                        return !searchContext.ElementExists(locator);
                    }
                    catch (StaleElementReferenceException)
                    {
                        return true;
                    }
                };
        }

        /// <summary>
        /// An expectation for checking that an elements text.
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="text">The text an <see cref="IWebElement"/> should be</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement">IWebElements</see> text equals; otherwise, <see langword="false"/></returns>
        public static Func<ISearchContext, bool> ElementTextEquals(By locator, string text)
        {
            return (searchContext) => { return searchContext.FindElement(locator).Text == text; };
        }

        /// <summary>
        /// An expectation for checking that an elements text.
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="text">The text an <see cref="IWebElement"/> should not be</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement">IWebElements</see> text not equals; otherwise, <see langword="false"/></returns>
        public static Func<ISearchContext, bool> ElementTextNotEquals(By locator, string text)
        {
            return (searchContext) => { return searchContext.FindElement(locator).Text != text; };
        }

        /// <summary>
        /// An expectation for checking that an element contains specific text.
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="text">The text an <see cref="IWebElement"/> should contain</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> contains the text; otherwise, <see langword="false"/></returns>
        public static Func<ISearchContext, bool> ElementTextContains(By locator, string text)
        {
            return (searchContext) => { return searchContext.FindElement(locator).Text.Contains(text); };
        }

        /// <summary>
        /// An expectation for checking that an element text not contains specific text.
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="text">The text an <see cref="IWebElement"/> should contain</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> not contains the text; otherwise, <see langword="false"/></returns>
        public static Func<ISearchContext, bool> ElementTextNotContains(By locator, string text)
        {
            return (searchContext) => { return !searchContext.FindElement(locator).Text.Contains(text); };
        }

        /// <summary>
        /// An expectation for checking that an attribute value.
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="htmlTagAttribute">The the name of the attribute</param>
        /// <param name="attributeValue">The <see cref="string"/> value the attribute should be equal to.</param>
        /// <returns><see langword="true"/> if the attribute value equals; otherwise, <see langword="false"/></returns>
        public static Func<ISearchContext, bool> ElementAttributeEquals(By locator, string htmlTagAttribute, string attributeValue)
        {
            return (searchContext) =>
                {
                    if (!searchContext.ElementExists(locator))
                        return false;
                    return searchContext.FindElement(locator).GetAttribute(htmlTagAttribute) == attributeValue;
                };
        }

        /// <summary>
        /// An expectation for checking that an attribute  os not equal a value.
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="htmlTagAttribute">The the name of the attribute</param>
        /// <param name="attributeValue">The <see cref="string"/> value the attribute should be not equal to.</param>
        /// <returns><see langword="true"/> if the attribute values not equal; otherwise, <see langword="false"/></returns>
        public static Func<ISearchContext, bool> ElementAttributeNotEquals(By locator, string htmlTagAttribute, string attributeValue)
        {
            return (searchContext) => { return searchContext.FindElement(locator).GetAttribute(htmlTagAttribute) != attributeValue; };
        }

        /// <summary>
        /// An expectation for checking that an element is not present on the DOM of a page and not visible
        /// This method returns faster than <see cref="ElementIsVisible"/> when the <see cref="IWebElement"/> is expected to not be visible
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> is not present; otherwise, <see langword="false"/></returns>
        public static Func<ISearchContext, bool> ElementNotVisible(By locator)
        {
            return (searchContext) =>
            {
                bool notVisiable = false;
                try
                {
                    if (!searchContext.ElementExists(locator))
                        return true;
                    notVisiable = !ElementVisible(searchContext.FindElement(locator));
                }
                catch (StaleElementReferenceException)
                {
                    return true;
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
                catch (WebDriverTimeoutException)
                {
                    return true;
                }
                return notVisiable;
            };
        }

        public static Func<ISearchContext, IWebElement> ElementIsVisible(By locator)
        {
            return (searchContext) =>
            {
                try
                {
                    return ElementIfVisible(searchContext.FindElement(locator));
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }

        public static Func<ISearchContext, IWebElement> ElementExists(By locator)
        {
            return (iSearchContext) =>
            {
                try
                {
                    iSearchContext.FindElement(locator);
                }catch(NoSuchElementException)
                {
                }
                return null;
            };
        }

        public static Func<ISearchContext, bool> ElementCountIs(By locator, int expectedCount)
        {
            return (iSearchContext) => iSearchContext.FindElements(locator).Count == expectedCount;
        }

        public static Func<ISearchContext, bool> ElementRested(Func<ISearchContext, bool> condition, int restTimeInSeconds)
        {
            int restCtr = 0;
            return (searchContext) =>
            {
                while (searchContext.WaitUntil(condition, 0))
                {
                    if (restCtr >= restTimeInSeconds)
                        return true;
                    restCtr++;
                    Thread.Sleep(1000);
                }
                return false;
            };
        }

        public static Func<IWebDriver, bool> AlertTextEquals(string text)
        {
            return (iWebDriver) =>
            {
                try
                {
                    IAlert alert = iWebDriver.SwitchTo().Alert();
                    if (alert != null)
                        return (text == alert.Text);

                }
                catch (NoAlertPresentException)
                {
                }
                return false;
            };
        }

        public static Func<IWebDriver, bool> AlertTextContains(string text)
        {
            return (iWebDriver) =>
            {
                try
                {
                    IAlert alert = iWebDriver.SwitchTo().Alert();
                    if (alert != null)
                        return (alert.Text.Contains(text));
                }
                catch (NoAlertPresentException)
                {
                }
                return false;
            };
        }

        private static IWebElement ElementIfVisible(IWebElement element)
        {
            return element.Displayed ? element : null;
        }

        private static bool ElementVisible(IWebElement element)
        {
            if (element.Displayed)
                return true;
            return false;
        }
    }
}
