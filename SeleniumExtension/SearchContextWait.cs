using System;
using OpenQA.Selenium.Support.UI;

namespace OpenQA.Selenium
{
    public class SearchContextWait : DefaultWait<ISearchContext>
    {
        public SearchContextWait(ISearchContext element, TimeSpan timeout)
            : this(new SystemClock(), element, timeout, DefaultSleepTimeout)
        {
        }

        public SearchContextWait(IClock clock, ISearchContext element, TimeSpan timeout, TimeSpan sleepInterval)
            : base(element, clock)
        {
            this.Timeout = timeout;
            this.PollingInterval = sleepInterval;
            this.IgnoreExceptionTypes(typeof(NotFoundException));
        }

        private static TimeSpan DefaultSleepTimeout
        {
            get { return TimeSpan.FromMilliseconds(500); }
        }
    }
}
