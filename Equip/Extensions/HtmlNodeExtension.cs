using NMock2;
using OpenQA.Selenium;
using System.Text.RegularExpressions;

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
                    tempElement = CreateNMock2IWebElement(child);
                    var byID = By.Id(child.Id);
                    Stub.On(mockIWebElement).Method("FindElement").With(byID).Will(Return.Value(tempElement));
                }
            }
            return mockIWebElement;
        }

        private static string GetAttributeValue(HtmlAttributeCollection attributes, string attributeName)
        {
            var attribute = attributes[attributeName];
            if (attribute != null)
                return attribute.Value;
            return "";
        }

        public static string ToCssSelectorString(this HtmlNode htmlNode)
        {
            string cssString = null;

            var attributes = htmlNode.Attributes;
            var id = GetAttributeValue(attributes, "id");
            if (!string.IsNullOrEmpty(id))
                return $"#{id}";

            switch (htmlNode.Name)
            {
                case "a":
                case "A":
                    var href = GetAttributeValue(attributes, "href");
                    if (!string.IsNullOrEmpty(href))
                    {
                        cssString = $"a[href='{href}']";
                        break;
                    }
                    var text = "";
                    var textNode = htmlNode.SelectSingleNode("text()");
                    if (textNode != null)
                        text = textNode.InnerText;

                    if (!string.IsNullOrEmpty(text))
                        cssString = ""; // todo return a differnt type of locator (by xpath or by linktext)
                    break;
                case "label":
                case "Label":
                    var labelText = "";
                    var labelTextNode = htmlNode.SelectSingleNode("text()");
                    if (labelTextNode != null)
                        labelText = labelTextNode.InnerText;
                    if (!string.IsNullOrEmpty(labelText))
                        cssString = ""; // todo return a differnt type of locator (by xpath or by linktext)
                    break;
                default:
                    cssString = null;
                    break;
            }
            return cssString;
        }

        public static string ToNameString(this HtmlNode htmlNode)
        {
            string nameString = null;

            var attributes = htmlNode.Attributes;
            var id = GetAttributeValue(attributes, "id");
            if (!string.IsNullOrEmpty(id))
                nameString = id;

            switch (htmlNode.Name)
            {
                case "a":
                case "A":
                    if (string.IsNullOrEmpty(nameString))
                    {
                        var text = "";
                        var textNode = htmlNode.SelectSingleNode("text()");
                        if (textNode != null)
                            text = textNode.InnerText;
                        if (!string.IsNullOrEmpty(text))
                        {
                            nameString = text.Replace(" ", "");
                        }
                    }
                    if (string.IsNullOrEmpty(nameString))
                    {
                        var href = GetAttributeValue(attributes, "href");
                        if (!string.IsNullOrEmpty(href))
                            nameString = Regex.Match(href, @".*\/([^/]*)$").Groups[1].Value.ToString();
                    }
                    nameString = $"{nameString}Link";
                    break;
                case "label":
                case "Label":
                    var labelText = "";
                    var labelTextNode = htmlNode.SelectSingleNode("text()");
                    if (labelTextNode != null)
                        labelText = labelTextNode.InnerText;
                    if (!string.IsNullOrEmpty(labelText))
                        nameString = ""; // todo return a differnt type of locator (by xpath or by linktext)
                    break;
                default:
                    nameString = null;
                    break;
            }
            return nameString;
        }
    }
}
