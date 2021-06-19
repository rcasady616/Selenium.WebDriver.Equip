using HtmlAgilityPack;
//using NMock2;
using OpenQA.Selenium;

namespace Selenium.WebDriver.Equip.Tests
{
    //public class MockTool
    //{
    //    private Mockery mock;
    //    public MockTool(Mockery mockery)
    //    {
    //        mock = mockery;
    //    }

    //    public IWebElement ConvertHtmlToMock(string html)
    //    {
    //        var htmlDoc = $"<html>{html}</html>";
    //        var doc = new HtmlDocument();
    //        doc.LoadHtml(htmlDoc);
    //        var node = doc.DocumentNode.SelectSingleNode("./html/*");
    //        var mockIWebElement = node.ToNMock2IWebElement();
            
    //        if (node.HasChildNodes)
    //        {
    //            foreach (var child in node.ChildNodes)
    //            {
    //                var tempElement = child.ToNMock2IWebElement();
    //                var byID = By.Id(child.Id);
    //                Stub.On(mockIWebElement).Method("FindElement").With(byID).Will(Return.Value(tempElement));
    //            }
    //        }
    //        return mockIWebElement;
    //    }
    //}
}
