using System;
using System.IO;
using SeleniumExtension.SauceLabs;
using Gallio.Framework;
using Gallio.Model;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using TestWebPages.UIFramework.Pages;

namespace SeleniumExtentions.MbUnit.Tests
{
    [TestFixture]
    //[Parallelizable]
    [Header("browser", "version", "platform")]
    [Row("firefox", "25", "Windows 7")]
    [Row("chrome", "31", "Windows 7")]
    //[Row("ie", "11", "Windows 7")]
    //[Row("firefox", "25", "Windows 8")]
    //[Row("ie", "11", "Windows 8")]
    public class WaitUntilTests
    {
        private IWebDriver _Driver;
        private AjaxyControlPage ajaxyControlPage;
        public ISearchContext item;

        #region Setup and Teardown

        /// <summary>starts a sauce labs sessions</summary>
        /// <param name="browser">name of the browser to request</param>
        /// <param name="version">version of the browser to request</param>
        /// <param name="platform">operating system to request</param>
        private void _Setup(string browser, string version, string platform)
        {
            // construct the url to sauce labs

            Uri commandExecutorUri = new Uri("http://ondemand.saucelabs.com:80/wd/hub");

            // set up the desired capabilities
            DesiredCapabilities desiredCapabilites = new DesiredCapabilities(browser, version, Platform.CurrentPlatform); // set the desired browser
            desiredCapabilites.SetCapability("platform", platform); // operating system to use
            desiredCapabilites.SetCapability("username", Constants.SAUCE_LABS_ACCOUNT_NAME); // supply sauce labs username 
            desiredCapabilites.SetCapability("accessKey", Constants.SAUCE_LABS_ACCOUNT_KEY);  // supply sauce labs account key
            desiredCapabilites.SetCapability("name", TestContext.CurrentContext.Test.Name); // give the test a name

            // start a new remote web driver session on sauce labs
            _Driver = new RemoteWebDriver(commandExecutorUri, desiredCapabilites);
            _Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            _Driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(30));

            // navigate to the page under test
            string url = string.Format(@"{0}{1}", "http://rickcasady.com/SeleniumExtentions/v1.0", AjaxyControlPage.Url);
            _Driver.Navigate().GoToUrl(url);
         
        }

        /// <summary>called at the end of each test to tear it down</summary>
        [TearDown] // denotes that this will be called at the end of each test method
        public void CleanUp()
        {
            // get the status of the current test
            bool passed = TestContext.CurrentContext.Outcome.Status == TestStatus.Passed;
            try
            {
                // log the result to sauce labs
                ((IJavaScriptExecutor)_Driver).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
            }
            finally
            {
                // terminate the remote webdriver session
                if (_Driver != null)
                    _Driver.Quit();
            }
        }

        #endregion

        /// <summary>tests the title of the page</summary>
        //[Parallelizable, Test]
        //[Row(true, "red")]//, Row(false, "NeverGonnaGetItNeverGonnaGetIt")]
        //[Row(false, "NeverGonnaGetItNeverGonnaGetIt")]
        ////[TestCase(true, "red")]
        ////[TestCase(false, "NeverGonnaGetItNeverGonnaGetIt")]
        //public void TestWaitUntilExists(bool expected, string id)
        //{
        //    Assert.AreEqual(expected, item.WaitUntilExists(By.Id(id)));
        //}

        /// <summary>tests that the difference between the server and client render times are within an interval</summary>
        [Parallelizable, Test] // denotes that this method is a test and can be run in parallel
        public void DeliveryTime(string browser, string version, string platform)
        {


            // start the remote webdriver session with sauce labs
            _Setup(browser, version, platform);

            // navigate to the page under test
            _Driver.Navigate().GoToUrl("https://saucelabs.com/test/guinea-pig");

            // read two timestamps off the bottom of the page
            var serverLoadTime = int.Parse(_Driver.FindElement(By.Id("servertime")).Text);
            var clientLoadTime = int.Parse(_Driver.FindElement(By.Id("clienttime")).Text);

            // check that they are within 5 seconds of one another
            Assert.AreApproximatelyEqual(serverLoadTime, clientLoadTime, 5);
        }

        [Parallelizable, Test]
        public void TestWaitUntilExistsElementLoadedByJavaScriptAfterPageLoad(string browser, string version, string platform)
        {
            _Setup(browser, version, platform);
            ajaxyControlPage = new AjaxyControlPage(_Driver);
             Assert.AreEqual(true, ajaxyControlPage.IsPageLoaded());
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            ajaxyControlPage.SubmitButton.Click();
            Assert.AreEqual(true, item.WaitUntilVisible(AjaxyControlPage.ByLabelsDiv));
        }
    }
}
