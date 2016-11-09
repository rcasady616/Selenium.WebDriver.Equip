using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Selenium.WebDriver.Equip.SauceLabs;
using OpenQA.Selenium.Chrome;

namespace Selenium.WebDriver.Equip
{
    public class EnvironmentManager
    {
        public static readonly EnvironmentManager instance = new EnvironmentManager();
        private Type webDriverType;
        private Browser browser;
        private IWebDriver driver;
        //private UrlBuilder urlBuilder;
        private string remoteBrowserName;
        private string remoteBrowserVersion;
        private string remoteOsPlatform;

        public Browser Browser
        {
            get { return browser; }
        }

        private EnvironmentManager()
        {
            string driverClassName = GetSettingValue("Driver.Class");
            string assemblyName = GetSettingValue("Assembly");
            var assembly = Assembly.Load(assemblyName);
            webDriverType = assembly.GetType(driverClassName);
            browser = (Browser)Enum.Parse(typeof(Browser), GetSettingValue("Drivertype"));
            remoteBrowserName = GetSettingValue("RemoteBrowserName");
            remoteBrowserVersion = GetSettingValue("RemoteBrowserVersion");

            remoteOsPlatform = GetSettingValue("RemoteOsPlatform");
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
            if (browser == Browser.Remote)
            {

            }


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


        public IWebDriver CreateDriverInstance(string testName)
        {
            if (browser == Browser.SauceLabs)
                return WebDriverFactory.GetSauceDriver(testName, remoteBrowserName, remoteBrowserVersion, remoteOsPlatform);
            if (browser == Browser.Remote)
                throw new NotImplementedException();// return WebDriverFactory.GetRemoteWebDriver();
            if (browser == Browser.Chrome)
            {
                driver.NuGetChromeDriver();
                return WebDriverFactory.GetBrowser<ChromeDriver>();
            }
            if (browser == Browser.Firefox)
            {
                driver.NuGetChromeDriver();
                return WebDriverFactory.GetBrowser<ChromeDriver>();
            }
            return (IWebDriver)Activator.CreateInstance(webDriverType);
        }

        public IWebDriver CreateFreshDriver(string testName)
        {
            CloseCurrentDriver();
            driver = CreateDriverInstance(testName);
            return driver;
        }

        public void CloseCurrentDriver(bool? outcome = null)
        {
            if (Browser == Browser.SauceLabs)
                if (outcome != null)
                    UpDateJob(Boolean.Parse(outcome.ToString()));
            if (driver != null) driver.Quit();
            driver = null;
        }

        public void UpDateJob(bool outcome)
        {
            var sessionId = (string)((RemoteWebDriver)driver).Capabilities.GetCapability("webdriver.remote.sessionid");
            try
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("sauce:job-result=" + (outcome ? "passed" : "failed"));
            }
            finally
            {

            }
        }
    }
}
