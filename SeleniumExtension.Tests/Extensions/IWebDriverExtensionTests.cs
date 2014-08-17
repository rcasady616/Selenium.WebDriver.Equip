using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Medrio.QA.UITestFramework.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestWebPages.UIFramework.Pages;

namespace SeleniumExtension.Tests.Extensions
{
    [TestFixture]
    [Category("Extension")]
    public class IWebDriverExtensionTests : BaseTest
    {
        private AjaxyControlPage ajaxyControlPage;
        private string pageAUrl = "http://rickcasady.com/SeleniumExtentions/v1.0/TestWebPages/PageA.htm";
        private string throwAlertUrl = "http://rickcasady.com/SeleniumExtentions/v1.0/TestWebPages/ThrowAlert.html";

        [SetUp]
        public void SetupTest()
        {
            Driver.Navigate().GoToUrl(AjaxyControlPage.Url);
            ajaxyControlPage = new AjaxyControlPage(Driver);
            Assert.AreEqual(true, ajaxyControlPage.IsPageLoaded());
        }

        [Test]
        public void TestSwitchBrowserWindow()
        {
            Driver.Navigate().GoToUrl(IndexPage.Url);
            var index = new IndexPage(Driver);

            index.AjaxyControlNewWindowLink.Click();
            Driver.SwitchBrowserWindow(ExpectedConditions.TitleIs("AjaxyControl"));

            var ajaxyControl = new AjaxyControlPage(Driver);
            Assert.That(ajaxyControl.IsPageLoaded());
        }

        [Test]
        public void TestSwitchBrowserWindowNull()
        {
            Driver.Navigate().GoToUrl(IndexPage.Url);
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

        [TestCase(true, "TestIsPageLoaded", 10)]
        [TestCase(false, "NeverGonnaGetItNeverGonnaGetIt", 2)]
        public void TestWaitUntilTextEquals(bool expected, string text, int waitTime)
        {
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            ajaxyControlPage.SubmitButton.Click();
            Assert.AreEqual(expected, Driver.WaitUntilTextEquals(AjaxyControlPage.ByLabelsDiv, text, waitTime));
        }

        [TestCase(false, "TestIsPageLoaded")]
        [TestCase(true, "NeverGonnaGetItNeverGonnaGetIt")]
        public void TestWaitUntilTextNotEquals(bool expected, string text)
        {
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            ajaxyControlPage.SubmitButton.Click();
            Assert.AreEqual(expected, Driver.WaitUntilTextNotEquals(AjaxyControlPage.ByLabelsDiv, text, 10));
        }

        [TestCase(false, "NeverGonnaGetItNeverGonnaGetIt")]
        public void TestWaitUntilTextNotEqualsForNoneExistingElement(bool expected, string text)
        {
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            ajaxyControlPage.SubmitButton.Click();
            Assert.AreEqual(expected, Driver.WaitUntilTextNotEquals(By.Id(text), text, 10));
        }

        [TestCase(true, "TestIsPageLoaded", 10)]
        [TestCase(true, "Load", 10)]
        [TestCase(false, "NeverGonnaGetItNeverGonnaGetIt", 2)]
        public void TestWaitUntilTextContains(bool expected, string text, int waitTime)
        {
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            ajaxyControlPage.SubmitButton.Click();
            Assert.AreEqual(expected, Driver.WaitUntilTextContains(AjaxyControlPage.ByLabelsDiv, text, waitTime));
        }

        [TestCase(false, "TestIsPageLoaded", 2)]
        [TestCase(false, "Load", 2)]
        [TestCase(true, "NeverGonnaGetItNeverGonnaGetIt", 10)]
        public void TestWaitUntilTextNotContains(bool expected, string text, int waitTime)
        {
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            ajaxyControlPage.SubmitButton.Click();
            Assert.AreEqual(expected, Driver.WaitUntilTextNotContains(AjaxyControlPage.ByLabelsDiv, text, waitTime));
        }

        [TestCase(false, "NeverGonnaGetItNeverGonnaGetIt", 10)]
        public void TestWaitUntilTextNotContainsElementNoneExist(bool expected, string text, int waitTime)
        {
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            ajaxyControlPage.SubmitButton.Click();
            Assert.AreEqual(expected, Driver.WaitUntilTextNotContains(By.Id(text), text, waitTime));
        }

        [TestCase("text1", "myinput", HtmlTagAttribute.Class)]
        [TestCase("label1", "label one", HtmlTagAttribute.Title)]
        [TestCase("add1", "add", HtmlTagAttribute.Value)]
        [TestCase("add1", "", HtmlTagAttribute.Class)]
        public void TestWaitUntilAttributeEquals(string id, string expectedValue, string htmlTagAttribute)
        {
            Driver.Navigate().GoToUrl(pageAUrl);
            Assert.AreEqual(true, Driver.WaitUntilAttributeEquals(By.Id(id), htmlTagAttribute, expectedValue));
        }

        [TestCase("text1", "notmyinput", HtmlTagAttribute.Class)]
        [TestCase("label1", "not label one", HtmlTagAttribute.Title)]
        [TestCase("add1", "not add", HtmlTagAttribute.Value)]
        [TestCase("text1", "", HtmlTagAttribute.Class)]
        public void TestWaitUntilAttributeEqualsFalse(string id, string expectedValue, string htmlTagAttribute)
        {
            Assert.AreEqual(false, Driver.WaitUntilAttributeEquals(By.Id(id), htmlTagAttribute, expectedValue));
        }

        [TestCase("text1", "myinput", HtmlTagAttribute.Class)]
        [TestCase("label1", "label one", HtmlTagAttribute.Title)]
        [TestCase("add1", "add", HtmlTagAttribute.Value)]
        [TestCase("add1", "", HtmlTagAttribute.Class)]
        public void TestWaitUntilAttributeNotEquals(string id, string expectedValue, string htmlTagAttribute)
        {
            Driver.Navigate().GoToUrl(pageAUrl);
            string newValue = "newValue";
            Driver.FindElement(id).SetAttribute(htmlTagAttribute, newValue);
            Assert.AreEqual(true, Driver.WaitUntilAttributeNotEquals(By.Id(id), htmlTagAttribute, expectedValue));
        }

        [Test]
        public void TestWaitUntilAlertExists()
        {
            Driver.Navigate().GoToUrl(throwAlertUrl);
            Driver.FindElement("button1").Click();
            Assert.AreEqual(true, Driver.WaitUntilAlertExists());
            Driver.SwitchTo().Alert().Accept();
        }

        [Test]
        public void TestWaitUntilAlertTextEquals()
        {
            Driver.Navigate().GoToUrl(throwAlertUrl);
            Driver.FindElement("button1").Click();
            Assert.AreEqual(true, Driver.WaitUntilAlertTextEquals("Hello! I am an alert box!"));
            Driver.SwitchTo().Alert().Accept();
        }

        [Test]
        public void TestWaitUntilAlertTextContains()
        {
            Driver.Navigate().GoToUrl(throwAlertUrl);
            Driver.FindElement("button1").Click();
            Assert.True(Driver.WaitUntilAlertTextContains("I am an alert box"));
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
            Driver.Navigate().GoToUrl(IndexPage.Url);
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
            Driver.Navigate().GoToUrl(IndexPage.Url);

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
            Driver.Navigate().GoToUrl(IndexPage.Url);
            Assert.That(indexPage.IsPageLoaded());
            Assert.DoesNotThrow(delegate { indexPage.AjaxyControlLink.ClickWaitForPage<AjaxyControlPage>(Driver); });
        }

        [Test]
        public void TestClickWaitForPageFalse()
        {
            var indexPage = new IndexPage(Driver);
            Driver.Navigate().GoToUrl(IndexPage.Url);
            Assert.That(indexPage.IsPageLoaded());
            Assert.Throws<PageNotLoadedException>(delegate { indexPage.AjaxyControlLink.ClickWaitForPage<IndexPage>(Driver); });
        }

        #endregion

        [Test]
        public void TestTakeScreenShot()
        {
            string file = "TestTakeScreenShot.jpeg";
            Driver.Navigate().GoToUrl(IndexPage.Url);
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
