using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;

namespace Selenium.WebDriver.Equip.Manager.Tests
{
    [TestFixture]
    public class SauceServerTests
    {
        IWebDriver driver;

        [SetUp]
        public void SetupTest()
        {
        }

        [TearDown]
        public void TearDown()
        {
            var outcome = TestContext.CurrentContext.Result.Outcome == ResultState.Success;

            if (outcome != null)
                UpDateJob(Boolean.Parse(outcome.ToString()));
            driver.Quit();
        }

        [Test]
        public void FirefoxDriver()
        {
           // var options =new  DriverOptions();
            driver = driver.GetRDriver<FirefoxDriver>(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ChromeDriver()
        {
            // var options =new  DriverOptions();
            driver = driver.GetRDriver<ChromeDriver>(TestContext.CurrentContext.Test.Name);
        }

        public void UpDateJob(bool outcome)
        {
            var sessionId = (string)((RemoteWebDriver)driver).Capabilities.GetCapability("webdriver.remote.sessionid");
            try
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("sauce:job-result=" + (outcome ? "passed" : "failed"));
            }
            finally
            {
            }
        }
    }
}
