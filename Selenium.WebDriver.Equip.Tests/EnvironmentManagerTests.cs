using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Selenium.WebDriver.Equip.Tests
{
    [TestFixture]
    public class EnvironmentManagerTests
    {
        public IWebDriver Driver;

        [SetUp]
        public void SetupTest()
        {
            Driver = EnvironmentManager.instance.CreateDriverInstance(TestContext.CurrentContext.Test.Name);
        }
    }
}
