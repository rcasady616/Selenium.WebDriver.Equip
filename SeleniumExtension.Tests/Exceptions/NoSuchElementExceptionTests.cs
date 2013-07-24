using System;
using System.IO;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestWebPages.UIFramework.Pages;

namespace SeleniumExtention.Tests.Exceptions
{
    [TestFixture]
    public class NoSuchElementExceptionTests
    {
        private IWebDriver _driver;
        [SetUp]
        public void SetupTest()
        {
            _driver = IWebDriverFactory.GetBrowser();
        }

        [TearDown]
        public void TearDown()
        {
            if (_driver != null)
            {
                _driver.Close();
                _driver.Quit();
            }
        }

        [Test]
        public void TestNoSuchElementException()
        {
            string url = string.Format(@"file:///{0}../../../..{1}", Directory.GetCurrentDirectory(), AjaxyControlPage.Url);
            _driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 10)); //10 seconds
            _driver.Navigate().GoToUrl(url);
            var ex = Assert.Throws<NoSuchElementException>(() => _driver.FindElement(By.Id("text1")));
        }

        [Test]
        public void TestWebDriverTimeoutException()
        {
            string url = string.Format(@"file:///{0}../../../..{1}", Directory.GetCurrentDirectory(), AjaxyControlPage.Url);
            _driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 10)); //10 seconds
            _driver.Navigate().GoToUrl(url);
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var ex = Assert.Throws<WebDriverTimeoutException>(() => wait.Until(ExpectedConditions.ElementExists(By.Id("text1"))));
        }

        [Test]
        public void TestSelectByText()
        {
            string url = string.Format(string.Format(@"file:///{0}../../../../TestWebPages/PageA.htm", Directory.GetCurrentDirectory()));
            _driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 10)); //10 seconds
            _driver.Navigate().GoToUrl(url);
            int ctr = 0;
            while (ctr <=100)
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                //var ex =Assert.Throws<WebDriverTimeoutException>(() => wait.Until(ExpectedConditions.ElementExists(By.Id("text1"))));

                if (_driver.WaitUntilExists(By.Id("select1")))
                {
                    var list = new SelectElement(_driver.FindElement(By.Id("select1")));
                    list.SelectByText("one");
                    Thread.Sleep(1000);
                    list.SelectByText("two");
                    Thread.Sleep(1000);
                    list.SelectByText("three");
                    Thread.Sleep(1000);

                }
                ctr++;
            }
        }
    }
}
