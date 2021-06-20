using HtmlAgilityPack;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            }
            return loc;

        }

        private string ToAlphaNumeric(string str)
        {
            return Regex.Replace(str, "[^A-Za-z0-9 _]", "").Replace(" ", "");
        }

        public string CapitalizeFirstLetter(string s)
        {
            if (String.IsNullOrEmpty(s))
                return s;
            if (s.Length == 1)
                return s.ToUpper();
            return s.Remove(1).ToUpper() + s.Substring(1);
        }

        public VirtualLocator ToVirtualLocatorString()
        {
            var vLocator = new VirtualLocator();
            var id = GetAttribute("id");
            if (!string.IsNullOrEmpty(id))
            {
                vLocator.Name = ToAlphaNumeric($"{id}");
                vLocator.LocatorType = LocatorType.Id;
                vLocator.LocatorText = id;
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
                            vLocator.Name = ToAlphaNumeric(text);//.Replace(" ", "");
                            vLocator.LocatorType = LocatorType.LinkText;
                            vLocator.LocatorText = text;
                        }
                    }
                    if (string.IsNullOrEmpty(vLocator.Name))
                    {
                        var href = GetAttribute("href");
                        if (!string.IsNullOrEmpty(href))
                        {
                            var name1 = ToAlphaNumeric(Regex.Match(href, @".*\/([^/]*)$").Groups[1].Value.ToString());
                            var uri = new Uri(href);
                            var name2 = ToAlphaNumeric(uri.Host).Replace("www","");
                            vLocator.Name = $"{name1}_{name2}";
                            vLocator.LocatorType = LocatorType.Css;
                            vLocator.LocatorText = $"a[href='{href}']";
                        }
                    }
                    if (string.IsNullOrEmpty(vLocator.Name))
                        return vLocator;
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
                case "input":
                case "Input":
                    if (!string.IsNullOrEmpty(vLocator.Name))
                    {
                        var inputType = CapitalizeFirstLetter(GetAttribute("type"));
                        vLocator.Name = $"{vLocator.Name}{inputType}";
                    }
                    break;
                default:
                    vLocator.Name = null;
                    break;
            }
            return vLocator;
        }
    }
}
