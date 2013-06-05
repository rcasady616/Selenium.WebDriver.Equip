
namespace OpenQA.Selenium
{
    public static class ISearchContextExtention
    {
        public static IWebElement FindElement(this ISearchContext iSearchContext, string id)
        {
            return iSearchContext.FindElement(By.Id(id));
        }
    }
}
