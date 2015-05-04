using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using TreeCollection;
using TkParser;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TkClient
{
    public partial class Client
    {
        public string GenerateHTML()
        {
            Parse(_identifier);
            return GenerateHTML(elements);
        }

        public TreeNode<Element> GenerateTreeNode()
        {
            Parse(_identifier);
            return elements;
        }

        private string GenerateHTML(TreeNode<Element> elems)
        {
            if (elems == null || elems.Count == 0) { return string.Empty; }

            var html = new StringBuilder();

            foreach (var element in elems)
            { 
                if(element.Value.Type != Element.ElementType.Title) { element.Value.Value = element.Value.Value.Replace("\n", "<br />"); }
                if (element.Value.Type == Element.ElementType.Title) {
                    this.Title = element.Value.Value;
                } else if (element.Value.Type == Element.ElementType.Box) {
                    html.AppendLine(File.ReadAllText("html/box.html").Replace(new Tuple<string, string>("$content", element.Value.Value + GenerateHTML(element))));
                } else if (element.Value.Type == Element.ElementType.Text || element.Value.Type == Element.ElementType.Color) {
                    if (!(element.Value.ContentObj is string) || (string)element.Value.ContentObj != string.Empty) {
                        var color = ((Color?)element.Value.ContentObj ?? Color.Black);
                        var hexColor = "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
                        html.AppendLine(File.ReadAllText("html/textblock.html").Replace(new Tuple<string, string>("$content", element.Value.Value.Replace("\n", "<br />") + GenerateHTML(element)), new Tuple<string, string>("$color", hexColor)));
                    } else {
                        html.AppendLine(element.Value.Value + GenerateHTML(element));
                    }
                } else if (element.Value.Type == Element.ElementType.Link) {
                    html.AppendLine(File.ReadAllText("html/link.html").Replace(new Tuple<string, string>("$content", (string)element.Value.Value + GenerateHTML(element)), new Tuple<string, string>("$destination", (string)element.Value.ContentObj)));
                } else if (element.Value.Type == Element.ElementType.Root) {
                    html.AppendLine(element.Value.Value + GenerateHTML(element));
                } else if (element.Value.Type == Element.ElementType.Literal) {
                    html.AppendLine(element.Value.Value + GenerateHTML(element));
                } else {
                    html.AppendLine(element.Value.Value + GenerateHTML(element));
                }
        }

            Console.Write(html);
            return html.ToString();
        }
    }
}
