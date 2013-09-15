using System.Collections.Generic;
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

        [TestCase(true, "red")]
        [TestCase(false, "NeverGonnaGetItNeverGonnaGetIt")]
        public void TestWaitUntilExists(bool expected, string id)
        {
            Assert.AreEqual(expected, Driver.WaitUntilExists(By.Id(id), 2));
        }

        [TestCase]
        public void TestWaitUntilExists()
        {
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            ajaxyControlPage.SubmitButton.Click();
            Assert.AreEqual(true, Driver.WaitUntilExists(AjaxyControlPage.ByLabelsDiv));
        }

        [TestCase(false, "red")]
        [TestCase(true, "NeverGonnaGetItNeverGonnaGetIt")]
        public void TestWaitUntilNotExists(bool expected, string id)
        {
            Assert.AreEqual(expected, Driver.WaitUntilNotExists(By.Id(id)));
        }

        [TestCase(true, "red")]
        [TestCase(false, "NeverGonnaGetItNeverGonnaGetIt")]
        public void TestWaitUntilVisible(bool expected, string id)
        {
            Assert.AreEqual(expected, Driver.WaitUntilVisible(By.Id(id), 2));
        }

        [TestCase]
        public void TestWaitUntilVisible()
        {
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            ajaxyControlPage.SubmitButton.Click();
            Assert.AreEqual(true, Driver.WaitUntilVisible(AjaxyControlPage.ByLabelsDiv));
        }

        [Test, TestCaseSource("GetAjaxyControlPageLocators")]
        public void TestWaitUntilVisiblesTrue(List<By> locators)
        {
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            ajaxyControlPage.SubmitButton.Click();
            Assert.AreEqual(true, Driver.WaitUntilVisible(locators));
        }

        [Test, TestCaseSource("GetAjaxyControlPageLocators")]
        public void TestWaitUntilVisiblesFalse(List<By> locators)
        {
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            Assert.AreEqual(false, Driver.WaitUntilVisible(locators, 2));
        }

        [Test, TestCaseSource("GetAjaxyControlPageLocators")]
        public void TestWaitUntilNotVisiblesFalse(List<By> locators)
        {
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestWaitUntilNotVisiblesFalse");
            ajaxyControlPage.SubmitButton.Click();
            Assert.AreEqual(false, Driver.WaitUntilNotVisible(locators, 2));
        }

        [Test, TestCaseSource("GetAjaxyControlPageLocators")]
        public void TestWaitUntilNotVisiblesTrue(List<By> locators)
        {
            Driver.Navigate().GoToUrl("http://rickcasady.blogspot.com/2013/08/automate-like-superhero.html");
            Assert.AreEqual(true, Driver.WaitUntilNotVisible(locators));
        }

        [TestCase(true, "AjaxyControl")]
        [TestCase(false, "NeverGonnaGetItNeverGonnaGetIt")]
        public void TestWaitUntilTitleIs(bool expected, string title)
        {
            Assert.AreEqual(expected, Driver.WaitUntilTitleIs(title));
        }

        [TestCase(true, "TestIsPageLoaded")]
        [TestCase(false, "NeverGonnaGetItNeverGonnaGetIt")]
        public void TestWaitUntilTextEquals(bool expected, string text)
        {
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            ajaxyControlPage.SubmitButton.Click();
            Assert.AreEqual(expected, Driver.WaitUntilTextEquals(AjaxyControlPage.ByLabelsDiv, text, 2));
        }

        [TestCase(false, "TestIsPageLoaded")]
        [TestCase(true, "NeverGonnaGetItNeverGonnaGetIt")]
        public void TestWaitUntilTextNotEquals(bool expected, string text)
        {
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            ajaxyControlPage.SubmitButton.Click();
            Assert.AreEqual(expected, Driver.WaitUntilTextNotEquals(AjaxyControlPage.ByLabelsDiv, text, 2));
        }

        [TestCase(true, "TestIsPageLoaded")]
        [TestCase(true, "Load")]
        [TestCase(false, "NeverGonnaGetItNeverGonnaGetIt")]
        public void TestWaitUntilTextContains(bool expected, string text)
        {
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            ajaxyControlPage.SubmitButton.Click();
            Assert.AreEqual(expected, Driver.WaitUntilTextContains(AjaxyControlPage.ByLabelsDiv, text, 2));
        }

        [TestCase(false, "TestIsPageLoaded")]
        [TestCase(false, "Load")]
        [TestCase(true, "NeverGonnaGetItNeverGonnaGetIt")]
        public void TestWaitUntilTextNotContains(bool expected, string text)
        {
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            ajaxyControlPage.SubmitButton.Click();
            Assert.AreEqual(expected, Driver.WaitUntilTextNotContains(AjaxyControlPage.ByLabelsDiv, text, 2));
        }

        [Test, TestCaseSource("GetLocators")]
        public void TestClickWaitForCondition(By locator)
        {
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            ajaxyControlPage.SubmitButton.ClickWaitForCondition(Driver, ExpectedConditions.ElementExists(locator));
            Assert.AreEqual(true, Driver.ElementExists(locator));
        }

        [Test, TestCaseSource("GetLocators")]
        public void TestClickWaitUnilVisable(By locator)
        {
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            Assert.AreEqual(true, ajaxyControlPage.SubmitButton.ClickWaitUnilVisable(Driver, locator));
            Assert.AreEqual(true, Driver.ElementExists(locator));
        }

        [TestCase("text1", "myinput", HtmlTagAttribute.Class)]
        [TestCase("label1", "label one", HtmlTagAttribute.Title)]
        [TestCase("add1", "add", HtmlTagAttribute.Value)]
        [TestCase("add1", "", HtmlTagAttribute.Class)]
        public void TestWaitUntilAttributeEquals(string id, string expectedValue, string htmlTagAttribute)
        {
            Driver.Navigate().GoToUrl(string.Format(@"file:///{0}../../../../TestWebPages/PageA.htm", Directory.GetCurrentDirectory()));
            Assert.AreEqual(true, Driver.WaitUntilAttributeEquals(By.Id(id), htmlTagAttribute, expectedValue));
        }

        [TestCase("text1", "myinput", HtmlTagAttribute.Class)]
        [TestCase("label1", "label one", HtmlTagAttribute.Title)]
        [TestCase("add1", "add", HtmlTagAttribute.Value)]
        [TestCase("add1", "", HtmlTagAttribute.Class)]
        public void TestWaitUntilAttributeEqualsFalse(string id, string expectedValue, string htmlTagAttribute)
        {
            Assert.AreEqual(false, Driver.WaitUntilAttributeEquals(By.Id(id), htmlTagAttribute, expectedValue, 2));
        }

        [TestCase("text1", "myinput", HtmlTagAttribute.Class)]
        [TestCase("label1", "label one", HtmlTagAttribute.Title)]
        [TestCase("add1", "add", HtmlTagAttribute.Value)]
        [TestCase("add1", "", HtmlTagAttribute.Class)]
        public void TestWaitUntilAttributeNotEquals(string id, string expectedValue, string htmlTagAttribute)
        {
            Driver.Navigate().GoToUrl(string.Format(@"file:///{0}../../../../TestWebPages/PageA.htm", Directory.GetCurrentDirectory()));
            string newValue = "newValue";
            Driver.FindElement(id).SetAttribute(htmlTagAttribute, newValue);
            Assert.AreEqual(true, Driver.WaitUntilAttributeNotEquals(By.Id(id), htmlTagAttribute, expectedValue));
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
