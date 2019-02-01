using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using Selenium.WebDriver.Equip;

namespace OpenQA.Selenium
{
    public static class SearchContextExtension
    {
        /// <summary>
        /// Check is a <see cref="IWebElement"/> to be exists in the page DOM
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> exists; otherwise, <see langword="false"/></returns>
        public static bool ElementExists(this ISearchContext iSearchContext, By locator)
        {
            return iSearchContext.WaitUntilExists(locator, 0);
        }

        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to meet specific conditions
        /// </summary>
        /// <param name="condition">The <see cref="ExpectedConditions"/> criteria to <see cref="SearchContextWait"/> for</param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the condition</param>
        /// <returns><see langword="true"/> if the condition is meet; otherwise, <see langword="false"/>.</returns>
        public static bool WaitUntil<T>(this ISearchContext iSearchContext, Func<ISearchContext, T> condition, int maxWaitTimeInSeconds = GlobalConstants.MaxWaitTimeInSeconds)
        {
            var searchContext = (ISearchContext)iSearchContext;
            var wait = new SearchContextWait(searchContext, TimeSpan.FromSeconds(maxWaitTimeInSeconds));
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

        #region Expected conditions to wait

        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to exists in the page DOM
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to exist</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> exists; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilExists(this ISearchContext iSearchContext, By locator, int maxWaitTimeInSeconds = GlobalConstants.MaxWaitTimeInSeconds)
        {
            return iSearchContext.WaitUntil(ExpectedCondition.ElementExists(locator), maxWaitTimeInSeconds);
        }

        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to not exists in the page DOM
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to exist</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> exists; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilNotExists(this ISearchContext iSearchContext, By locator, int maxWaitTimeInSeconds = GlobalConstants.MaxWaitTimeInSeconds)
        {
            return iSearchContext.WaitUntil(ExpectedCondition.ElementNotExists(locator), maxWaitTimeInSeconds);
        }

        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to be visible in the page
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to become visible</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> is visible; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilVisible(this ISearchContext iSearchContext, By locator, int maxWaitTimeInSeconds = GlobalConstants.MaxWaitTimeInSeconds)
        {
            return iSearchContext.WaitUntil(ExpectedCondition.ElementIsVisible(locator), maxWaitTimeInSeconds);
        }

        /// <summary>
        /// Waits for a <see cref="List{T}"/>/<<see cref="IWebElement"/>/> to be visible in the page
        /// </summary>
        /// <param name="locator">The <see cref="List"/>/<<see cref="By"/>/> locators of the <see cref="IWebElement"/>s</param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to become visible</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> is visible; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilVisible(this ISearchContext iSearchContext, List<By> locators, int maxWaitTimeInSeconds = GlobalConstants.MaxWaitTimeInSeconds)
        {
            var failedLocs = new List<By>();
            foreach (var loc in locators)
            {
                if (!iSearchContext.WaitUntil(ExpectedCondition.ElementIsVisible(loc), maxWaitTimeInSeconds))
                    failedLocs.Add(loc);
            }
            if (failedLocs.Count == 0)
                return true;
            if (failedLocs.Count > 0 && maxWaitTimeInSeconds > 1)
                return iSearchContext.WaitUntilVisible(failedLocs, maxWaitTimeInSeconds / 2);
            if (failedLocs.Count > 0 && maxWaitTimeInSeconds <= 1)
                return false;
            return true;
        }

        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to not be visible in the page
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to become not visible</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> is not visible; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilNotVisible(this ISearchContext iSearchContext, By locator, int maxWaitTimeInSeconds = GlobalConstants.MaxWaitTimeInSeconds)
        {
            if (!iSearchContext.ElementExists(locator))
            {
                Console.WriteLine("Element did not exist, element cannot be visable, by: {0}", locator);
                return true;
            }
            return iSearchContext.WaitUntil(ExpectedCondition.ElementNotVisible(locator), maxWaitTimeInSeconds);
        }

        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to not be visible in the page
        /// </summary>
        /// <param name="locators">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to become not visible</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> is not visible; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilNotVisible(this ISearchContext iSearchContext, List<By> locators, int maxWaitTimeInSeconds = GlobalConstants.MaxWaitTimeInSeconds)
        {
            var failedLocs = new List<By>();
            foreach (var loc in locators)
            {
                if (iSearchContext.WaitUntil(ExpectedCondition.ElementNotVisible(loc), maxWaitTimeInSeconds))
                    failedLocs.Add(loc);
            }
            if (failedLocs.Count == 0)
                return true;
            if (failedLocs.Count > 0 && maxWaitTimeInSeconds > 1)
                return iSearchContext.WaitUntilNotVisible(failedLocs, maxWaitTimeInSeconds / 2);
            if (failedLocs.Count > 0 && maxWaitTimeInSeconds <= 1)
                return false;
            return true;
        }

        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to have specific text
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="text">The text it should equal</param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to become visible</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> text is a match; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilTextEquals(this ISearchContext iSearchContext, By locator, string text, int maxWaitTimeInSeconds = GlobalConstants.MaxWaitTimeInSeconds)
        {
            return iSearchContext.WaitUntil(ExpectedCondition.ElementTextEquals(locator, text), maxWaitTimeInSeconds);
        }

        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to not have specific text
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="text">The text it should equal</param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to become visible</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> text is not a match; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilTextNotEquals(this ISearchContext iSearchContext, By locator, string text, int maxWaitTimeInSeconds = GlobalConstants.MaxWaitTimeInSeconds)
        {
            if (!iSearchContext.WaitUntilExists(locator, maxWaitTimeInSeconds))
            {
                Console.WriteLine("Element did not exist, could not compare text of element with locator: {0}", locator);
                return false;
            }
            return iSearchContext.WaitUntil(ExpectedCondition.ElementTextNotEquals(locator, text), maxWaitTimeInSeconds);
        }

        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to have contain specific text
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="text">The text it should contain</param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to become visible</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> contains the text; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilTextContains(this ISearchContext iSearchContext, By locator, string text, int maxWaitTimeInSeconds = GlobalConstants.MaxWaitTimeInSeconds)
        {
            return iSearchContext.WaitUntil(ExpectedCondition.ElementTextContains(locator, text), maxWaitTimeInSeconds);
        }

        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to have specific attribute value
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="text">The value the <see cref="IWebElement"/> attribute should equal</param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to become visible</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> attribute value is a match; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilAttributeEquals(this ISearchContext iSearchContext, By locator, string htmlTagAttribute,
                                                    string attributeValue, int maxWaitTimeInSeconds = GlobalConstants.MaxWaitTimeInSeconds)
        {
            return iSearchContext.WaitUntil(ExpectedCondition.ElementAttributeEquals(locator, htmlTagAttribute, attributeValue), maxWaitTimeInSeconds);
        }

        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to not have specific attribute value
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="text">The value the <see cref="IWebElement"/> attribute not should equal</param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to become visible</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> attribute value is not a match; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilAttributeNotEquals(this ISearchContext iSearchContext, By locator, string htmlTagAttribute,
                                                     string attributeValue, int maxWaitTimeInSeconds = GlobalConstants.MaxWaitTimeInSeconds)
        {
            if (!iSearchContext.WaitUntilExists(locator, maxWaitTimeInSeconds))
                return false;
            return iSearchContext.WaitUntil(ExpectedCondition.ElementAttributeNotEquals(locator, htmlTagAttribute, attributeValue), maxWaitTimeInSeconds);
        }

        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to not contain specific text
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="text">The text it should contain</param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to become visible</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> not contains the text; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilTextNotContains(this ISearchContext iSearchContext, By locator, string text, int maxWaitTimeInSeconds = GlobalConstants.MaxWaitTimeInSeconds)
        {
            if (!iSearchContext.WaitUntilExists(locator, maxWaitTimeInSeconds))
            {
                Console.WriteLine("Element did not exist, could not compare text of element with locator: {0}", locator);
                return false;
            }
            return iSearchContext.WaitUntil(ExpectedCondition.ElementTextNotContains(locator, text), maxWaitTimeInSeconds);
        }

        public static bool WaitUntilRested(this ISearchContext iSearchContext, Func<ISearchContext, bool> condition, int restTimeInSeconds, int maxWaitTimeInSeconds = GlobalConstants.MaxWaitTimeInSeconds)
        {
            if (restTimeInSeconds > maxWaitTimeInSeconds)
                throw new ArgumentException("The restTimeInSeconds cannot be greater than the maxWaitTimeInSeconds");
            bool rested = false;
            var timeSpan = new Stopwatch();
            timeSpan.Start();
            while (timeSpan.Elapsed.Seconds <= 10 && rested == false)
            {
                if (iSearchContext.WaitUntil(ExpectedCondition.ElementRested(condition, restTimeInSeconds), maxWaitTimeInSeconds))
                    rested = true;
            }
            timeSpan.Stop();

            return rested;
        }

        /// <summary>
        /// An expectation for checking that an element contains specific text.
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="expectedCount">The expected number of <see cref="IWebElement"/> that are found from the <see cref="By"/> locator</param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> count is</param>
        /// <returns><see langword="true"/> there are the expected number of <see cref="IWebElement"/>'s; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilElementCountIs(this ISearchContext iSearchContext, By locator, int expectedCount, int maxWaitTimeInSeconds = GlobalConstants.MaxWaitTimeInSeconds)
        {
            return iSearchContext.WaitUntil(ExpectedCondition.ElementCountIs(locator, expectedCount), maxWaitTimeInSeconds);
        }

        #endregion

    }
}