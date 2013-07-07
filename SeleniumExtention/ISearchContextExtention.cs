using System;
using OpenQA.Selenium.Support.UI;
using SeleniumExtention;

namespace OpenQA.Selenium
{
    public static class ISearchContextExtention
    {
        /// <summary>
        /// Finds the first <see cref="IWebElement"/> using its id
        /// </summary>
        /// <param name="id">The id of the <see cref="IWebElement"/></param>
        /// <returns>The first matching <see cref="IWebElement"/> on the current context.</returns>
        /// <exception cref="NoSuchElementException">If no element matches the criteria.</exception>
        public static IWebElement FindElement(this ISearchContext iSearchContext, string id)
        {
            return iSearchContext.FindElement(By.Id(id));
        }

        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to meet specific conditions
        /// </summary>
        /// <param name="condition">The <see cref="ExpectedConditions"/> criteria to <see cref="WebDriverWait"/> for</param>
        /// <param name="waitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the condition</param>
        /// <returns><see langword="true"/> if the condition is meet; otherwise, <see langword="false"/>.</returns>
        public static bool WaitUntil<T>(this ISearchContext iSearchContext, Func<IWebDriver, T> condition, int waitTimeInSeconds = 10)
        {
            var driver = (IWebDriver)iSearchContext;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitTimeInSeconds));
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
        /// <param name="waitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to exist</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> exists; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilExists(this ISearchContext iSearchContext, By locator, int waitTimeInSeconds = 10)
        {
            return WaitUntil(iSearchContext, ExpectedConditions.ElementExists(locator), waitTimeInSeconds);
        }

        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to not be present on the page DOM
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="waitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to exist</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> exists; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilOblivion(this ISearchContext iSearchContext, By locator, int waitTimeInSeconds = 10)
        {
            return WaitUntil(iSearchContext, ExpectedCondition.ElementOblivion(locator), waitTimeInSeconds);
        }

        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to be visible in the page
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="waitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to become visible</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> is visible; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilVisible(this ISearchContext iSearchContext, By locator, int waitTimeInSeconds = 10)
        {
            return WaitUntil(iSearchContext, ExpectedConditions.ElementIsVisible(locator), waitTimeInSeconds);
        }

        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to not be visible in the page
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="waitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to become not visible</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> is not visible; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilNotVisible(this ISearchContext iSearchContext, By locator, int waitTimeInSeconds = 10)
        {
            return WaitUntil(iSearchContext, ExpectedCondition.ElementNotVisible(locator), waitTimeInSeconds);
        }

        /// <summary>
        /// Waits for a page title to be a specific text
        /// </summary>
        /// <param name="title">The title of the page</param>
        /// <param name="waitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to become visible</param>
        /// <returns><see langword="true"/> if the title is a match; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilTitleIs(this ISearchContext iSearchContext, string title, int waitTimeInSeconds = 10)
        {
            return WaitUntil(iSearchContext, ExpectedConditions.TitleIs(title), waitTimeInSeconds);
        }

        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to have specific text
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="text">The text it should equal</param>
        /// <param name="waitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to become visible</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> text is a match; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilTextEquals(this ISearchContext iSearchContext, By locator, string text, int waitTimeInSeconds = 10)
        {
            return WaitUntil(iSearchContext, ExpectedCondition.ElementTextEquals(locator, text), waitTimeInSeconds);
        }

        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to have contain specific text
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <param name="text">The text it should contain</param>
        /// <param name="waitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to become visible</param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> contains the text; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilTextContains(this ISearchContext iSearchContext, By locator, string text, int waitTimeInSeconds = 10)
        {
            return WaitUntil(iSearchContext, ExpectedCondition.ElementTextContains(locator,text), waitTimeInSeconds);
        } 

        /// <summary>
        /// Check is a <see cref="IWebElement"/> to be exists in the page DOM
        /// </summary>
        /// <param name="locator">The <see cref="By"/> locator of the <see cref="IWebElement"/></param>
        /// <returns><see langword="true"/> if the <see cref="IWebElement"/> exists; otherwise, <see langword="false"/></returns>
        public static bool ElementExists(this ISearchContext iSearchContext, By locator)
        {
            return iSearchContext.FindElements(locator).Count > 0;
        }
    }
}
