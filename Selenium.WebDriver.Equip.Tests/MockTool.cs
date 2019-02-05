using HtmlAgilityPack;
using NMock2;
using OpenQA.Selenium;

namespace Selenium.WebDriver.Equip.Tests
{
    public class MockTool
    {
        private Mockery mock;
        public MockTool(Mockery mockery)
        {
            mock = mockery;
        }

        public IWebElement ConvertHtmlToMock(string html)
        {
            var mockIWebElement = mock.NewMock<IWebElement>();
            var htmlDoc = $"<html>{html}</html>";
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlDoc);
            var node = doc.DocumentNode.SelectSingleNode("./html/*");
            ConvertHtmlNodeToMock(mockIWebElement, node);

            if (node.HasChildNodes)
            {
                foreach (var child in node.ChildNodes)
                {
                    var tempElement = mock.NewMock<IWebElement>();
                    ConvertHtmlNodeToMock(tempElement, child);
                    var byID = By.Id(child.Id);
                    Stub.On(mockIWebElement).Method("FindElement").With(byID).Will(Return.Value(tempElement));
                }
            }

            return mockIWebElement;
        }

        private static void ConvertHtmlNodeToMock(IWebElement mockIWebElement, HtmlNode node)
        {
            var attributes = node.Attributes;
            foreach (var att in attributes)
                Stub.On(mockIWebElement).Method("GetAttribute").With(att.Name).Will(Return.Value(att.Value));

            Stub.On(mockIWebElement).GetProperty("TagName").Will(Return.Value(node.Name));
            var text = node.SelectSingleNode("text()");
            if (text != null)
                Stub.On(mockIWebElement).GetProperty("Text").Will(Return.Value(text.InnerText));
            else
                Stub.On(mockIWebElement).GetProperty("Text").Will(Return.Value(""));
        }
    }
}
