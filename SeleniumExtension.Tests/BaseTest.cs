using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumExtension.Tests
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
            Driver = EnvironmentManager.instance.GetCurrentDriver();
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
                if (TestContext.CurrentContext.Result.Status != TestStatus.Passed)
                    new TestCapture(Driver).CaptureWebPage(GetCleanTestName(TestContext.CurrentContext.Test.FullName) + ".Failed");

                EnvironmentManager.instance.CloseCurrentDriver();
                Driver = null;
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
