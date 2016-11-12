using NuGet;
using OpenQA.Selenium.Support.UI;
using Selenium.WebDriver.Equip;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace OpenQA.Selenium
{
    public static class WebDriverExtention
    {
        public static bool GetNuGetChromeDriver(this IWebDriver iWebDriver)
        {
            string fileName = "chromedriver.exe";
            string packageID = "Selenium.WebDriver.ChromeDriver";
            var version = "2.25.0.0";
            if (!File.Exists(fileName))
            {
                var path = Directory.GetCurrentDirectory();
                GetNuGetPackage(packageID, path, SemanticVersion.Parse(version));
                File.Copy(string.Format(@"{0}\{1}.{2}\driver\{3}", path,packageID, version, fileName), string.Format("{0}\\{1}", path, fileName));
            }
            return File.Exists(fileName);
        }

        private static void GetNuGetPackage(string packageID, string path, SemanticVersion version)
        {
            var repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
            var packageManager = new PackageManager(repo, path);
            packageManager.InstallPackage(packageID, version);

        }

        public static bool GetNuGetIEDriver(this IWebDriver iWebDriver)
        {
            string fileName = "IEDriverServer.exe";
            string packageID = "Selenium.WebDriver.IEDriver";
            var version = "3.0.0.0";
            if (!File.Exists(fileName))
            {
                var path = Directory.GetCurrentDirectory();
                GetNuGetPackage(packageID, path, SemanticVersion.Parse(version));
                File.Copy(string.Format(@"{0}\{1}.{2}\driver\{3}", path,packageID, version, fileName), string.Format("{0}\\{1}", path, fileName));
            }
            return File.Exists(fileName);
        }

        public static bool DownloadUrlGeckoDriver(this IWebDriver iwebDriver)
        {
            string fileName = "geckodriver.exe";
            string zipFileName = "geckodriver.zip";
            if (!File.Exists(fileName))
            {
                //new WebClient().DownloadFile("https://github.com/mozilla/geckodriver/releases/download/v0.11.1/geckodriver-v0.11.1-win32.zip", zipFileName);
                new WebClient().DownloadFile("https://github.com/mozilla/geckodriver/releases/download/v0.11.1/geckodriver-v0.11.1-win64.zip", zipFileName);
                new FileInfo(zipFileName).UnZip(Directory.GetCurrentDirectory());
            }
            return File.Exists(fileName);
        }

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

        /// <summary>
        /// Waits for a <see cref="IWebElement"/> to meet specific conditions
        /// </summary>
        /// <param name="condition">The <see cref="ExpectedConditions"/> criteria to <see cref="WebDriverWait"/> for</param>
        /// <param name="maxWaitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the condition</param>
        /// <returns><see langword="true"/> if the condition is meet; otherwise, <see langword="false"/>.</returns>
        public static bool DriverWaitUntil<T>(this IWebDriver iWebDriver, Func<IWebDriver, T> condition, int maxWaitTimeInSeconds = 10)
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
        public static bool WaitUntilTitleIs(this IWebDriver iWebDriver, string title, int maxWaitTimeInSeconds = 10)
        {
            return iWebDriver.DriverWaitUntil(ExpectedConditions.TitleIs(title), maxWaitTimeInSeconds);
        }

        /// <summary>
        /// Waits for an IAlert
        /// </summary>
        /// <param name="waitTimeInSeconds">Maximum amount of seconds as <see cref="int"/> to wait for the <see cref="IWebElement"/> to become visible</param>
        /// <returns><see langword="true"/> if the alert exists; otherwise, <see langword="false"/></returns>
        public static bool WaitUntilAlertExists(this IWebDriver iWebDriver, int waitTimeInSeconds = 10)
        {
            return DriverWaitUntil(iWebDriver, ExpectedConditions.AlertIsPresent(), waitTimeInSeconds);
        }

        public static bool WaitUntilAlertTextEquals(this IWebDriver iWebDriver, string text, int waitTimeInSeconds = 10)
        {
            return DriverWaitUntil(iWebDriver, ExpectedCondition.AlertTextEquals(text), waitTimeInSeconds);
        }

        public static bool WaitUntilAlertTextContains(this IWebDriver iWebDriver, string text, int waitTimeInSeconds = 10)
        {
            return DriverWaitUntil(iWebDriver, ExpectedCondition.AlertTextContains(text), waitTimeInSeconds);
        }

        public static void TakeScreenShot(this IWebDriver iWebDriver, string fileName, ImageFormat imageFormat)
        {
            var tempDriver = (ITakesScreenshot)iWebDriver;
            var screenShot = tempDriver.GetScreenshot();
            screenShot.SaveAsFile(fileName, imageFormat);
        }
    }
}
