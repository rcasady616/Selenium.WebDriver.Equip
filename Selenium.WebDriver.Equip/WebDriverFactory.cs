﻿using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using Selenium.WebDriver.Equip.SauceLabs;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Selenium.WebDriver.Equip
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

        public static TBrowser GetBrowser<TBrowser, T>(T options, string url = null) where TBrowser : IWebDriver, new()
        {
            var browser = (TBrowser)Activator.CreateInstance(typeof(TBrowser), options);

            if (url != null)
                browser.Navigate().GoToUrl(url);
            return browser;
        }

        /// <summary>
        /// Gets a <see cref="FirefoxDriver"/> and navigates to a url
        /// </summary>
        /// <param name="url">The url to start the browser at, not required</param>
        /// <returns>A <see cref="FirefoxDriver"/></returns>
        public static IWebDriver FirefoxBrowser(string url = null)
        {
            var service = FirefoxDriverService.CreateDefaultService(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            service.FirefoxBinaryPath = DriversConfiguration.FireFoxBinaryPath;
            var browser = new FirefoxDriver(service);
            if (url != null)
                browser.Navigate().GoToUrl(url);
            return browser;
        }

        public static RemoteWebDriver GetSauceDriver(string testName, string browser = "firefox", string version = "10", string platform = "Windows 10", string url = null)
        {
            var capabillities = new DesiredCapabilities();
            RemoteWebDriver driver = null;
            capabillities.SetCapability(CapabilityType.BrowserName, browser);
            capabillities.SetCapability(CapabilityType.Platform, platform);
            capabillities.SetCapability("version", version);
            var build = Assembly.GetAssembly(typeof(WebDriverFactory)).GetName().Version.ToString();
            string assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            Console.WriteLine($"assemblyVersion: {assemblyVersion}");
            string assemblyVersion2 = Assembly.LoadFile("your assembly file").GetName().Version.ToString();
            Console.WriteLine($"assemblyVersion2: {assemblyVersion2}");
            string fileVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
            Console.WriteLine($"fileVersion: {fileVersion}");
            string productVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
            Console.WriteLine($"productVersion: {productVersion}");
            Console.WriteLine($"build: {build}");
            capabillities.SetCapability("build", build);
            // add these two enviorment variables and there values to use Sauce Labs
            capabillities.SetCapability("username", SauceDriverKeys.SAUCELABS_USERNAME);
            capabillities.SetCapability("accessKey", SauceDriverKeys.SAUCELABS_ACCESSKEY);
            capabillities.SetCapability("name", testName);
            driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), capabillities);
            if (!string.IsNullOrEmpty(url))
                driver.Navigate().GoToUrl(url);
            return driver;
        }

        public static RemoteWebDriver GetRemoteWebDriver(string browserName = "Chrome", string version = "49", string url = null)
        {
            var browser = (BrowserName)Enum.Parse(typeof(BrowserName), browserName);
            throw new Exception("refactor obsolut code, or remove");
            DesiredCapabilities capabillities = new DesiredCapabilities();
            RemoteWebDriver driver = null;
            try
            {
                switch (browser)
                {
                    case BrowserName.IE:
                        capabillities.SetCapability("nativeEvents", false);
                        break;
                    case BrowserName.Firefox:
                        //var profile = new FirefoxProfile { EnableNativeEvents = seleniumSettings.EnableNativeEvents };
                        //capabilities = new DesiredCapabilities(seleniumSettings.BrowserName, seleniumSettings.BrowserVersion, new Platform(PlatformType.Windows));
                        //capabilities = DesiredCapabilities.Firefox();// new DesiredCapabilities(seleniumSettings.BrowserName, seleniumSettings.BrowserVersion, new Platform(PlatformType.Windows));

                        //capabilities.SetCapability(CapabilityType.AcceptSslCertificates, true);
                        //capabilities.SetCapability("firefox_profile", profile.ToBase64String());
                        break;
                    case BrowserName.Chrome:
                        break;
                    default:
                        throw new Exception("Unhandled browser type");
                }
                capabillities.SetCapability(CapabilityType.Version, version);
                capabillities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.XP));
                //capabillities.SetCapability("name", "Testing Selenium 2 with C# on Sauce");
                //capabillities.SetCapability("build", Assembly.GetAssembly(typeof(WebDriverFactory)).GetName().Version.ToString());
                //capabillities.SetCapability("username", SauceDriverKeys.SAUCELABS_USERNAME);
                //capabillities.SetCapability("accessKey", SauceDriverKeys.SAUCELABS_ACCESSKEY);
                driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), capabillities);
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
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 10);
            //driver.Manage().Timeouts().SetPageLoadTimeout(new TimeSpan(0, 0, 30));
            //driver.Manage().Timeouts().SetScriptTimeout(new TimeSpan(0, 0, 15));
            return driver;
        }
    }
}