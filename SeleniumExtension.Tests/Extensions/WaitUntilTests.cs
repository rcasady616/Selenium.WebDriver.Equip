using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using TestWebPages.UIFramework.Pages;

namespace SeleniumExtension.Tests.Extensions
{
    public class WaitUntilTests : BasePage
    {
        private AjaxyControlPage ajaxyControlPage;
        public ISearchContext item;

        [SetUp]
        public void SetupTest()
        {
            string url = string.Format(@"file:///{0}../../../..{1}", Directory.GetCurrentDirectory(),
                                       AjaxyControlPage.Url);
            Driver = WebDriverFactory.GetBrowser(url);
            ajaxyControlPage = new AjaxyControlPage(Driver);
            Assert.AreEqual(true, ajaxyControlPage.IsPageLoaded());

        }

        [TestCase(true, "red")]
        [TestCase(false, "NeverGonnaGetItNeverGonnaGetIt")]
        public void TestWaitUntilExists(bool expected, string id)
        {
            Assert.AreEqual(expected, item.WaitUntilExists(By.Id(id)));
        }

        [TestCase]
        public void TestWaitUntilExistsElementLoadedByJavaScriptAfterPageLoad()
        {
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            ajaxyControlPage.SubmitButton.Click();
            Assert.AreEqual(true, item.WaitUntilVisible(AjaxyControlPage.ByLabelsDiv));
        }

        [TestCase(false, "red")]
        [TestCase(true, "NeverGonnaGetItNeverGonnaGetIt")]
        public void TestWaitUntilNotExists(bool expected, string id)
        {
            Assert.AreEqual(expected, item.WaitUntilNotExists(By.Id(id)));
        }

        [TestCase(true, "red")]
        [TestCase(false, "NeverGonnaGetItNeverGonnaGetIt")]
        public void TestWaitUntilVisible(bool expected, string id)
        {
            Assert.AreEqual(expected, item.WaitUntilVisible(By.Id(id), 2));
        }

        [TestCase]
        public void TestWaitUntilVisibleElementLoadedByJavaScriptAfterPageLoad()
        {
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            ajaxyControlPage.SubmitButton.Click();
            Assert.AreEqual(true, item.WaitUntilVisible(AjaxyControlPage.ByLabelsDiv));
        }

        [Test, TestCaseSource("GetAjaxyControlPageLocators")]
        public void TestWaitUntilVisible(List<By> locators)
        {
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            ajaxyControlPage.SubmitButton.Click();
            Assert.AreEqual(true, item.WaitUntilVisible(locators));
        }

        [Test, TestCaseSource("GetAjaxyControlPageLocators")]
        public void TestWaitUntilVisibleFalse(List<By> locators)
        {
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            Assert.AreEqual(false, item.WaitUntilVisible(locators, 2));
        }

        [Test, TestCaseSource("GetAjaxyControlPageLocators")]
        public void TestWaitUntilNotVisibleTrue(List<By> locators)
        {
            Driver.Navigate().GoToUrl("http://rickcasady.blogspot.com/2013/08/automate-like-superhero.html");
            Assert.AreEqual(true, item.WaitUntilNotVisible(locators));
        }

        [Test, TestCaseSource("GetAjaxyControlPageLocators")]
        public void TestWaitUntilNotVisibleFalse(List<By> locators)
        {
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestWaitUntilNotVisiblesFalse");
            ajaxyControlPage.SubmitButton.Click();
            Assert.AreEqual(false, item.WaitUntilNotVisible(locators));
        }

        [TestCase(true, "TestIsPageLoaded", 10)]
        [TestCase(false, "NeverGonnaGetItNeverGonnaGetIt", 2)]
        public void TestWaitUntilTextEquals(bool expected, string text, int waitTime)
        {
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            ajaxyControlPage.SubmitButton.Click();
            Assert.AreEqual(expected, item.WaitUntilTextEquals(AjaxyControlPage.ByLabelsDiv, text, waitTime));
        }

        [TestCase(false, "TestIsPageLoaded")]
        [TestCase(true, "NeverGonnaGetItNeverGonnaGetIt")]
        public void TestWaitUntilTextNotEquals(bool expected, string text)
        {
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            ajaxyControlPage.SubmitButton.Click();
            Assert.AreEqual(expected, item.WaitUntilTextNotEquals(AjaxyControlPage.ByLabelsDiv, text, 10));
        }

        [TestCase(false, "NeverGonnaGetItNeverGonnaGetIt")]
        public void TestWaitUntilTextNotEqualsForNoneExistingElement(bool expected, string text)
        {
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            ajaxyControlPage.SubmitButton.Click();
            Assert.AreEqual(expected, item.WaitUntilTextNotEquals(By.Id(text), text, 10));
        }

        [TestCase(true, "TestIsPageLoaded", 10)]
        [TestCase(true, "Load", 10)]
        [TestCase(false, "NeverGonnaGetItNeverGonnaGetIt", 2)]
        public void TestWaitUntilTextContains(bool expected, string text, int waitTime)
        {
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            ajaxyControlPage.SubmitButton.Click();
            Assert.AreEqual(expected, item.WaitUntilTextContains(AjaxyControlPage.ByLabelsDiv, text, waitTime));
        }

        [TestCase(false, "TestIsPageLoaded", 2)]
        [TestCase(false, "Load", 2)]
        [TestCase(true, "NeverGonnaGetItNeverGonnaGetIt", 10)]
        public void TestWaitUntilTextNotContains(bool expected, string text, int waitTime)
        {
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            ajaxyControlPage.SubmitButton.Click();
            Assert.AreEqual(expected, item.WaitUntilTextNotContains(AjaxyControlPage.ByLabelsDiv, text, waitTime));
        }

        [TestCase(false, "NeverGonnaGetItNeverGonnaGetIt", 10)]
        public void TestWaitUntilTextNotContainsElementNoneExist(bool expected, string text, int waitTime)
        {
            ajaxyControlPage.GreenRadio.Click();
            ajaxyControlPage.NewLabelText.SendKeys("TestIsPageLoaded");
            ajaxyControlPage.SubmitButton.Click();
            Assert.AreEqual(expected, item.WaitUntilTextNotContains(By.Id(text), text, waitTime));
        }

        [TestCase("text1", "myinput", HtmlTagAttribute.Class)]
        [TestCase("label1", "label one", HtmlTagAttribute.Title)]
        [TestCase("add1", "add", HtmlTagAttribute.Value)]
        [TestCase("add1", "", HtmlTagAttribute.Class)]
        public void TestWaitUntilAttributeEquals(string id, string expectedValue, string htmlTagAttribute)
        {
            Assert.AreEqual(true, item.WaitUntilAttributeEquals(By.Id(id), htmlTagAttribute, expectedValue));
        }

        [TestCase("text1", "myinput", HtmlTagAttribute.Class)]
        [TestCase("label1", "label one", HtmlTagAttribute.Title)]
        [TestCase("add1", "add", HtmlTagAttribute.Value)]
        [TestCase("add1", "", HtmlTagAttribute.Class)]
        public void TestWaitUntilAttributeEqualsFalse(string id, string expectedValue, string htmlTagAttribute)
        {
            Assert.AreEqual(false, item.WaitUntilAttributeEquals(By.Id(id), htmlTagAttribute, expectedValue, 2));
        }

        [TestCase("text1", "myinput", HtmlTagAttribute.Class)]
        [TestCase("label1", "label one", HtmlTagAttribute.Title)]
        [TestCase("add1", "add", HtmlTagAttribute.Value)]
        [TestCase("add1", "", HtmlTagAttribute.Class)]
        public void TestWaitUntilAttributeNotEquals(string id, string expectedValue, string htmlTagAttribute)
        {
            string newValue = "newValue";
            Driver.FindElement(id).SetAttribute(htmlTagAttribute, newValue);
            Assert.AreEqual(true, item.WaitUntilAttributeNotEquals(By.Id(id), htmlTagAttribute, expectedValue));
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

    [TestFixture]
    public class WaitUntilSearchContextTests : WaitUntilTests
    {
        [SetUp]
        public void SetupTest2()
        {
            item = Driver.FindElement(By.TagName("body"));
        }
    }

    [TestFixture]
    public class WaitUnitlDriverTests : WaitUntilTests
    {
        [SetUp]
        public void SetupTest2()
        {
            item = Driver;
        }
    }
}
