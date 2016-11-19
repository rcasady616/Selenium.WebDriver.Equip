using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace Selenium.WebDriver.Equip.Tests
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
                var passed = (TestContext.CurrentContext.Result.Outcome == ResultState.Success);
                try
                {
                    ((IJavaScriptExecutor)_driver).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
                }
                finally
                {
                    if (_driver != null)
                        _driver.Quit();
                }
            }
        }

        [Test]
        [Ignore("")]
        public void GetSauceTest()
        {
            _driver = WebDriverFactory.GetSauceDriver(TestContext.CurrentContext.Test.Name, url: "http://rickcasady.blogspot.com/");
            Assert.AreEqual(typeof(RemoteWebDriver), _driver.GetType());
        }
    }
}
