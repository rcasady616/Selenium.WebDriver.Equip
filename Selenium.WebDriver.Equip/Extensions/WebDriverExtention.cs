using NuGet;
using NuGet.Versioning;
using OpenQA.Selenium.Support.UI;
using Selenium.WebDriver.Equip;
using Selenium.WebDriver.Equip.PageObjectGenerator;
using Selenium.WebDriver.Equip.Settings;
using System;
using System.IO;
using System.Reflection;

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
    }
}