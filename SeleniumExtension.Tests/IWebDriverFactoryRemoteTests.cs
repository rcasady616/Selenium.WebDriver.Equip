using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using SeleniumExtension.Server;

namespace SeleniumExtension.Tests
{
    [TestFixture]
    public class IWebDriverFactoryRemoteTests
    {
        private IWebDriver _driver;
        [SetUp]
        public void SetupTest()
        {
            if (SeleniumServer.isSeleniumServerRunning())
                throw new Exception("Server is already running, please shut down before running tests again!");
            SeleniumServer.Start();
        }

        [TearDown]
        public void TearDown()
        {
            if (_driver != null)
            {
                _driver.Close();
                _driver.Quit();
            }
            if (false == SeleniumServer.Stop())
                throw new Exception("Server wasn't stopped, and will could cause problems running tests");
        }

        [Test]
        public void GetRemoteWebDriverFirefoxTest()
        {
            _driver = IWebDriverFactory.GetRemoteWebDriver(null, "http://rickcasady.blogspot.com/");
            Assert.AreEqual(typeof(RemoteWebDriver), _driver.GetType());
        }
    }
}
