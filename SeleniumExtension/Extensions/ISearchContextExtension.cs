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
