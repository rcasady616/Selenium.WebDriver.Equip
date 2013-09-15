using NUnit.Framework;
using OpenQA.Selenium;

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
            Driver = IWebDriverFactory.GetBrowser();
        }

        /// <summary>
        /// Dereference the instance of the browser
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Status == TestStatus.Failed)
            {
                new TestCapture(Driver).CaptureWebPage();
            }
            if (Driver != null)
            {
                Driver.Close();
                Driver.Quit();
            }
        }
    }
}
