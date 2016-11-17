using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Selenium.WebDriver.Equip.SauceLabs;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Selenium.WebDriver.Equip.Server;
using Selenium.WebDriver.Equip.Settings;

namespace Selenium.WebDriver.Equip
{
    public class EnvironmentManager
    {
        private static EnvironmentManager instance = new EnvironmentManager();
        private Browser browser;
        private IWebDriver driver;
        //private UrlBuilder urlBuilder;
        private string remoteBrowserName;
        private BrowserName browserName;
        private string remoteBrowserVersion;
        private string remoteOsPlatform;
        private SeleniumServerProxy remoteServer;

        public Browser Browser
        {
            get { return browser; }
        }

        public static EnvironmentManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new EnvironmentManager();
                return instance;
            }
        }

        public SeleniumServerProxy RemoteServer
        {
            get { return remoteServer; }
        }

        private EnvironmentManager()
        {
            browser = (Browser)Enum.Parse(typeof(Browser), GetSettingValue("Drivertype"));
            browserName = (BrowserName)Enum.Parse(typeof(BrowserName), GetSettingValue("BrowserName"));
            switch (browser)
            {
                case Browser.Remote:
                    // todo get config
                    // todo validate config
                    ReadRemoteConfiguration();
                    //if(GetSettingValue("AutoStart"))

                    //throw new NotImplementedException();
                    var settings = new SeleniumServerSettings { HostName = "localhost", Port = "4444", StandAlonePath = @"C:\Users\Rick\Documents\GitHub\SeleniumExtensions\selenium-server-standalone-3.0.1.jar" };

                    remoteServer = new SeleniumServerProxy(settings);
                    break;
                case Browser.SauceLabs:
                    // todo get config
                    // todo validate config
                    ReadRemoteConfiguration();
                    Assembly executingAssembly = Assembly.GetExecutingAssembly();
                    string assemblyLocation = executingAssembly.Location;
                    string currentDirectory = Path.GetDirectoryName(assemblyLocation);
                    break;
                case Browser.IPhone:
                case Browser.Android:
                case Browser.WindowsPhone:
                    throw new NotImplementedException("No mobile support at this time");
                default: //all other cases are local drivers
                    break;
            }
        }

        private void ReadRemoteConfiguration()
        {
            remoteBrowserName = GetSettingValue("RemoteBrowserName");
            remoteBrowserVersion = GetSettingValue("RemoteBrowserVersion");
            remoteOsPlatform = GetSettingValue("RemoteOsPlatform");
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
            switch (browser)
            {
                case Browser.SauceLabs:
                    driver = WebDriverFactory.GetSauceDriver(testName, remoteBrowserName, remoteBrowserVersion, remoteOsPlatform);
                    break;
                case Browser.Remote:
                    //Todo start localy if not already running
                    //throw new NotImplementedException();
                    driver = WebDriverFactory.GetRemoteWebDriver();
                    break;
                case Browser.Chrome:
                    if (!driver.GetNuGetChromeDriver()) throw new DriverServiceNotFoundException();
                    driver = WebDriverFactory.GetBrowser<ChromeDriver>();
                    break;
                case Browser.Firefox:
                    if (!driver.DownloadGeckoDriver()) throw new DriverServiceNotFoundException();
                    driver = WebDriverFactory.GetBrowser<FirefoxDriver>();
                    break;
                case Browser.IE:
                    if (!driver.GetNuGetIEDriver()) throw new DriverServiceNotFoundException();
                    driver = WebDriverFactory.GetBrowser<InternetExplorerDriver>();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return driver;
        }

        public void CloseCurrentDriver(bool? outcome = null)
        {
            if (browser == Browser.SauceLabs)
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