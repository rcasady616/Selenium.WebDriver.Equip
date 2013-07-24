using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumExtension.Tests
{
    [TestFixture]
    public class IWebElementExtentionTests
    {
        private IWebDriver driver = null;
        [SetUp]
        public void SetupTest()
        {
            driver = IWebDriverFactory.GetBrowser(string.Format(@"file:///{0}../../../../TestWebPages/PageA.htm", Directory.GetCurrentDirectory()));
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Close();
                driver.Quit();
            }
        }

        #region Mock properties extention

        [Test]
        public void TestClassNameExtention()
        {
            Assert.AreEqual("myinput", driver.FindElement("text1").ClassName());
        }

        [Test]
        public void TestIdExtention()
        {
            Assert.AreEqual("text1", driver.FindElement("text1").Id());
        }

        [Test]
        public void TestNameExtention()
        {
            Assert.AreEqual("text1", driver.FindElement("text1").Name());
        }

        [Test]
        public void TestNextSiblingExtention()
        {
            Assert.AreEqual(driver.FindElement("tdthree"), driver.FindElement("tdtwo").NextSibling());
        }

        [Test]
        public void TestParentExtention()
        {
            Assert.AreEqual(driver.FindElement("tdone"), driver.FindElement("text1").Parent());
        }

        [Test]
        public void TestPreviousSiblingExtention()
        {
            Assert.AreEqual(driver.FindElement("tdone"), driver.FindElement("tdtwo").PreviousSibling());
        }

        [Test]
        public void TestStyleExtention()
        {
            string style = driver.FindElement("label1").Style().Replace(" ", "");
            Assert.AreEqual("background-color:red;", style);
        }

        [Test]
        public void TestTitleExtention()
        {
            Assert.AreEqual("label one", driver.FindElement("label1").Title());
        }

        [Test]
        public void TestTypeExtention()
        {
            Assert.AreEqual("button", driver.FindElement("add1").Type());
        }

        [Test]
        public void TestValueExtention()
        {
            Assert.AreEqual("add", driver.FindElement("add1").Value());
        }

        #endregion

        [TestCase("text1", HtmlTagAttribute.Class, "myinput")]
        [TestCase("label1", HtmlTagAttribute.Title, "label one")]
        [TestCase("add1", HtmlTagAttribute.Value, "add")]
        public void TestGetAttribute(string id, HtmlTagAttribute attribute, string expected)
        {
            Assert.AreEqual(expected, driver.FindElement(id).GetAttribute(attribute));
        }
    }
}
