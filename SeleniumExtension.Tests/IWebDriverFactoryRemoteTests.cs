using System;
using System.Diagnostics;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using SeleniumExtension.Nunit;
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
                string sessionId = "";
                var sessionIdProperty = typeof(RemoteWebDriver).GetProperty("SessionId", BindingFlags.Instance | BindingFlags.NonPublic);
                if (sessionIdProperty != null)
                {
                     sessionId = sessionIdProperty.GetValue(_driver, null).ToString();
                    if (sessionId == null)
                    {
                        Trace.TraceWarning("Could not obtain SessionId.");
                    }
                    else
                    {
                        Trace.TraceInformation("SessionId is " + sessionId.ToString());
                    }
                }

                //var sessionId = ((RemoteWebDriver)_driver).Capabilities.GetCapability("webdriver.remote.sessionid");
                //var sessionId = ((RemoteWebDriver) (_driver)).sessionId;
                //string sessionId = ((RemoteWebDriver)_driver).GetSessionId().toString();
                //var sessionId = _driver.ToString();
               // ValueType t = _driver..Capabilities.GetCapability("webdriver.remote.sessionid");
                _driver.Close();
                _driver.Dispose();
                if (TestContext.CurrentContext.Result.Status == TestStatus.Failed)
                {
                    Job.UpDateJob(sessionId, false);
                   // new TestCapture(_driver).CaptureWebPage(GetCleanTestName(TestContext.CurrentContext.Test.FullName) + ".Failed");
                }
                Job.UpDateJob(sessionId, true);
              
              
            }
        }

        [Test]
        public void GetRemoteWebDriverFirefoxTest()
        {
            _driver = WebDriverFactory.GetRemoteWebDriver(null, "http://rickcasady.blogspot.com/");
            Assert.AreEqual(typeof(RemoteWebDriver), _driver.GetType());
        }
    }

   

}
