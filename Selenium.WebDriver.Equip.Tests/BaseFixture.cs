using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using Selenium.WebDriver.Equip.Server;
using Selenium.WebDriver.Equip.Settings;
using Selenium.WebDriver.Equip.WebDriver;
using System;
using System.IO;
using System.Reflection;

namespace Selenium.WebDriver.Equip.Tests
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(FirefoxDriver))]
    public class BaseFixture<TDriver> where TDriver : IWebDriver, new()
    {
        public IWebDriver Driver;

        private DriverType driverType;
        private string remoteBrowserName;
        private BrowserName browserName;
        private string remoteBrowserVersion;
        private string remoteOsPlatform;
        private SeleniumServerProxy remoteServer;
        private SeleniumSettings seleniumSettings;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
          

        }
             

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
        }

        [SetUp]
        public void SetupTest()
        {
            Driver = Driver.GetSauceDriver<TDriver>(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void TearDown()
        {
            var outcome = TestContext.CurrentContext.Result.Outcome == ResultState.Success;

            if (outcome != null)
                UpDateJob(Boolean.Parse(outcome.ToString()));
            if (Driver != null) Driver.Quit();
        }


        public void UpDateJob(bool outcome)
        {
            var sessionId = (string)((RemoteWebDriver)Driver).Capabilities.GetCapability("webdriver.remote.sessionid");
            try
            {
                ((IJavaScriptExecutor)Driver).ExecuteScript("sauce:job-result=" + (outcome ? "passed" : "failed"));
            }
            finally
            {
            }
        }
    }
}
