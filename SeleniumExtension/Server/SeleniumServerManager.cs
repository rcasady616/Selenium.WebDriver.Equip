using System;
using Selenium;

namespace SeleniumExtension.Server
{
    public class SeleniumServerManager : IDisposable
    {
        private ISelenium _selenium = null;

        public SeleniumServerManager(string seleniumHost, int seleniumPort, string browserName, string browserUrl)
        {
            _selenium = new DefaultSelenium(seleniumHost, seleniumPort, browserName, browserUrl);
        }

        public void Start()
        {
        }

        public void Stop()
        {
        }

        public void Dispose()
        {
           Stop();
           _selenium = null;
        }
    }
}
