using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Selenium.WebDriver.Equip.Tests;
using Selenium.WebDriver.Equip.DriverManager;
using System;
using System.IO;

namespace Selenium.WebDriver.Equip.Manager.Tests
{
    [TestFixture]
    [Category(TestCategories.LocalOnly)]
    public class DriverManagerTests
    {
        public string TestFolder = @$"{AppDomain.CurrentDomain.BaseDirectory}\TestFolder-{DateTime.Now.ToPath()}";
        public DriverManager.Manager driverManager;

        [SetUp]
        public void SetupTest()
        {
            driverManager = new DriverManager.Manager();
            if (!Directory.Exists(TestFolder))
                Directory.CreateDirectory(TestFolder);
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed)
                if (Directory.Exists(TestFolder))
                    Directory.Delete(TestFolder, true);
        }

        [Test]
        public void DownloadChromeDriverBinary()
        {
            driverManager.GetAndUnpack(new ChromeDriverBinary(), TestFolder);
            Assert.AreEqual(true, File.Exists(@$"{TestFolder}\{new ChromeDriverBinary().FileName}"));
        }

        [Test]
        public void DownloadFirefoxDriverBinary()
        {
            driverManager.GetAndUnpack(new FirefoxDriverBinary(), TestFolder);
            Assert.AreEqual(true, File.Exists(@$"{TestFolder}\{new FirefoxDriverBinary().FileName}"));
        }
    }
}