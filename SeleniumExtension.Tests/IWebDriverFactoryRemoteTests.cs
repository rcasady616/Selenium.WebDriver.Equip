using System;
using System.Diagnostics;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using SeleniumExtension.SauceLabs;
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

        }

        [TearDown]
        public void TearDown()
        {
            if (_driver != null)
            {
                var passed = TestContext.CurrentContext.Result.Status == TestStatus.Passed;
                try
                {
                    ((IJavaScriptExecutor) _driver).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
                }
                finally
                {
                    if (_driver != null)
                        _driver.Quit();
                }
            }
        }

        [Test]
        public void GetRemoteWebDriverFirefoxTest()
        {
            _driver = WebDriverFactory.GetRemoteWebDriver(null, "http://rickcasady.blogspot.com/");
            Assert.AreEqual(typeof(RemoteWebDriver), _driver.GetType());
        }
        [Test]
        public void GetSauceTest()
        {
            _driver = WebDriverFactory.GetSauceDriver(url: "http://rickcasady.blogspot.com/");
            Assert.AreEqual(typeof(RemoteWebDriver), _driver.GetType());
        }
    }
}
