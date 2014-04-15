using Medrio.QA.UITestFramework.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtension.Nunit;
using TestWebPages.UIFramework.Pages;

namespace SeleniumExtension.Tests.Extensions
{
    [TestFixture]
    [Category("Extension")]
    public class IWebElementExtensionTests : BaseTest
    {
        [SetUp]
        public void SetupTest()
        {
            Driver.Navigate().GoToUrl(pageAUrl);
        }
        
        #region Mock properties extention

        [Test]
        public void TestClassNameExtention()
        {
            Assert.AreEqual("myinput", Driver.FindElement("text1").ClassName());
        }

        [Test]
        public void TestIdExtention()
        {
            Assert.AreEqual("text1", Driver.FindElement("text1").Id());
        }

        [Test]
        public void TestNameExtention()
        {
            Assert.AreEqual("text1", Driver.FindElement("text1").Name());
        }

        [Test]
        public void TestNextSiblingExtention()
        {
            Assert.AreEqual(Driver.FindElement("tdthree"), Driver.FindElement("tdtwo").NextSibling());
        }

        [Test]
        public void TestParentExtention()
        {
            Assert.AreEqual(Driver.FindElement("tdone"), Driver.FindElement("text1").Parent());
        }

        [Test]
        public void TestPreviousSiblingExtention()
        {
            Assert.AreEqual(Driver.FindElement("tdone"), Driver.FindElement("tdtwo").PreviousSibling());
        }

        [Test]
        public void TestStyleExtention()
        {
            string style = Driver.FindElement("label1").Style().Replace(" ", "");
            Assert.AreEqual("background-color:red;", style);
        }

        [Test]
        public void TestTitleExtention()
        {
            Assert.AreEqual("label one", Driver.FindElement("label1").Title());
        }

        [Test]
        public void TestTypeExtention()
        {
            Assert.AreEqual("button", Driver.FindElement("add1").Type());
        }

        [Test]
        public void TestValueExtention()
        {
            Assert.AreEqual("add", Driver.FindElement("add1").Value());
        }

        #endregion

        [TestCase("text1", "myinput", HtmlTagAttribute.Class)]
        [TestCase("label1", "label one", HtmlTagAttribute.Title)]
        [TestCase("add1", "add", HtmlTagAttribute.Value)]
        [TestCase("add1", "", HtmlTagAttribute.Class)]
        public void TestGetAttribute(string id, string expectedValue, string htmlTagAttribute)
        {
            Assert.AreEqual(expectedValue, Driver.FindElement(id).GetAttribute(htmlTagAttribute));
        }

        [TestCase("text1", HtmlTagAttribute.Class)]
        [TestCase("label1", HtmlTagAttribute.Title)]
        [TestCase("add1", HtmlTagAttribute.Value)]
        public void TestSetAttribute(string id, string htmlTagAttribute)
        {
            string expectedValue = "newValue";
            Driver.FindElement(id).SetAttribute(htmlTagAttribute, expectedValue);
            Assert.AreEqual(expectedValue, Driver.FindElement(id).GetAttribute(htmlTagAttribute));
        }

        [Test]
        public void TestClickWaitUntilPost()
        {
            var indexPage = new IndexPage(Driver);
            Driver.Navigate().GoToUrl(IndexPage.Url);
            Assert.That(indexPage.IsPageLoaded());
            Assert.That(indexPage.AjaxyControlLink.ClickWaitUntilPost(Driver));
        }

        [Test]
        public void TestClickWaitUntilPostFlase()
        {
            var ajaxPage = new AjaxyControlPage(Driver);
            Driver.Navigate().GoToUrl(AjaxyControlPage.Url);
            Assert.That(ajaxPage.IsPageLoaded());
            Assert.That(!ajaxPage.GreenRadio.ClickWaitUntilPost(Driver));
        }
    }
}
