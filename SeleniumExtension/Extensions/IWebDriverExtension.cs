using System;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;
using SeleniumExtension;

namespace OpenQA.Selenium
{
    public static class IWebDriverExtention
    {
        #region Expected conditions to wait

        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to meet specific conditions
        /// </summary>
        /// <param name="condition">The <see cref="ExpectedConditions"/> criteria to <see cref="WebDriverWait"/> for</param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the condition</param>
        /// <returns><see langword="true"/> if the condition is meet; otherwise, <see langword="false"/>.</returns>
        public static bool WaitUntil<T>(this IWebDriver iWebDriver, Func<IWebDriver, T> condition, int maxWaitTimeInSeconds = 10)
        {
            var driver = (IWebDriver)iWebDriver;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(maxWaitTimeInSeconds));
            try
            {
                wait.Until(condition);
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to be exists in the page DOM
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to exist</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> exists; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilExists(this IWebDriver iWebDriver, By locator, int maxWaitTimeInSeconds = 10)
        {
            return WaitUntil(iWebDriver, ExpectedConditions.ElementExists(locator), maxWaitTimeInSeconds);
        }

        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to not be present on the page DOM
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to exist</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> exists; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilNotExists(this IWebDriver iWebDriver, By locator, int maxWaitTimeInSeconds = 10)
        {
            return WaitUntil(iWebDriver, ExpectedCondition.ElementNotExists(locator), maxWaitTimeInSeconds);
        }

        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to be visible in the page
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to become visible</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> is visible; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilVisible(this IWebDriver iWebDriver, By locator, int maxWaitTimeInSeconds = 10)
        {
            return WaitUntil(iWebDriver, ExpectedConditions.ElementIsVisible(locator), maxWaitTimeInSeconds);
        }

        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to be visible in the page
        /// </summary>
        /// <param name="locator">The <see cref="List"/>/<<see cref="By"/>/> locators of the <see cref="IWebElement"/>s</param>
        /// <param name="waitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to become visible</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> is visible; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilVisible(this IWebDriver iWebDriver, List<By> locator, int waitTimeInSeconds = 10)
        {
            var failedLocs = new List<By>();
            foreach (var loc in locator)
            {
                if (!WaitUntil(iWebDriver, ExpectedConditions.ElementIsVisible(loc), waitTimeInSeconds))
                    failedLocs.Add(loc);
            }
            if (failedLocs.Count == 0)
                return true;
            if (failedLocs.Count > 0 && waitTimeInSeconds > 1)
                return WaitUntilVisible(iWebDriver, failedLocs, waitTimeInSeconds / 2);
            if (failedLocs.Count > 0 && waitTimeInSeconds <= 1)
                return false;
            return true;
        }

        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to not be visible in the page
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to become not visible</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> is not visible; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilNotVisible(this IWebDriver iWebDriver, By locator, int maxWaitTimeInSeconds = 10)
        {
            return WaitUntil(iWebDriver, ExpectedCondition.ElementNotVisible(locator), maxWaitTimeInSeconds);
        }

        /// <summary>
        /// Waits for a page title to be a specific text
        /// </summary>
        /// <param name="title">The title of the page</param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to become visible</param>
        /// <returns><see langword="true"/> if the title is a match; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilTitleIs(this IWebDriver iWebDriver, string title, int maxWaitTimeInSeconds = 10)
        {
            return WaitUntil(iWebDriver, ExpectedConditions.TitleIs(title), maxWaitTimeInSeconds);
        }

        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to have specific text
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="text">The text it should equal</param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to become visible</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> text is a match; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilTextEquals(this IWebDriver iWebDriver, By locator, string text, int maxWaitTimeInSeconds = 10)
        {
            return WaitUntil(iWebDriver, ExpectedCondition.ElementTextEquals(locator, text), maxWaitTimeInSeconds);
        }

        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to not have specific text
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="text">The text it should equal</param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to become visible</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> text is not a match; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilTextNotEquals(this IWebDriver iWebDriver, By locator, string text, int maxWaitTimeInSeconds = 10)
        {
            return WaitUntil(iWebDriver, ExpectedCondition.ElementTextNotEquals(locator, text), maxWaitTimeInSeconds);
        }

        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to have contain specific text
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="text">The text it should contain</param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to become visible</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> contains the text; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilTextContains(this IWebDriver iWebDriver, By locator, string text, int maxWaitTimeInSeconds = 10)
        {
            return WaitUntil(iWebDriver, ExpectedCondition.ElementTextContains(locator, text), maxWaitTimeInSeconds);
        }

        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to not contain specific text
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="text">The text it should contain</param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to become visible</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> not contains the text; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilTextNotContains(this IWebDriver iWebDriver, By locator, string text, int maxWaitTimeInSeconds = 10)
        {
            return WaitUntil(iWebDriver, ExpectedCondition.ElementTextNotContains(locator, text), maxWaitTimeInSeconds);
        }

        #endregion
    }
}
