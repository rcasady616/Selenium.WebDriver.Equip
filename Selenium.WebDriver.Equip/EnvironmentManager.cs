using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using Selenium.WebDriver.Equip.Server;
using Selenium.WebDriver.Equip.Settings;
using Selenium.WebDriver.Equip.WebDriver;
using System;
using System.IO;
using System.Reflection;

namespace Selenium.WebDriver.Equip
{
    public class EnvironmentManager
    {
        private DriverType driverType;
        //private static ThreadLocal<IWebDriver> threadLoaclDriver = new ThreadLocal<IWebDriver>();
        private IWebDriver driver;
        private string remoteBrowserName;
        private BrowserName browserName;
        private string remoteBrowserVersion;
        private string remoteOsPlatform;
        private SeleniumServerProxy remoteServer;
        private SeleniumSettings seleniumSettings;

        public DriverType DriverType
        {
            get { return driverType; }
        }

        public SeleniumServerProxy RemoteServer
        {
            get { return remoteServer; }
        }

        public EnvironmentManager()
        {
            seleniumSettings = new SeleniumSettings().Deserialize();

            driverType = seleniumSettings.DriverType;
            browserName = seleniumSettings.BrowserName;
            switch (driverType)
            {
                case DriverType.Remote:
                    // todo get config
                    // todo validate config
                    ReadRemoteConfiguration();
                    //if(GetSettingValue("AutoStart"))

                    var settings = new SeleniumServerSettings { HostName = "localhost", Port = "4444", StandAlonePath = @"C:\Users\Rick\Documents\GitHub\SeleniumExtensions\selenium-server-standalone-3.0.1.jar" };
                    remoteServer = new SeleniumServerProxy(settings);
                    break;
                case DriverType.SauceLabs:
                    // todo get config
                    // todo validate config
                    ReadRemoteConfiguration();
                    Assembly executingAssembly = Assembly.GetExecutingAssembly();
                    string assemblyLocation = executingAssembly.Location;
                    string currentDirectory = Path.GetDirectoryName(assemblyLocation);
                    break;
                case DriverType.IPhone:
                case DriverType.Android:
                case DriverType.WindowsPhone:
                    throw new NotImplementedException("No mobile support at this time");
                default: //all other cases are local drivers
                    break;
            }
        }

        private void ReadRemoteConfiguration()
        {
            remoteBrowserName =seleniumSettings.RemoteBrowserName;
            remoteBrowserVersion = seleniumSettings.RemoteBrowserVersion;
            remoteOsPlatform = seleniumSettings.RemoteOsPlatform;
        }

        public string GetSettingValue(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings.GetValues(key)[0];
        }

        public IWebDriver CreateDriverInstance(string testName)
        {
            switch (driverType)
            {
                case DriverType.SauceLabs:
                    driver = WebDriverFactory.GetSauceDriver(testName, remoteBrowserName, remoteBrowserVersion, remoteOsPlatform);
                    break;
                case DriverType.Remote:
                    //Todo start localy if not already running
                    //throw new NotImplementedException();
                   // driver = WebDriverFactory.GetRemoteWebDriver();
                    break;
                case DriverType.Chrome:
                    if (!driver.GetNuGetChromeDriver()) throw new DriverServiceNotFoundException();
                    driver = WebDriverFactory.GetBrowser<ChromeDriver>();
                    break;
                case DriverType.Firefox:
                    if (!driver.DownloadGeckoDriver()) throw new DriverServiceNotFoundException();
                    driver = WebDriverFactory.FirefoxBrowser();
                    break;
                case DriverType.IE:
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
            if (driverType == DriverType.SauceLabs)
                if (outcome != null)
                    UpDateJob(Boolean.Parse(outcome.ToString()));
            if (driver != null) driver.Quit();
            driver = null;
            //threadLoaclDriver.Dispose();
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