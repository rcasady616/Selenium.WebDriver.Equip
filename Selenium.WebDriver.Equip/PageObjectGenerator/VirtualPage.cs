using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace Selenium.WebDriver.Equip.PageObjectGenerator
{
    public class VirtualPage
    {
        public HtmlDocument HtmlDoc;
        public List<VirtualElement> ChildElements = new List<VirtualElement>();
        public string Name { get; set; }

        public VirtualPage(string htmlString)
        {
            HtmlDoc = new HtmlDocument();
            HtmlDoc.LoadHtml(htmlString);
            foreach (var node in HtmlDoc.DocumentNode.ChildNodes)
                ChildElements.Add(new VirtualElement(node));
        }

        public HtmlNodeCollection GetAllElements()
        {
            var nodes = HtmlDoc.DocumentNode.SelectNodes("./html/*");
            return nodes;
        }

        public IEnumerable<VirtualElement> Descendants()
        {
            var nodes = new Stack<VirtualElement>(ChildElements);
            while (nodes.Any())
            {
                var node = nodes.Pop();
                yield return node;
                foreach (var n in node.ChildElements) nodes.Push(n);
            }
        }

        public IEnumerable<VirtualElement> GetLinks()
        {
            return this.Descendants().Where(element => element.TagName == "a");
        }
    }
}
