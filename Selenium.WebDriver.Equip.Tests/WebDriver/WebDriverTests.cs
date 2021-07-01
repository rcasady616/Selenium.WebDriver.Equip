using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.WebDriver.Equip.WebDriver;
using System;

namespace Selenium.WebDriver.Equip.Tests.WebDriver
{
    [TestFixture(typeof(ChromeDriver))]
    [Category(TestCategories.LocalOnly)]
    public class WebDriverTests<TDriver> : BaseTests<TDriver> where TDriver : IWebDriver, new()
    {
        IWebDriver driver;
        [SetUp]
        public void SetupTest()
        {
            DriverManager m = new DriverManager();
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            m.GetAndUnpack(new ChromeDriverBinary(), "");

            driver = driver.GetDriver<ChromeDriver>();
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Close();
                driver.Quit();
            }
        }


        [Test]
        public void GetChromeBrowser()
        {
            var url = "http://rickcasady.blogspot.com/";
            //var _driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);
            Assert.AreEqual(typeof(ChromeDriver), driver.GetType());
            Assert.AreEqual(url, driver.Url);
        }
    }
}
