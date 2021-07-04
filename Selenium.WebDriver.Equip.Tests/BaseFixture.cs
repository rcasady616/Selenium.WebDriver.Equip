﻿using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using Selenium.WebDriver.Equip.Server;
using Selenium.WebDriver.Equip.Settings;
using Selenium.WebDriver.Equip.WebDriver;
using System;
using System.IO;
using System.Reflection;

namespace Selenium.WebDriver.Equip.Tests
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture(typeof(ChromeDriver), OSType.LINUX)]
    [TestFixture(typeof(ChromeDriver), OSType.MAC)]
    [TestFixture(typeof(ChromeDriver), OSType.WINDOWS)]
    [TestFixture(typeof(FirefoxDriver),OSType.LINUX)]
    [TestFixture(typeof(FirefoxDriver),OSType.MAC)]
    [TestFixture(typeof(FirefoxDriver),OSType.WINDOWS)]
    public class BaseFixture<TDriver> where TDriver : IWebDriver, new()
    {
        public IWebDriver Driver;
        public OSType OS;// = null;
        private DriverType driverType;
        private string remoteBrowserName;
        private BrowserName browserName;
        private string remoteBrowserVersion;
        private string remoteOsPlatform;
        private SeleniumServerProxy remoteServer;
        private SeleniumSettings seleniumSettings;

        public BaseFixture(OSType os)
        {
            OS = os;

        }

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
            Driver = Driver.GetSauceDriver<TDriver>(TestContext.CurrentContext.Test.Name, OS);
        }

        [TearDown]
        public void TearDown()
        {
            var outcome = TestContext.CurrentContext.Result.Outcome == ResultState.Success;

            if (outcome != null)
                UpDateJob(Boolean.Parse(outcome.ToString()));
            if (Driver != null) Driver.Quit();
        }


        public void UpDateJob(bool outcome)
        {
            var sessionId = (string)((RemoteWebDriver)Driver).Capabilities.GetCapability("webdriver.remote.sessionid");
            try
            {
                ((IJavaScriptExecutor)Driver).ExecuteScript("sauce:job-result=" + (outcome ? "passed" : "failed"));
            }
            finally
            {
            }
        }
    }
}
