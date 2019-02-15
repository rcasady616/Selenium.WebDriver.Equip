using NMock2;
using NUnit.Framework;
using OpenQA.Selenium;
using Selenium.WebDriver.Equip.Elements;
using TestWebPages.UIFramework.Pages;

namespace Selenium.WebDriver.Equip.Tests.Tools
{
    [TestFixture]
    [Category(TestCategories.Extension)]
    public class LinkCheckerTests : BaseTest
    {
        private AjaxyControlPage _ajaxyControlPage;
        public string ThrowAlertUrl = "http://rickcasady.com/SeleniumExtentions/v1.0/TestWebPages/ThrowAlert.html";

        [SetUp]
        public void SetupWebDriverExtensionTests()
        {
            Driver.Navigate().GoToUrl(AjaxyControlPage.Url);
            _ajaxyControlPage = new AjaxyControlPage(Driver);
            Assert.AreEqual(true, _ajaxyControlPage.IsPageLoaded());
        }

        [Category("Unit")]
        [Test]
        public void TestSwitchBrowserWindow()
        {
            Driver.Navigate().GoToUrl(IndexPage.Url);
            var linkChecker = new LinkChecker(Driver);
            linkChecker.HammerLinks();
        }
    }

    [TestFixture]
    [Category(TestCategories.Tools)]
    public class LinkCheckerMockTests
    {
        private Mockery mocks;
        private IWebElement iWebElement;

        [SetUp]
        public void SetUp()
        {
            mocks = new Mockery();
            iWebElement = mocks.NewMock<IWebElement>();
        }

        [Test]
        public void Test()
        {
            var href = "www.RickCasady.com";
            Stub.On(iWebElement).GetProperty("TagName").Will(Return.Value("a"));
            Stub.On(iWebElement).GetProperty("Text").Will(Return.Value("Link Text")); // branch
            Expect.Once.On(iWebElement).Method("GetAttribute").With("id").Will(Return.Value(null)); //branch
            Expect.Once.On(iWebElement).Method("GetAttribute").With("href").Will(Return.Value(href)); //branch
            Assert.AreEqual($"a[href='{href}']", LinkChecker.MakeMap(iWebElement));
        }

        //Link
        [TestCase("a", "Link Text", "101", "www.RickCasady.com", ExpectedResult = "#101", TestName = "Link with id")]
        [TestCase("a", "Link Text", null, "www.RickCasady.com", ExpectedResult = "a[href='www.RickCasady.com']", TestName = "Link With href")]
        [TestCase("a", null, null, "www.RickCasady.com", ExpectedResult = "a[href='www.RickCasady.com']", TestName = "Link With href and no text")]
        [TestCase("a", "Link Text", null, null, ExpectedResult = "", TestName = "Link With no href")]
        //Button
        [TestCase("button", null, "101", null, ExpectedResult = "#101", TestName = "Button with id")]
        [TestCase("button", null, null, null, ExpectedResult = "", TestName = "Button with no id")]
        //Label
        [TestCase("label", null, "101", null, ExpectedResult = "#101", TestName = "Label with id")]
        [TestCase("label", null, null, null, ExpectedResult = null, TestName = "Label with no id and no text")]
        [TestCase("label", "Label Text", null, null, ExpectedResult = "", TestName = "Label with no id")]
        public string TestsMakeMap(string tag, string text, string id, string href)
        {
            Stub.On(iWebElement).GetProperty("TagName").Will(Return.Value(tag));
            Stub.On(iWebElement).GetProperty("Text").Will(Return.Value(text));
            Stub.On(iWebElement).Method("GetAttribute").With(HtmlTagAttribute.Id).Will(Return.Value(id));
            Stub.On(iWebElement).Method("GetAttribute").With(HtmlTagAttribute.Href).Will(Return.Value(href));

            return LinkChecker.MakeMap(iWebElement);
        }

        //Input
        [TestCase("input", Input.Button, "101", ExpectedResult = "#101", TestName = "Input button with id")]
        [TestCase("input", Input.Button, null, ExpectedResult = "", TestName = "Input button with no id")]
        public string TestsMakeMapThrows(string tag, string type, string id)
        {
            Stub.On(iWebElement).GetProperty("TagName").Will(Return.Value(tag));
            Stub.On(iWebElement).Method("GetAttribute").With(HtmlTagAttribute.Type).Will(Return.Value(type));
            Stub.On(iWebElement).Method("GetAttribute").With(HtmlTagAttribute.Id).Will(Return.Value(id));
            return LinkChecker.MakeMap(iWebElement);
        }

        [TestCase("<div ></div>", "div", "", null, "")]
        [TestCase("<span />", "span", "", null, null)]
        [TestCase("<a >Link Text</a>", "a", "Link Text", null, null)]
        [TestCase("<a href='www.RickCasady.com'>Link Text</a>", "a", "Link Text", "www.RickCasady.com", null)]
        [TestCase("<a id='101' href='www.RickCasady.com'>Link Text</a>", "a", "Link Text", "www.RickCasady.com", "101")]
        public void TestsConvertHtmlToMock(string html, string tag, string text, string href, string id)
        {
            var mockTool = new MockTool(mocks);
            var mockElement = mockTool.ConvertHtmlToMock(html);

            Assert.AreEqual(tag, mockElement.TagName);
            Assert.AreEqual(text, mockElement.Text);
            if (!string.IsNullOrEmpty(href)) Assert.AreEqual(href, mockElement.GetAttribute("href"));
            if (!string.IsNullOrEmpty(id)) Assert.AreEqual(id, mockElement.Id());
        }

        [TestCase("<form id='search'><input id='filter' type='text' /></form>", "form", "search", "","input", "filter")]
        [TestCase("<form id='search'>txt1<input id='filter' type='text' /><div>txt2</div></form>", "form", "search", "txt1","input", "filter")]
        public void TestsConvertHtmlToMockWithchildElements(string html, string tag, string id, string text,string childTag, string childId)
        {
            var mockTool = new MockTool(mocks);
            var mockElement = mockTool.ConvertHtmlToMock(html);

            Assert.AreEqual(tag, mockElement.TagName);
            Assert.AreEqual(text, mockElement.Text);
            if (!string.IsNullOrEmpty(id)) Assert.AreEqual(id, mockElement.Id());

            var childWebElement = mockElement.FindElement(By.Id(childId));
            Assert.AreEqual(childTag, childWebElement.TagName);
            Assert.AreEqual(text, mockElement.Text);
        }
    }
}