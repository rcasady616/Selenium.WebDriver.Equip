using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.WebDriver.Equip.DriverManager;
using System;

namespace Selenium.WebDriver.Equip.Tests.WebDriver
{
   // [TestFixture(typeof(ChromeDriver))]
    [Category(TestCategories.LocalOnly)]
    public class WebDriverTests<TDriver> : BaseTests<TDriver> where TDriver : IWebDriver, new()
    {
        [SetUp]
        public void SetupTest()
        {
            Manager m = new Manager();
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
          //  m.GetAndUnpack(new ChromeDriverBinary(), "");

        }

        [TearDown]
        public void TearDown() 
        {
        }

        [Test]
        public void GetDriverGetBrowser()
        {
            var url = "http://rickcasady.blogspot.com/";

           // driver = driver.GetDriver<TDriver>();

            Driver.Navigate().GoToUrl(url);
            Assert.AreEqual(typeof(TDriver), Driver.GetType());
            Assert.AreEqual(url, Driver.Url);
        }
    }
}
