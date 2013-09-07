using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using TestWebPages.UIFramework.Pages;

namespace SeleniumExtension.Tests
{
    [TestFixture]
    public class IWebElementExtentionTests
    {
        private IWebDriver _driver = null;
        [SetUp]
        public void SetupTest()
        {
            _driver = IWebDriverFactory.GetBrowser(string.Format(@"file:///{0}../../../../TestWebPages/PageA.htm", Directory.GetCurrentDirectory()));
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

        #region Mock properties extention

        [Test]
        public void TestClassNameExtention()
        {
            Assert.AreEqual("myinput", _driver.FindElement("text1").ClassName());
        }

        [Test]
        public void TestIdExtention()
        {
            Assert.AreEqual("text1", _driver.FindElement("text1").Id());
        }

        [Test]
        public void TestNameExtention()
        {
            Assert.AreEqual("text1", _driver.FindElement("text1").Name());
        }

        [Test]
        public void TestNextSiblingExtention()
        {
            Assert.AreEqual(_driver.FindElement("tdthree"), _driver.FindElement("tdtwo").NextSibling());
        }

        [Test]
        public void TestParentExtention()
        {
            Assert.AreEqual(_driver.FindElement("tdone"), _driver.FindElement("text1").Parent());
        }

        [Test]
        public void TestPreviousSiblingExtention()
        {
            Assert.AreEqual(_driver.FindElement("tdone"), _driver.FindElement("tdtwo").PreviousSibling());
        }

        [Test]
        public void TestStyleExtention()
        {
            string style = _driver.FindElement("label1").Style().Replace(" ", "");
            Assert.AreEqual("background-color:red;", style);
        }

        [Test]
        public void TestTitleExtention()
        {
            Assert.AreEqual("label one", _driver.FindElement("label1").Title());
        }

        [Test]
        public void TestTypeExtention()
        {
            Assert.AreEqual("button", _driver.FindElement("add1").Type());
        }

        [Test]
        public void TestValueExtention()
        {
            Assert.AreEqual("add", _driver.FindElement("add1").Value());
        }

        #endregion

        [TestCase("text1", "myinput")]
        public void TestGetAttributeClass(string id,  string expected)
        {
            Assert.AreEqual(expected, _driver.FindElement(id).GetAttribute(HtmlTagAttribute.Class));
        }

        [TestCase("label1", "label one")]
        public void TestGetAttributeTitle(string id, string expected)
        {
            Assert.AreEqual(expected, _driver.FindElement(id).GetAttribute(HtmlTagAttribute.Title));
        }

        [TestCase("add1", "add")]
        public void TestGetAttributeValue(string id,  string expected)
        {
            Assert.AreEqual(expected, _driver.FindElement(id).GetAttribute(HtmlTagAttribute.Value));
        }

        [TestCase(true, "red")]
        [TestCase(false, "NeverGonnaGetNeverGonnaGet")]
        public void TestWaitUntilExistsIWebElement(bool expected, string id)
        {
            string url = string.Format(@"file:///{0}../../../..{1}", Directory.GetCurrentDirectory(), AjaxyControlPage.Url);
            _driver.Navigate().GoToUrl(url);
            var body = _driver.FindElement(By.TagName("body"));
            Assert.AreEqual(expected, body.WaitUntilExists(By.Id(id)));
        }
    }
}
