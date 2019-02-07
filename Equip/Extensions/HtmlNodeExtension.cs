using NMock2;
using OpenQA.Selenium;

namespace HtmlAgilityPack
{
    public static class HtmlNodeExtension
    {
        private static IWebElement CreateNMock2IWebElement(this HtmlNode htmlNode)
        {
            var mockIWebElement = new Mockery().NewMock<IWebElement>();
            var attributes = htmlNode.Attributes;
            if (attributes["id"] == null)
                Stub.On(mockIWebElement).Method("GetAttribute").With("id").Will(Return.Value(""));

            foreach (var att in attributes)
                Stub.On(mockIWebElement).Method("GetAttribute").With(att.Name).Will(Return.Value(att.Value));

            Stub.On(mockIWebElement).GetProperty("TagName").Will(Return.Value(htmlNode.Name));
            var text = htmlNode.SelectSingleNode("text()");
            if (text != null)
                Stub.On(mockIWebElement).GetProperty("Text").Will(Return.Value(text.InnerText));
            else
                Stub.On(mockIWebElement).GetProperty("Text").Will(Return.Value(""));
            return mockIWebElement;
        }

        public static IWebElement ToNMock2IWebElement(this HtmlNode htmlNode)
        {
            var mockIWebElement = CreateNMock2IWebElement(htmlNode);
            if (htmlNode.HasChildNodes)
            {
                foreach (var child in htmlNode.ChildNodes)
                {
                    var tempElement = new Mockery().NewMock<IWebElement>();
                    tempElement = CreateNMock2IWebElement( child);
                    var byID = By.Id(child.Id);
                    Stub.On(mockIWebElement).Method("FindElement").With(byID).Will(Return.Value(tempElement));
                }
            }

            return mockIWebElement;
        }
    }
}
