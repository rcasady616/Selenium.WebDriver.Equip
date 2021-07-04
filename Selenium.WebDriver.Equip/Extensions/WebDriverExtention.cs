using NuGet;
using NuGet.Versioning;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using Selenium.WebDriver.Equip;
using Selenium.WebDriver.Equip.DriverManager;
using Selenium.WebDriver.Equip.PageObjectGenerator;
using Selenium.WebDriver.Equip.SauceLabs;
using Selenium.WebDriver.Equip.Settings;
using Selenium.WebDriver.Equip.WebDriver;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace OpenQA.Selenium
{
    public static class WebDriverExtention
    {

        /// <summary>
        /// Casts the driver into a IJavaScriptExecutor
        /// </summary>
        /// <returns>IJavaScriptExecutor</returns>
        public static IJavaScriptExecutor Scripts(this IWebDriver driver)
        {
            return (IJavaScriptExecutor)driver;
        }

        /// <summary>
        /// Using nuget to retrieve the Selenium.WebDriver.ChromeDriver specified in the app.config
        /// https://www.nuget.org/packages/Selenium.WebDriver.ChromeDriver/
        /// </summary>
        /// <returns>Weather the file path exists</returns>
        public static bool GetNuGetChromeDriver(this IWebDriver iWebDriver)
        {
            var config = new SeleniumDriverExeSettings().Deserialize();
            string fileName = "chromedriver.exe";
            string packageID = "Selenium.WebDriver.ChromeDriver";
            var version = config.NugetChromeDriverVersion;
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var fileNamePath = Path.Combine(path, fileName);
            if (!File.Exists(fileNamePath))
            {
                GetNuGetPackage(packageID, path, SemanticVersion.Parse(version));
                File.Copy(string.Format(@"{0}\{1}.{2}\driver\win32\{3}", path, packageID, version, fileName), fileNamePath);
            }
            return File.Exists(fileNamePath);
        }

        private static void GetNuGetPackage(string packageID, string path, SemanticVersion version)
        {
            //var repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
            //var packageManager = new PackageManager(repo, path);
            //packageManager.InstallPackage(packageID, version);

        }

        public static bool GetNuGetIEDriver(this IWebDriver iWebDriver)
        {
            var config = new SeleniumDriverExeSettings().Deserialize();
            string fileName = "IEDriverServer.exe";
            string packageID = "Selenium.WebDriver.IEDriver";
            var version = config.NugetIEDriverVersion;
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var fileNamePath = Path.Combine(path, fileName);
            if (!File.Exists(fileNamePath))
            {
                GetNuGetPackage(packageID, path, SemanticVersion.Parse(version));
                File.Copy(string.Format(@"{0}\{1}.{2}\driver\{3}", path, packageID, version, fileName), fileNamePath);
            }
            return File.Exists(fileNamePath);
        }

        public static bool GetNuGetGeckoDriver(this IWebDriver iWebDriver)
        {
            var config = new SeleniumDriverExeSettings().Deserialize();
            string fileName = "geckodriver.exe";
            string packageID = "Selenium.WebDriver.GeckoDriver";
            var version = config.NugetGeckoDriverVersion;
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var fileNamePath = Path.Combine(path, fileName);
            if (!File.Exists(fileNamePath))
            {
                GetNuGetPackage(packageID, path, SemanticVersion.Parse(version));
                if (IntPtr.Size == 4)
                    File.Copy(string.Format(@"{0}\{1}.{2}\driver\win32\{3}", path, packageID, version, fileName), fileNamePath);
                else if (IntPtr.Size == 8)
                    File.Copy(string.Format(@"{0}\{1}.{2}\driver\win64\{3}", path, packageID, version, fileName), fileNamePath);

            }
            return File.Exists(fileNamePath);
        }

        /// <summary>
        /// Download the Gecko Driver from Mozilla Github https://github.com/mozilla/geckodriver
        /// Uses the 64 bit driver unless otherwise configured 
        /// </summary>
        /// <returns><see langword="true"/> if the driver is present in the working directory; otherwise, <see langword="false"/>.</returns>
        public static bool DownloadGeckoDriver(this IWebDriver iwebDriver)
        {
            if (IntPtr.Size == 4)
                throw new NotImplementedException("Unknow processor mode");
            //return iwebDriver.DownloadGeckoDriver(DriversConfiguration.GeockoDriverURL32);
            else if (IntPtr.Size == 8)
                throw new NotImplementedException("Unknow processor mode");
            //return iwebDriver.DownloadGeckoDriver(DriversConfiguration.GeockoDriverURL64);
            else
                throw new NotImplementedException("Unknow processor mode");
        }

        //public static bool DownloadGeckoDriver(this IWebDriver iwebDriver, string geockoDriverURL)
        //{
        //    string fileName = DriversConfiguration.GeockoDriverFileName;
        //    string zipFileName = "geckodriver.zip";
        //    var file = new FileInfo(zipFileName);
        //    if (!File.Exists(fileName))
        //        file.DownloadUrl(geockoDriverURL).UnZip(Directory.GetCurrentDirectory());
        //    return File.Exists(fileName);
        //}

        //public static bool DownloadChromeDriver(this IWebDriver iwebDriver, string chromeDriverURL)
        //{
        //    string fileName = DriversConfiguration.GeockoDriverFileName;
        //    string zipFileName = "chromedriver.zip";
        //    var file = new FileInfo(zipFileName);
        //    if (!File.Exists(fileName))
        //        file.DownloadUrl(chromeDriverURL).UnZip(Directory.GetCurrentDirectory());
        //    return File.Exists(fileName);
        //}



        public static IWebDriver SwitchBrowserWindowByTitle(this IWebDriver iWebDriver, string title)
        {
            return iWebDriver.SwitchBrowserWindow(driver => driver.WaitUntilTitleIs(title));
        }

        /// <summary>
        /// Switches to the first browser that meets the <see cref="ExpectedConditions"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition">The <see cref="ExpectedConditions"/> criteria to <see cref="WebDriverWait"/> for</param>
        /// <returns>An instance of the <see cref="IWebDriver"/></returns>
        public static IWebDriver SwitchBrowserWindow<T>(this IWebDriver iWebDriver, Func<IWebDriver, T> condition)
        {
            var CurrentWindowHandle = iWebDriver.CurrentWindowHandle;
            IWebDriver newWindowDriver = null;
            var windowIterator = iWebDriver.WindowHandles;
            foreach (var window in windowIterator)
            {
                var handel = window;
                newWindowDriver = iWebDriver.SwitchTo().Window(window);
                if (newWindowDriver.DriverWaitUntil(condition, 2))
                    return newWindowDriver;
            }
            return null;
        }

        public static IWebDriver PopBrowser(this IWebDriver iWebDriver)
        {
            var CurrentWindowHandle = iWebDriver.CurrentWindowHandle;
            var windowIterator = iWebDriver.WindowHandles;
            if (windowIterator.Count > 1)
                foreach (var window in windowIterator)
                    if (window != CurrentWindowHandle)
                        return iWebDriver.SwitchTo().Window(window);
            return null;
        }


        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to meet specific conditions
        /// </summary>
        /// <param name="condition">The <see cref="ExpectedConditions"/> criteria to <see cref="WebDriverWait"/> for</param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the condition</param>
        /// <returns><see langword="true"/> if the condition is meet; otherwise, <see langword="false"/>.</returns>
        public static bool DriverWaitUntil<T>(this IWebDriver iWebDriver, Func<IWebDriver, T> condition, int maxWaitTimeInSeconds = GlobalConstants.MaxWaitTimeInSeconds)
        {
            var driver = (IWebDriver)iWebDriver;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(maxWaitTimeInSeconds));
            try
            {
                wait.Until(condition);
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Waits for a page title to be a specific text
        /// </summary>
        /// <param name="title">The title of the page</param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to become visible</param>
        /// <returns><see langword="true"/> if the title is a match; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilTitleIs(this IWebDriver iWebDriver, string title, int maxWaitTimeInSeconds = GlobalConstants.MaxWaitTimeInSeconds)
        {
            return iWebDriver.DriverWaitUntil(ExpectedCondition.TitleIs(title), maxWaitTimeInSeconds);
        }

        /// <summary>
        /// Waits for an IAlert
        /// </summary>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to become visible</param>
        /// <returns><see langword="true"/> if the alert exists; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilAlertExists(this IWebDriver iWebDriver, int maxWaitTimeInSeconds = GlobalConstants.MaxWaitTimeInSeconds)
        {
            return DriverWaitUntil(iWebDriver, ExpectedCondition.AlertIsPresent(), maxWaitTimeInSeconds);
        }

        public static bool WaitUntilAlertTextEquals(this IWebDriver iWebDriver, string text, int maxWaitTimeInSeconds = GlobalConstants.MaxWaitTimeInSeconds)
        {
            return DriverWaitUntil(iWebDriver, ExpectedCondition.AlertTextEquals(text), maxWaitTimeInSeconds);
        }

        public static bool WaitUntilAlertTextContains(this IWebDriver iWebDriver, string text, int maxWaitTimeInSeconds = GlobalConstants.MaxWaitTimeInSeconds)
        {
            return DriverWaitUntil(iWebDriver, ExpectedCondition.AlertTextContains(text), maxWaitTimeInSeconds);
        }

        public static void TakeScreenShot(this IWebDriver iWebDriver, string fileName, ScreenshotImageFormat imageFormat)
        {
            var tempDriver = (ITakesScreenshot)iWebDriver;
            var screenShot = tempDriver.GetScreenshot();
            screenShot.SaveAsFile(fileName, imageFormat);
        }

        public static IAlert Alert(this IWebDriver iWebDriver)
        {
            return iWebDriver.SwitchTo().Alert();
        }

        public static POG PageObjectGenerator(this IWebDriver iWebDriver)
        {
            return new POG(iWebDriver);
        }

        #region Driver Manager
        //private void ReadRemoteConfiguration()
        //{
        //    remoteBrowserName = seleniumSettings.RemoteBrowserName;
        //    remoteBrowserVersion = seleniumSettings.RemoteBrowserVersion;
        //    remoteOsPlatform = seleniumSettings.RemoteOsPlatform;
        //}
        //public static IWebDriver GetADriver(this IWebDriver iWebDriver, string testName)
        //{
        //    var seleniumSettings = new SeleniumSettings().Deserialize();

        //   var driverType = seleniumSettings.DriverType;
        //   var browserName = seleniumSettings.BrowserName;
        //    switch (driverType)
        //    {
        //        case DriverType.Remote:
        //            //ReadRemoteConfiguration();
        //            //var settings = new SeleniumServerSettings { HostName = "localhost", Port = "4444", StandAlonePath = @"C:\Users\Rick\Documents\GitHub\SeleniumExtensions\selenium-server-standalone-3.0.1.jar" };
        //            //remoteServer = new SeleniumServerProxy(settings);
        //            break;
        //        case DriverType.SauceLabs:
        //            // todo get config
        //            // todo validate config
        //           // ReadRemoteConfiguration();
        //            //Assembly executingAssembly = Assembly.GetExecutingAssembly();
        //            //string assemblyLocation = executingAssembly.Location;
        //            //string currentDirectory = Path.GetDirectoryName(assemblyLocation);
        //            break;
        //        case DriverType.IPhone:
        //        case DriverType.Android:
        //        case DriverType.WindowsPhone:
        //            throw new NotImplementedException("No mobile support at this time");
        //        default: //all other cases are local drivers
        //            break;
        //    }


        //    dynamic options = null;
        //    string version = "10";
        //    string platform = "Windows 10";
        //    string url = null;

        //    switch (driverType)
        //    {
        //        case DriverType.Chrome:
        //            if (options == null) options = new ChromeOptions();
        //            break;
        //        case DriverType.Firefox:
        //            if (options == null) options = new FirefoxOptions();
        //            //options.AddAdditionalCapability(CapabilityType.Platform, platform);
        //            break;
        //        default:
        //            throw new NotImplementedException("unknown Driver");
        //    }
        //    // //options.AcceptInsecureCertificates = true;
        //    // sauce
        //    string assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        //    options.AddAdditionalCapability("build", assemblyVersion, true);
        //    options.AddAdditionalCapability("username", SauceDriverKeys.SAUCELABS_USERNAME, true);
        //    options.AddAdditionalCapability("accessKey", SauceDriverKeys.SAUCELABS_ACCESSKEY, true);
        //    options.AddAdditionalCapability("name", testName, true);

        //    iWebDriver = (TDriver)Activator.CreateInstance(typeof(TDriver),
        //        args: new object[] { new Uri("http://ondemand.saucelabs.com:80/wd/hub"), options.ToCapabilities() });
        //    //iWebDriver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), options.ToCapabilities());

        //    if (!string.IsNullOrEmpty(url))
        //        iWebDriver.Navigate().GoToUrl(url);
        //    return (TDriver)iWebDriver;
        //}

        public static TDriver GetDriver<TDriver, TOptions>(this IWebDriver iWebDriver, TOptions options, string url = null) where TDriver : IWebDriver, new()
        {
            iWebDriver = (TDriver)Activator.CreateInstance(typeof(TDriver), options);
            if (url != null)
                iWebDriver.Navigate().GoToUrl(url);
            return (TDriver)iWebDriver;
        }


        public static TDriver GetDriver<TDriver>(this IWebDriver iWebDriver, string url = null) where TDriver : IWebDriver, new()
        {
            dynamic options = null;
            switch (typeof(TDriver).Name)
            {
                case "ChromeDriver":
                    if (File.Exists(new ChromeDriverBinary().BrowserExePath))
                        if (!File.Exists(new ChromeDriverBinary().FileName))
                            new Manager().GetAndUnpack(new ChromeDriverBinary());
                    break;
                case "FirefoxDriver":
                    if (File.Exists(new FirefoxDriverBinary().BrowserExePath))
                        if (!File.Exists(new FirefoxDriverBinary().FileName))
                            new Manager().GetAndUnpack(new FirefoxDriverBinary());
                    options = new FirefoxOptions();
                    options.BrowserExecutableLocation = new FirefoxDriverBinary().BrowserExePath;
                    break;
                default:
                    throw new NotImplementedException("unknown Driver");
            }

            iWebDriver = (TDriver)Activator.CreateInstance(typeof(TDriver), options);
            if (url != null)
                iWebDriver.Navigate().GoToUrl(url);
            return (TDriver)iWebDriver;
        }

        //public static RemoteWebDriver GetSauceDriver<TDriver>(this IWebDriver iWebDriver, string testName, string url = null) where TDriver : IWebDriver, new()
        //{
        //    dynamic options = null;
        //    string version = "10";
        //    string platform = "Windows 10";

        //    switch (typeof(TDriver).Name)
        //    {
        //        case "ChromeDriver":
        //            if (options == null) options = new ChromeOptions();
        //            break;
        //        case "FirefoxDriver":
        //            if (options == null) options = new FirefoxOptions();
        //            //options.AddAdditionalCapability(CapabilityType.Platform, platform);
        //            break;
        //        default:
        //            throw new NotImplementedException("unknown Driver");
        //    }
        //    // //options.AcceptInsecureCertificates = true;
        //    // sauce
        //    string assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        //    options.AddAdditionalCapability("build", assemblyVersion, true);
        //    options.AddAdditionalCapability("username", SauceDriverKeys.SAUCELABS_USERNAME, true);
        //    options.AddAdditionalCapability("accessKey", SauceDriverKeys.SAUCELABS_ACCESSKEY, true);
        //    options.AddAdditionalCapability("name", testName, true);

        //    //iWebDriver = (RemoteWebDriver)Activator.CreateInstance(typeof(RemoteWebDriver),
        //    //    args: new object[] { new Uri("http://ondemand.saucelabs.com:80/wd/hub"), options.ToCapabilities() });
        //    iWebDriver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), options.ToCapabilities());

        //    if (!string.IsNullOrEmpty(url))
        //        iWebDriver.Navigate().GoToUrl(url);
        //    return (RemoteWebDriver)iWebDriver;
        //}

        public static RemoteWebDriver GetSauceDriver<TDriver>(this IWebDriver iWebDriver, string testName, OSType os = OSType.LINUX, string url = null) where TDriver : IWebDriver, new()
        {
            dynamic options = null;
            string version = "10";
            string platform = "Windows 10";

            switch (typeof(TDriver).Name)
            {
                case "ChromeDriver":
                    if (options == null) options = new ChromeOptions();
                    break;
                case "FirefoxDriver":
                    if (options == null) options = new FirefoxOptions();
                    break;
                default:
                    throw new NotImplementedException("unknown Driver");
            }
            options.AddAdditionalCapability(CapabilityType.Platform, os.GetDescription(), true);
            // //options.AcceptInsecureCertificates = true;
            // sauce
            string assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            options.AddAdditionalCapability("build", assemblyVersion, true);
            options.AddAdditionalCapability("username", SauceDriverKeys.SAUCELABS_USERNAME, true);
            options.AddAdditionalCapability("accessKey", SauceDriverKeys.SAUCELABS_ACCESSKEY, true);
            options.AddAdditionalCapability("name", testName, true);

            //iWebDriver = (RemoteWebDriver)Activator.CreateInstance(typeof(RemoteWebDriver),
            //    args: new object[] { new Uri("http://ondemand.saucelabs.com:80/wd/hub"), options.ToCapabilities() });
            iWebDriver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), options.ToCapabilities());

            if (!string.IsNullOrEmpty(url))
                iWebDriver.Navigate().GoToUrl(url);
            return (RemoteWebDriver)iWebDriver;
        }


        #endregion
    }
}