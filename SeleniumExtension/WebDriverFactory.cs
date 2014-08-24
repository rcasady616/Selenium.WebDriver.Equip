using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace SeleniumExtension
{
    /// <summary>
    /// Defines a factory for creating local and remote <see cref="IWebDriver"/>
    /// </summary>
    public class WebDriverFactory
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

        public static RemoteWebDriver GetSauceDriver(string browser = "firefox",string version = "10", Platform platform = null, string url = null)
        {
            if(platform == null)
                platform = new Platform(PlatformType.Windows);
            var capabillities = new DesiredCapabilities(browser, version, Platform.CurrentPlatform);
            RemoteWebDriver driver = null;
            capabillities.SetCapability(CapabilityType.Version, version);
            capabillities.SetCapability(CapabilityType.Platform, platform);
            capabillities.SetCapability("build", Assembly.GetAssembly(typeof(WebDriverFactory)).GetName().Version.ToString());
            capabillities.SetCapability("username", Environment.GetEnvironmentVariable("SAUCELABS_USERNAME")); // supply sauce labs username 
            capabillities.SetCapability("accessKey", Environment.GetEnvironmentVariable("SAUCELABS_ACCESSKEY"));  // supply sauce labs account key
            capabillities.SetCapability("name", TestContext.CurrentContext.Test.Name); 

            capabillities.IsJavaScriptEnabled = true;
            driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), capabillities);
            driver.Navigate().GoToUrl(string.IsNullOrEmpty(url) ? "Rickcasady.com" : url);
            return driver;
        }

        public static RemoteWebDriver GetRemoteWebDriver(string browserName = "firefox", string version = "10", string url = null)
        {
           
            DesiredCapabilities capabillities = null;
            RemoteWebDriver driver = null;
            try
            {
                switch (browserName)
                {
                    case "internet explorer":
                        capabillities = DesiredCapabilities.InternetExplorer();
                        capabillities.SetCapability("nativeEvents", false);
                        break;
                    case "firefox":
                        capabillities = DesiredCapabilities.Firefox();


                        //var profile = new FirefoxProfile { EnableNativeEvents = seleniumSettings.EnableNativeEvents };
                        //capabilities = new DesiredCapabilities(seleniumSettings.BrowserName, seleniumSettings.BrowserVersion, new Platform(PlatformType.Windows));
                        //capabilities = DesiredCapabilities.Firefox();// new DesiredCapabilities(seleniumSettings.BrowserName, seleniumSettings.BrowserVersion, new Platform(PlatformType.Windows));

                        //capabilities.SetCapability(CapabilityType.AcceptSslCertificates, true);
                        //capabilities.SetCapability("firefox_profile", profile.ToBase64String());
                        break;
                    default:
                        throw new Exception("Unhandled browser type");
                }
                capabillities.SetCapability(CapabilityType.Version, "10");
                capabillities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.XP));
                capabillities.SetCapability("name", "Testing Selenium 2 with C# on Sauce");
                capabillities.SetCapability("username", "richardcasady");
                capabillities.SetCapability("accessKey", "a6618920-425d-4f34-b9c2-1576bef9686e");
                capabillities.SetCapability("build", "Local-1234");

                capabillities.IsJavaScriptEnabled = true;
                driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), capabillities);
                driver.Navigate().GoToUrl(string.IsNullOrEmpty(url) ? "http://rickcasady.blogspot.com/" : url);
                

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
            //driver.Manage().Timeouts().SetPageLoadTimeout(new TimeSpan(0, 0, 30));
            //driver.Manage().Timeouts().SetScriptTimeout(new TimeSpan(0, 0, 15));
            return driver;
        }
    }
}
