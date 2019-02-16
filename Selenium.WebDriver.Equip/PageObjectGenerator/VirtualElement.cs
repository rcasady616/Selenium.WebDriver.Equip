using HtmlAgilityPack;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Selenium.WebDriver.Equip.PageObjectGenerator
{
    public class VirtualElement
    {
        public HtmlNode HtmlNode;
        public string ParentId { set; get; }
        public bool Visable { set; get; }
        public string Id { get { return GetAttribute("id"); } }
        public string TagName { get { return HtmlNode.Name; } }
        public string Text
        {
            get
            {
                var text = HtmlNode.SelectSingleNode("text()");
                return text != null ? text.InnerText : "";
            }
        }
        public string Name { get { return VLocator.Name; } }
        public By Locator { get { return GetLocator(); } }
        public string LocatorText { get { return VLocator.LocatorText; } }
        public string LocatorTypeMethod { get; set; }
        public VirtualLocator VLocator { get; set; }

        // Attributes
        public HtmlAttributeCollection Attributes { get { return HtmlNode.Attributes; } }
        public string Href { get { return GetAttribute("href"); } }

        public List<VirtualElement> ChildElements = new List<VirtualElement>();

        public VirtualElement(HtmlNode htmlNode)
        {
            HtmlNode = htmlNode;
            VLocator = ToVirtualLocatorString();
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

        private string GetAttribute(string name)
        {
            return Attributes[name] != null ? Attributes[name].Value : "";
        }

        public By GetLocator()
        {
            By loc;
            switch (VLocator.LocatorType)
            {
                case LocatorType.Id:
                    LocatorTypeMethod = "Id";
                    loc = By.Id(LocatorText);
                    break;
                case LocatorType.Css:
                    LocatorTypeMethod = "CssSelector";
                    loc = By.CssSelector(LocatorText);
                    break;
                case LocatorType.LinkText:
                    LocatorTypeMethod = "LinkText";
                    loc = By.LinkText(LocatorText);
                    break;
                case LocatorType.XPath:
                    LocatorTypeMethod = "XPath";
                    loc = By.XPath(LocatorText);
                    break;
                default:
                    throw new NotImplementedException("Locator type not currently handdeled");
                    break;
            }
            return loc;

        }

        public VirtualLocator ToVirtualLocatorString()
        {
            var vLocator = new VirtualLocator();
            var id = GetAttribute("id");
            if (!string.IsNullOrEmpty(id))
            {
                vLocator.Name = $"{id}";
                vLocator.LocatorType = LocatorType.Id;
                vLocator.LocatorText = id;
                return vLocator;
            }
            switch (HtmlNode.Name)
            {
                case "a":
                case "A":
                    if (string.IsNullOrEmpty(vLocator.Name))
                    {
                        var text = "";
                        var textNode = HtmlNode.SelectSingleNode("text()");
                        if (textNode != null)
                            text = textNode.InnerText;
                        if (!string.IsNullOrEmpty(text))
                        {
                            vLocator.Name = text.Replace(" ", "");
                            vLocator.LocatorType = LocatorType.LinkText;
                            vLocator.LocatorText = text;
                        }
                    }
                    if (string.IsNullOrEmpty(vLocator.Name))
                    {
                        var href = GetAttribute("href");
                        if (!string.IsNullOrEmpty(href))
                        {
                            vLocator.Name = Regex.Match(href, @".*\/([^/]*)$").Groups[1].Value.ToString();
                            vLocator.LocatorType = LocatorType.Css;
                            vLocator.LocatorText = $"a[href='{href}']";
                        }
                    }
                    vLocator.Name = $"{vLocator.Name}Link";
                    break;
                case "label":
                case "Label":
                    if(string.IsNullOrEmpty(vLocator.Name))
                    {
                        var labelText = "";
                        var labelTextNode = HtmlNode.SelectSingleNode("text()");
                        if (labelTextNode != null)
                            labelText = labelTextNode.InnerText;
                        if (!string.IsNullOrEmpty(labelText))
                        {
                            vLocator.Name = labelText.Replace(" ", "");
                            vLocator.LocatorType = LocatorType.Css;
                            vLocator.LocatorText = $"//label[text() = '{labelText}']";
                        }
                    }
                    vLocator.Name = $"{vLocator.Name}Label";
                    break;
                default:
                    vLocator.Name = null;
                    break;
            }
            return vLocator;
        }
    }
}
