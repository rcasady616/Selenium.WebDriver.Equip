using HtmlAgilityPack;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Selenium.WebDriver.Equip.PageObjectGenerator
{
    public class VirtualElement
    {
        public HtmlNode HtmlNode;
        public string ParentId { set; get; }
        public string Id { get { return Attributes["id"] != null ? Attributes["id"].Value : ""; } }
        public string TagName { get { return HtmlNode.Name; } }
        public string Text
        {
            get
            {
                var text = HtmlNode.SelectSingleNode("text()");
                return text != null ? text.InnerText : "";
            }
        }
        public string Name { get { return HtmlNode.ToNameString(); } }
        public By Locator { get { return By.CssSelector(LocatorText); } }
        public string LocatorText { get { return HtmlNode.ToCssSelectorString(); } }
        public HtmlAttributeCollection Attributes { get { return HtmlNode.Attributes; } }

        public List<VirtualElement> ChildElements = new List<VirtualElement>();

        public VirtualElement(HtmlNode htmlNode)
        {
            HtmlNode = htmlNode;
            if (htmlNode.HasChildNodes)
            {
                foreach (var child in htmlNode.ChildNodes)
                {
                    var childVElement = new VirtualElement(child);
                    childVElement.ParentId = Id;
                    ChildElements.Add(childVElement);
                }
            }
        }

        private void CreateVirtualElement(HtmlNode htmlNode)
        {
            //TagName = htmlNode.Name;
            //Attributes = htmlNode.Attributes;
            //Id = Attributes["id"] != null ? Attributes["id"].Value : "";
            //LocatorText = htmlNode.ToCssSelectorString();
            //if (!string.IsNullOrEmpty(LocatorText))
            //    Locator = By.CssSelector(LocatorText);
            //var text = htmlNode.SelectSingleNode("text()");
            //Text = text != null ? text.InnerText : "";
        }
    }
}
