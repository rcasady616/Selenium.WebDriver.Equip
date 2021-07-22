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
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        public void SetupTest()
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
        {
            DriverManager.DriverManager m = new();
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
          //  m.GetAndUnpack(new ChromeDriverBinary(), "");

        }

        [TearDown]
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        public void TearDown() 
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
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
