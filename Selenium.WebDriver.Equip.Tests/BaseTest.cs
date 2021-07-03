using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using NUnit.Framework.Interfaces;
using System.Threading;
using OpenQA.Selenium.Chrome;
using System;
using OpenQA.Selenium.Remote;

namespace Selenium.WebDriver.Equip.Tests
{
    /// <summary>
    /// A base fixture for Selenium testing single browser per test
    /// </summary>
    [Parallelizable(ParallelScope.Fixtures)]
    [TestFixture]
    public class BaseTest
    {
        /// <summary>
        /// Instance of the browser used for the test
        /// </summary>
        public IWebDriver Driver;
       // public EnvironmentManager EnvManager;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {

        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
        }

        /// <summary>
        /// Initialize the browser before the test starts
        /// </summary>
        [SetUp]
        public void SetupTest()
        {
            Driver = Driver.GetSauceDriver<ChromeDriver>(TestContext.CurrentContext.Test.Name);
            // EnvManager = new EnvironmentManager();
            // Driver = EnvManager.CreateDriverInstance(TestContext.CurrentContext.Test.Name);
        }

        /// <summary>
        /// Dereference the instance of the browser
        /// Takes screenshot and gets page source when failure occurs 
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            if (Driver != null)
            {
                var outcome = TestContext.CurrentContext.Result.Outcome == ResultState.Success;
                if (!outcome)
                {
                    new TestCapture(Driver, TestContext.CurrentContext.Test.GetCleanName() + ".Failed").CaptureWebPage();
                    UpDateJob(bool.Parse(outcome.ToString()));
                }
                    if (Driver != null) Driver.Quit();
                Driver = null;
            }
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
