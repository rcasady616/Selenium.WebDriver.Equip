using System;
using OpenQA.Selenium;

namespace SeleniumExtension
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
        public static Func<IWebDriver, bool> ElementOblivion(By locator)
        {
            return (driver) =>
                {
                    try
                    {
                        return !driver.ElementExists(locator);
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
        public static Func<IWebDriver, bool> ElementTextEquals(By locator, string text)
        {
            return (driver) => { return driver.FindElement(locator).Text == text; };
        }

        /// <summary>
        /// An expectation for checking that an element contains specific text.
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="text">The text an <see cref="IWebElement"/> should contain</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> contains the text; otherwise, <see langword="false"/></returns>
        public static Func<IWebDriver, bool> ElementTextContains(By locator, string text)
        {
            return (driver) => { return driver.FindElement(locator).Text.Contains(text); };
        }

        /// <summary>
        /// An expectation for checking that an element is not present on the DOM of a page and not visible
        /// This method returns faster than <see cref="ElementIsVisible"/> when the <see cref="IWebElement"/> is expected to not be visible
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> is not present; otherwise, <see langword="false"/></returns>
        public static Func<IWebDriver, bool> ElementNotVisible(By locator)
        {
            return (driver) =>
            {
                try
                {
                    return null != ElementIfVisible(driver.FindElement(locator));
                }
                catch (StaleElementReferenceException)
                {
                    return true;
                }
            };
        }

        private static IWebElement ElementIfVisible(IWebElement element)
        {
            if (element.Displayed)
                return element;
            return null;
        }
    }
}
