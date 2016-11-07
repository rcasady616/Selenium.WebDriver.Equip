using System;

namespace Selenium.WebDriver.Equip
{
    /// <summary>
    /// Represents errors that occur when pages arent loaded or features in pages are not loaded
    /// </summary>
    public class PageNotLoadedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageNotLoadedException"/> class.
        /// </summary>
        public PageNotLoadedException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageNotLoadedException"/> class.
        /// </summary>
        /// <param name="message">The <see cref="string"/>  exception message that describs the error.</param>
        public PageNotLoadedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageNotLoadedException"/> class.
        /// </summary>
        /// <param name="message">The <see cref="string"/> exception message that describs the error.</param>
        /// <param name="exception">The <see cref="Exception"/> that is the cause of the current exception.</param>
        public PageNotLoadedException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
