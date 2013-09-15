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
        public IWebDriver _driver;

        /// <summary>
        /// Initialize the browser before the test starts
        /// </summary>
        [SetUp]
        public void SetupTest()
        {
            _driver = IWebDriverFactory.GetBrowser();
        }

        /// <summary>
        /// Dereference the instance of the browser
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            if (_driver != null)
            {
                _driver.Close();
                _driver.Quit();
            }
        }
    }
}
