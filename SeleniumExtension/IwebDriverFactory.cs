using System;
using System.Configuration;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using SeleniumExtension.Server;

namespace SeleniumExtension
{
    /// <summary>
    /// Defines a factory for creating local and remote <see cref="IWebDriver"/>
    /// </summary>
    public class IWebDriverFactory
    {
        /// <summary>
        /// Gets a local <see cref="IWebDriver"/> and navigates to a url
        /// </summary>
        /// <typeparam name="TBrowser">The <see cref="IWebDriver"/> type to be returned</typeparam>
        /// <param name="url">The url to start the browser at, not required</param>
        /// <returns>A <see cref="IWebDriver"/></returns>
        public static TBrowser GetBrowser<TBrowser>(string url = null) where TBrowser : IWebDriver, new()
        {
            var browser = (TBrowser)Activator.CreateInstance(typeof(TBrowser));

            if (url != null)
                browser.Navigate().GoToUrl(url);
            return browser;
        }

        /// <summary>
        /// Gets a <see cref="FirefoxDriver"/> and navigates to a url
        /// </summary>
        /// <param name="url">The url to start the browser at, not required</param>
        /// <returns>A <see cref="FirefoxDriver"/></returns>
        public static IWebDriver GetBrowser(string url = null)
        {
            return GetBrowser<FirefoxDriver>(url);
        }

        public static RemoteWebDriver GetRemoteWebDriver(SeleniumSettings seleniumSettings = null, string url = null)
        {
            if (seleniumSettings == null)
                seleniumSettings = new SeleniumSettings();
            DesiredCapabilities capabilities = null;
            RemoteWebDriver driver = null;
            try
            {
                switch (seleniumSettings.BrowserName)
                {
                    case "internet explorer":
                        capabilities = DesiredCapabilities.InternetExplorer();
                        capabilities.SetCapability(CapabilityType.AcceptSslCertificates, true);
                        capabilities.SetCapability("nativeEvents", false);
                        break;
                    case "firefox":
                        var profile = new FirefoxProfile { EnableNativeEvents = seleniumSettings.EnableNativeEvents };
                        //capabilities = new DesiredCapabilities(seleniumSettings.BrowserName, seleniumSettings.BrowserVersion, new Platform(PlatformType.Windows));
                        capabilities = DesiredCapabilities.Firefox();// new DesiredCapabilities(seleniumSettings.BrowserName, seleniumSettings.BrowserVersion, new Platform(PlatformType.Windows));

                        //capabilities.SetCapability(CapabilityType.AcceptSslCertificates, true);
                        //capabilities.SetCapability("firefox_profile", profile.ToBase64String());
                        break;
                    default:
                        throw new Exception("Unhandled browser type");
                }
                capabilities.IsJavaScriptEnabled = true;
                driver = new RemoteWebDriver(new Uri(seleniumSettings.SeleniumServerAddress), capabilities);
                driver.Navigate().GoToUrl(string.IsNullOrEmpty(url) ? seleniumSettings.BrowserUrl : url);
            }
            catch (FileLoadException fileLoadEx)
            {
                throw new Exception("Failed to load FireFox profile while trying to Initialize the browser.", fileLoadEx);
            }
            catch (WebDriverException webDriverEx)
            {
                if ((webDriverEx.InnerException == null && webDriverEx.ToString().Contains("WebException")))
                {
                    throw new Exception("Selenium Server is not started or had an issue responding while trying to Initialize the browser.", webDriverEx);
                }
                throw;
            }
            driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 10));
            driver.Manage().Timeouts().SetPageLoadTimeout(new TimeSpan(0, 0, 30));
            driver.Manage().Timeouts().SetScriptTimeout(new TimeSpan(0, 0, 15));
            return driver;
        }
    }
}
