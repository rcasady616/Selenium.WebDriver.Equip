using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Medrio.QA.UITestFramework.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtension.Nunit;
using TestWebPages.UIFramework.Pages;

namespace SeleniumExtension.Tests.Extensions
{
    [TestFixture]
    public class IWebDriverExtensionTests : BaseTest
    {
        private AjaxyControlPage ajaxyControlPage;

        [SetUp]
        public void SetupTest()
        {
            string url = string.Format(@"file:///{0}../../../..{1}", Directory.GetCurrentDirectory(), AjaxyControlPage.Url);
            Driver.Navigate().GoToUrl(url);
            ajaxyControlPage = new AjaxyControlPage(Driver);
            Assert.AreEqual(true, ajaxyControlPage.IsPageLoaded());
        }

        [Test]
        public void TestSwitchBrowserWindow()
        {
            Driver.Navigate().GoToUrl(string.Format(@"file:///{0}../../../..{1}", Directory.GetCurrentDirectory(), IndexPage.Url));
            var index = new IndexPage(Driver);

            index.AjaxyControlNewWindowLink.Click();
            Driver.SwitchBrowserWindow(ExpectedConditions.TitleIs("AjaxyControl"));

            var ajaxyControl = new AjaxyControlPage(Driver);
            Assert.That(ajaxyControl.IsPageLoaded());
        }

        [Test]
        public void TestSwitchBrowserWindowNull()
        {
            Driver.Navigate().GoToUrl(string.Format(@"file:///{0}../../../..{1}", Directory.GetCurrentDirectory(), IndexPage.Url));
            var index = new IndexPage(Driver);

            index.AjaxyControlNewWindowLink.Click();
            Assert.IsNull(Driver.SwitchBrowserWindow(ExpectedConditions.TitleIs("false")));
        }

        #region Waits

        [TestCase(true, "AjaxyControl")]
        [TestCase(false, "NeverGonnaGetItNeverGonnaGetIt")]
        public void TestWaitUntilTitleIs(bool expected, string title)
        {
            Assert.AreEqual(expected, Driver.WaitUntilTitleIs(title));
        }

        #endregion

        #region click

        [Test, TestCaseSource("GetLocators")]
        public void TestClickWaitForCondition(By locator)
        {
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            ajaxyControlPage.SubmitButton.ClickWaitForCondition(Driver, ExpectedConditions.ElementExists(locator));
            Assert.AreEqual(true, Driver.ElementExists(locator));
        }

        [Test]
        public void TestClickWaitForConditionFalse()
        {
            By locator = By.Id("false");
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            ajaxyControlPage.SubmitButton.ClickWaitForCondition(Driver, ExpectedConditions.ElementExists(locator));
            Assert.AreEqual(false, Driver.ElementExists(locator));
        }

        [Test, TestCaseSource("GetLocators")]
        public void TestClickWaitUnilVisable(By locator)
        {
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            Assert.AreEqual(true, ajaxyControlPage.SubmitButton.ClickWaitUnilVisable(Driver, locator));
            Assert.AreEqual(true, Driver.ElementExists(locator));
        }

        [Test, TestCaseSource("GetLocators")]
        public void TestClickWaitUnilVisableFalse(By locator)
        {
            var indexPage = new IndexPage(Driver);
            Driver.Navigate().GoToUrl(string.Format(@"file:///{0}../../../..{1}", Directory.GetCurrentDirectory(), IndexPage.Url));
            Assert.AreEqual(false, indexPage.PageALink.ClickWaitUnilVisable(Driver, locator, 2));
            Assert.AreEqual(false, Driver.ElementExists(locator));
        }

        [Test, TestCaseSource("GetAjaxyControlPageLocators")]
        public void TestClickWaitForConditions(List<By> locators)
        {
            var ex = locators.Select(ExpectedConditions.ElementExists).ToList();

            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            ajaxyControlPage.SubmitButton.ClickWaitForConditions(Driver, ex);

            foreach (var locator in locators)
            {
                Assert.AreEqual(true, Driver.ElementExists(locator));
            }
        }

        [Test]
        public void TestClickWaitForConditionsFalse()
        {
            List<By> locators = new List<By>() { By.Id("false"), By.Id("nothome") };

            var ex = locators.Select(ExpectedConditions.ElementExists).ToList();

            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            ajaxyControlPage.SubmitButton.ClickWaitForConditions(Driver, ex);

            foreach (var locator in locators)
            {
                Assert.AreEqual(false, Driver.ElementExists(locator));
            }
        }

        [Test, TestCaseSource("GetAjaxyControlPageLocators")]
        public void TestClickWaitUnilVisables(List<By> locators)
        {
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");

            Assert.AreEqual(true, ajaxyControlPage.SubmitButton.ClickWaitUnilVisables(Driver, locators));
            foreach (var locator in locators)
            {
                Assert.AreEqual(true, Driver.ElementExists(locator));
            }
        }

        [Test, TestCaseSource("GetAjaxyControlPageLocators")]
        public void TestClickWaitUnilVisablesFalse(List<By> locators)
        {
            var indexPage = new IndexPage(Driver);
            Driver.Navigate().GoToUrl(string.Format(@"file:///{0}../../../..{1}", Directory.GetCurrentDirectory(), IndexPage.Url));

            Assert.AreEqual(false, indexPage.PageALink.ClickWaitUnilVisables(Driver, locators, 2));
            foreach (var locator in locators)
            {
                Assert.AreEqual(false, Driver.ElementExists(locator));
            }
        }

        [Test]
        public void TestClickWaitForPage()
        {
            var indexPage = new IndexPage(Driver);
            Driver.Navigate().GoToUrl(string.Format(@"file:///{0}../../../..{1}", Directory.GetCurrentDirectory(), IndexPage.Url));
            Assert.That(indexPage.IsPageLoaded());
            Assert.DoesNotThrow(delegate { indexPage.AjaxyControlLink.ClickWaitForPage<AjaxyControlPage>(Driver); });
        }

        [Test]
        public void TestClickWaitForPageFalse()
        {
            var indexPage = new IndexPage(Driver);
            Driver.Navigate().GoToUrl(string.Format(@"file:///{0}../../../..{1}", Directory.GetCurrentDirectory(), IndexPage.Url));
            Assert.That(indexPage.IsPageLoaded());
            Assert.Throws<PageNotLoadedException>(delegate { indexPage.AjaxyControlLink.ClickWaitForPage<IndexPage>(Driver); });
        }

        #endregion

        [Test]
        public void TestTakeScreenShot()
        {
            string file = "TestTakeScreenShot.jpeg";
            Driver.Navigate().GoToUrl(string.Format(@"file:///{0}../../../..{1}", Directory.GetCurrentDirectory(), IndexPage.Url));
            Assert.DoesNotThrow(delegate { Driver.TakeScreenShot(file, ImageFormat.Jpeg); });
            Assert.AreEqual(true, File.Exists(file));
            var f = new FileInfo(file);
            Assert.GreaterOrEqual(f.Length, 9000, "Check to see if the file is around expected size");
        }

        #region testdata

        public static List<List<By>> GetAjaxyControlPageLocators()
        {
            var ret = new List<List<By>>();
            ret.Add(new List<By>() { AjaxyControlPage.ByLabelsDiv });
            ret.Add(new List<By>() { AjaxyControlPage.ByLabelsDiv, AjaxyControlPage.ByGreenRadio });
            ret.Add(new List<By>() { AjaxyControlPage.ByLabelsDiv, AjaxyControlPage.ByGreenRadio, AjaxyControlPage.ByRedRadio });
            ret.Add(new List<By>() { AjaxyControlPage.ByLabelsDiv, AjaxyControlPage.ByGreenRadio, AjaxyControlPage.ByRedRadio });
            ret.Add(new List<By>() { AjaxyControlPage.ByLabelsDiv, AjaxyControlPage.ByGreenRadio, AjaxyControlPage.ByRedRadio, AjaxyControlPage.BySubmitButton });
            ret.Add(new List<By>() { AjaxyControlPage.ByLabelsDiv, AjaxyControlPage.ByGreenRadio, AjaxyControlPage.ByRedRadio, AjaxyControlPage.BySubmitButton, AjaxyControlPage.ByNewLableText });
            return ret;
        }

        public static List<By> GetLocators()
        {
            return new List<By>
                {
                    AjaxyControlPage.ByLabelsDiv,
                    AjaxyControlPage.ByGreenRadio,
                    AjaxyControlPage.ByRedRadio,
                    AjaxyControlPage.BySubmitButton,
                    AjaxyControlPage.ByNewLableText
                };
        }

        #endregion
    }
}
