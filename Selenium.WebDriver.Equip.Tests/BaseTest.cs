using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using NUnit.Framework.Interfaces;
using System.Threading;

namespace Selenium.WebDriver.Equip.Tests
{
    /// <summary>
    /// A base fixture for Selenium testing single browser per test
    /// </summary>
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    public class BaseTest
    {
        /// <summary>
        /// Instance of the browser used for the test
        /// </summary>
        public IWebDriver Driver;
        public EnvironmentManager EnvManager;

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
            EnvManager = new EnvironmentManager();
            Driver = EnvManager.CreateDriverInstance(TestContext.CurrentContext.Test.Name);
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
                    new TestCapture(Driver, TestContext.CurrentContext.Test.GetCleanName() + ".Failed").CaptureWebPage();
                EnvManager.CloseCurrentDriver(outcome);
                Driver = null;
            }
        }
    }
}
