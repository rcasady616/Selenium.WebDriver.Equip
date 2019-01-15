using System.Diagnostics;
using Medrio.QA.UITestFramework.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using TestWebPages.UIFramework.Pages;
using NMock2;

namespace Selenium.WebDriver.Equip.Tests.Extensions
{
    [TestFixture]
    [Category(TestCategories.Extension)]
    public class WebElementExtensionTests : BaseTest
    {
        public string PageAUrl = "http://rickcasady.com/SeleniumExtentions/v1.0/TestWebPages/PageA.htm";

        [SetUp]
        public void SetupWebElementExtensionTests()
        {
            Driver.Navigate().GoToUrl(PageAUrl);
        }

        [Test]
        public void TestNextSiblingExtention()
        {
            Assert.AreEqual(Driver.FindElement(By.Id("tdthree")), Driver.FindElement(By.Id("tdtwo")).NextSibling());
        }

        [Test]
        public void TestParentExtention()
        {
            Assert.AreEqual(Driver.FindElement(By.Id("tdone")), Driver.FindElement(By.Id("text1")).Parent());
        }

        [Test]
        public void TestPreviousSiblingExtention()
        {
            Assert.AreEqual(Driver.FindElement(By.Id("tdone")), Driver.FindElement(By.Id("tdtwo")).PreviousSibling());
        }

        [Category("Unit")]
        [TestCase("text1", "myinput", HtmlTagAttribute.Class)]
        [TestCase("label1", "label one", HtmlTagAttribute.Title)]
        [TestCase("add1", "add", HtmlTagAttribute.Value)]
        [TestCase("add1", "", HtmlTagAttribute.Class)]
        public void TestGetAttribute(string id, string expectedValue, string htmlTagAttribute)
        {
            Assert.AreEqual(expectedValue, Driver.FindElement(By.Id(id)).GetAttribute(htmlTagAttribute));
        }

        [Category("Unit")]
        [TestCase("text1", HtmlTagAttribute.Class)]
        [TestCase("label1", HtmlTagAttribute.Title)]
        [TestCase("add1", HtmlTagAttribute.Value)]
        public void TestSetAttribute(string id, string htmlTagAttribute)
        {
            const string expectedValue = "newValue";
            Driver.FindElement(By.Id(id)).SetAttribute(htmlTagAttribute, expectedValue);
            Assert.AreEqual(expectedValue, Driver.FindElement(By.Id(id)).GetAttribute(htmlTagAttribute));
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

        [Category("unit")]
        [TestCase(true, "add1")]
        [TestCase(false, "NeverGonnaGetItNeverGonnaGetIt")]
        public void TestWaitUntilExist(bool expected, string id)
        {
            var WaitTime = 2;
            var sw = new Stopwatch();
            sw.Start();
            var actual = Driver.WaitUntilExists(By.Id(id), WaitTime);
            sw.Stop();
            if (expected)
                Assert.Less(sw.Elapsed.Seconds, WaitTime);
            else
            {
                Assert.LessOrEqual(sw.Elapsed.Seconds, WaitTime + 1);
                Assert.GreaterOrEqual(sw.Elapsed.Seconds, WaitTime);
            }
            Assert.AreEqual(expected, actual);
        }

        [Category("unit")]
        [TestCase(false, "add1")]
        [TestCase(true, "NeverGonnaGetItNeverGonnaGetIt")]
        public void TestWaitUntilNotExist(bool expected, string id)
        {
            var WaitTime = 2;
            var sw = new Stopwatch();
            sw.Start();
            var actual = Driver.WaitUntilNotExists(By.Id(id), WaitTime);
            sw.Stop();
            if (expected)
                Assert.Less(sw.Elapsed.Seconds, WaitTime);
            else
            {
                Assert.LessOrEqual(sw.Elapsed.Seconds, WaitTime + 1);
                Assert.GreaterOrEqual(sw.Elapsed.Seconds, WaitTime);
            }
            Assert.AreEqual(expected, actual);
        }
    }

    [TestFixture]
    [Category(TestCategories.Extension)]
    [Category(TestCategories.HeadLess)]
    public class HeadLessWebElementExtensionTests
    {
        private Mockery mocks;
        private IWebElement webElement;

        public IWebElement WebElement { get { return webElement; } set { webElement = value; } }

        [SetUp]
        public void SetUp()
        {
            mocks = new Mockery();
            webElement = mocks.NewMock<IWebElement>();
        }

        [Test]
        public void TestClassNameExtention()
        {
            string className = "r2d2";
            Stub.On(WebElement).Method("GetAttribute").With(HtmlTagAttribute.Class).Will(Return.Value(className));
            StringAssert.AreEqualIgnoringCase(className, WebElement.ClassName());
        }

        [Test]
        public void TestIdExtention()
        {
            string id = "3133";
            Stub.On(WebElement).Method("GetAttribute").With(HtmlTagAttribute.Id).Will(Return.Value(id));
            StringAssert.AreEqualIgnoringCase(id, WebElement.Id());
        }

        [Test]
        public void TestNameExtention()
        {
            string name = "Lola";
            Stub.On(WebElement).Method("GetAttribute").With(HtmlTagAttribute.Name).Will(Return.Value(name));
            Assert.AreEqual(name, WebElement.Name());
        }
        
        [Test]
        public void TestStyleExtention()
        {
            string style = "background-color:red;";
            Stub.On(WebElement).Method("GetAttribute").With(HtmlTagAttribute.Style).Will(Return.Value(style));
            Assert.AreEqual( style,WebElement.Style());
        }

        [Test]
        public void TestTitleExtention()
        {
            string title = "MR MRS SR JR";
            Stub.On(WebElement).Method("GetAttribute").With(HtmlTagAttribute.Title).Will(Return.Value(title));
            Assert.AreEqual(title, WebElement.Title());
        }

        [Test]
        public void TestTypeExtention()
        {
            string type = "compact";
            Stub.On(WebElement).Method("GetAttribute").With(HtmlTagAttribute.Type).Will(Return.Value(type));
            Assert.AreEqual(type, WebElement.Type());
        }

        [Test]
        public void TestValueExtention()
        {
            string val = "max";
            Stub.On(WebElement).Method("GetAttribute").With(HtmlTagAttribute.Value).Will(Return.Value(val));
            Assert.AreEqual(val, WebElement.Value());
        }
    }
}