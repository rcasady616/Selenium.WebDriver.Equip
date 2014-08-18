using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace SeleniumExtension
{
    public class EnvironmentManager
    {
        public static readonly EnvironmentManager instance = new EnvironmentManager();
        private Type driverType;
        private Browser browser;
        private IWebDriver driver;
        //private UrlBuilder urlBuilder;
        private string remoteCapabilities;

        public Browser Browser
        {
            get { return browser; }
        }

        private EnvironmentManager()
        {
            string driverClassName = GetSettingValue("Driver");
            string assemblyName = GetSettingValue("Assembly");
            Assembly assembly = Assembly.Load(assemblyName);
            driverType = assembly.GetType(driverClassName);
            browser = (Browser)Enum.Parse(typeof(Browser), GetSettingValue("DriverName"));
            remoteCapabilities = GetSettingValue("RemoteCapabilities");

            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            string assemblyLocation = executingAssembly.Location;

            string currentDirectory = Path.GetDirectoryName(assemblyLocation);
            //DirectoryInfo info = new DirectoryInfo(currentDirectory);
            //while (info != info.Root && string.Compare(info.Name, "build", StringComparison.OrdinalIgnoreCase) != 0)
            //{
            //    info = info.Parent;
            //}

            //info = info.Parent;
            //bool autoStartRemoteServer = false;
            //if (browser == Browser.Remote)
            //{
            //    autoStartRemoteServer = bool.Parse(GetSettingValue("AutoStartRemoteServer"));
            //}
            

        }

        ~EnvironmentManager()
        {
            if (driver != null)
                driver.Quit();
        }

        public static string GetSettingValue(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings.GetValues(key)[0];
        }

        public IWebDriver GetCurrentDriver()
        {
            if (driver != null)
                return driver;
            else
                return CreateFreshDriver();
        }

        public IWebDriver CreateDriverInstance()
        {
            if (browser == Browser.SauceLabs)
                return WebDriverFactory.GetSauceDriver();
            return (IWebDriver)Activator.CreateInstance(driverType);
        }

        public IWebDriver CreateFreshDriver()
        {
            CloseCurrentDriver();
            driver = CreateDriverInstance();
            return driver;
        }

        public void CloseCurrentDriver()
        {
            if (driver != null)
            {
                if (Browser == Browser.SauceLabs)
                    UpDateJob();
                driver.Quit();
            }
            driver = null;
        }

        public void UpDateJob()
        {
            var sessionId = (string)((RemoteWebDriver)driver).Capabilities.GetCapability("webdriver.remote.sessionid");
            // get the status of the current test
            bool passed = TestContext.CurrentContext.Result.Status == TestStatus.Passed;
            try
            {
                // log the result to sauce labs
                ((IJavaScriptExecutor)driver).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
            }
            finally
            {

            }
        }
    }
}
