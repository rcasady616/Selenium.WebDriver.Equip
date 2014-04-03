using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using SeleniumExtension.SauceLabs;

namespace SeleniumExtension.Nunit
{
    /// <summary>
    /// A base fixture for Selenium testing single browser per test
    /// </summary>
    [TestFixture]
    public class BaseTest
    {
        /// <summary>
        /// Instance of the browser used for the test
        /// </summary>
        public IWebDriver Driver;

        /// <summary>
        /// Initialize the browser before the test starts
        /// </summary>
        [SetUp]
        public void SetupTest()
        {
            Driver = WebDriverFactory.GetBrowser();
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
                var sessionId = (string)((RemoteWebDriver)Driver).Capabilities.GetCapability("webdriver.remote.sessionid");
                if (TestContext.CurrentContext.Result.Status == TestStatus.Failed)
                {
                    Job.UpDateJob(sessionId,false);
                    new TestCapture(Driver).CaptureWebPage(GetCleanTestName(TestContext.CurrentContext.Test.FullName) + ".Failed");
                }
                Job.UpDateJob(sessionId,true);

                Driver.Close();
                Driver.Quit();
            }
        }

        private static string GetCleanTestName(string fullName)
        {
            if (fullName.Contains("("))
                fullName = fullName.Substring(0, fullName.LastIndexOf("("));
            var justName = fullName.Split('.').Last();
            return justName;
        }
    }
}
