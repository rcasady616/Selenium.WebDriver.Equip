using System;

namespace Selenium.WebDriver.Equip
{
    public class PageNotLoadedException : Exception
    {
        public PageNotLoadedException()
        {
        }
        public PageNotLoadedException(string message)
            : base(message)
        {
        }
        public PageNotLoadedException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
