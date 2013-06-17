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

        public static bool WaitUntilExists(this ISearchContext iSearchContext, By by, int waitTimeInSeconds = 10)
        {
            int ctr = 0;
            bool ret = false;
            do
            {
                try
                {
                    ctr++;
                    System.Threading.Thread.Sleep(500);
                    ret = iSearchContext.FindElement(by).Exists();
                }
                catch (NoSuchElementException ex)
                {
                    //ret = false;
                }
            } while (!ret && ctr < waitTimeInSeconds * 2);

            return ret;
        }

        
    }
}
