using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Selenium.WebDriver.Equip.Tests
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(FirefoxDriver))]
    public class BaseTests<TDriver> where TDriver : IWebDriver, new()
    {
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

        [SetUp]
        public void SetupTest()
        {
            Driver = Driver.GetDriver<TDriver>();
            //EnvManager = new EnvironmentManager();
            //Driver = EnvManager.CreateDriverInstance(TestContext.CurrentContext.Test.Name);
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
                if (!outcome) new TestCapture(Driver, TestContext.CurrentContext.Test.GetCleanName() + ".Failed").CaptureWebPage();
                //EnvManager.CloseCurrentDriver(outcome);
                if (Driver != null) Driver.Quit();
                Driver = null;
            }
        }
    }
}
