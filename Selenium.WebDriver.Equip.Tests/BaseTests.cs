using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using Selenium.WebDriver.Equip.Settings;
using Selenium.WebDriver.Equip.WebDriver;

namespace Selenium.WebDriver.Equip.Tests
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(FirefoxDriver))]
    [TestFixture(typeof(FirefoxDriver))]
    public class BaseTests<TDriver> where TDriver : IWebDriver, new()
    {
        public IWebDriver Driver;

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
            var seleniumSettings = new SeleniumSettings().Deserialize();
            var driverType = seleniumSettings.DriverType;
            switch (driverType)
            {
                case DriverType.SauceLabs:

                    Driver = Driver.GetSauceDriver<ChromeDriver>(TestContext.CurrentContext.Test.Name);
                    break;
                default:
                    Driver = Driver.GetDriver<ChromeDriver>();
                    break;
            }
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
