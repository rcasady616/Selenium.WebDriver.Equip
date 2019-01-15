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
        public EnvironmentManager EnvManager;

        [SetUp]
        public void SetupTest()
        {
            EnvManager = new EnvironmentManager();
            Driver = EnvManager.CreateDriverInstance(TestContext.CurrentContext.Test.Name);
        }
    }
}
