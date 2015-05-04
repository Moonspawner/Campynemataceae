using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Runtime.InteropServices;
using TkParser;
using TreeCollection;
using Newtonsoft.Json;

namespace TkClient
{
    public partial class Client
    {
        private string _identifier;

        Color NeutralColor = Color.Black; //may depend on the system style

        TreeNode<Element> elements;
        public string Title { get; private set; }
        public Client(string url)
        {
            _identifier = url;
        }
        private void Parse(string url)
        {
            //_url = url;
            //Title = String.Empty;
            //var beepdata = String.Empty;
            //var boxdata = String.Empty;
            //elements = new List<Element>();

            //Color ForegroundColor = NeutralColor;

            elements = ParseFragment(GetStreamForFile(url).ReadToEnd()); //fetches the URL from various sources
        }

        private TreeNode<Element> ParseFragment(string code)
        {
            var s = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(code)));
            var tree = new TreeNode<Element>() { new Element(null, Element.ElementType.Root) };

            var @break = false;

            while (!s.EndOfStream && !@break)
            {
                var @char = (char)s.Read(); //consume the char only once

                switch (@char)
                {
                    case '<': //since the input is not even remotely valid xml I can't rely on a xml parser
                        var tag = "";
                        ConsumeUntil(ref tag, ref s, '>');
                        tag = tag.Remove(tag.Length - 1);
                        switch (tag) //trim off the last character
                        {
                            case "head":
                                tree.EnumerateAllNodes().Last(t => !((Element)t.Value).Closed && t.Value.Type != Element.ElementType.Literal).Add(new Element("", Element.ElementType.Head));
                                break;
                            case "/head":
                                tree.EnumerateAll().Last(t => t.Type == Element.ElementType.Head && !t.Closed).Closed = true;
                                ConsumeWhiteSpace(ref s);
                                break;

                            case "title":
                                tree.EnumerateAllNodes().Last(t => !((Element)t.Value).Closed && t.Value.Type != Element.ElementType.Literal).Add(new Element("", Element.ElementType.Title));
                                break;
                            case "/title":
                                tree.EnumerateAll().Last(t => t.Type == Element.ElementType.Title && !t.Closed).Closed = true;
                                ConsumeWhiteSpace(ref s);
                                break;

                            case "beep":
                                tree.EnumerateAllNodes().Last(t => !((Element)t.Value).Closed && t.Value.Type != Element.ElementType.Literal).Add(new Element("", Element.ElementType.Beep));
                                break;
                            case "/beep":

                                for (var matchResults = new Regex(@"((?<freq>[0-9]+)\s+(?<dur>[0-9]+))+").Match((string)tree.EnumerateAll().Last(t => t.Type == Element.ElementType.Beep).ContentObj); matchResults.Success; matchResults = matchResults.NextMatch()) {
                                    NativeMethods.Beep(int.Parse(matchResults.Groups["freq"].Value), int.Parse(matchResults.Groups["dur"].Value));
                                }
                                tree.EnumerateAll().Last(t => t.Type == Element.ElementType.Beep && !t.Closed).Closed = true;
                                ConsumeWhiteSpace(ref s);
                                break;

                            case "box":
                                tree.EnumerateAllNodes().Last(t => !((Element)t.Value).Closed && t.Value.Type != Element.ElementType.Literal).Add(new Element("", Element.ElementType.Box));
                                break;
                            case "/box":
                                //CreateTextBlock(ref sb, ForegroundColor);

                                ////if (tagStack.First().Type == ParserState.Box) { tagStack.Pop(); }
                                //boxdata = Regex.Replace(sb.ToString(), @"(^\s+)|(\s+$)", "");
                                //sb = new StringBuilder();
                                //CreateBox(boxdata, ForegroundColor);
                                tree.EnumerateAll().Last(t => t.Type == Element.ElementType.Box && !t.Closed).Closed = true;
                                ConsumeWhiteSpace(ref s);
                                break;

                            case "text":
                                tree.EnumerateAllNodes().Last(t => !((Element)t.Value).Closed && t.Value.Type != Element.ElementType.Literal).Add(new Element("", Element.ElementType.Text));
                                break;
                            case "/text":
                                tree.EnumerateAll().Last(t => t.Type == Element.ElementType.Text && !t.Closed).Closed = true;
                                ConsumeWhiteSpace(ref s);
                                break;

                            case "/color":
                                //CreateTextBlock(ref sb, ForegroundColor);
                                //if (tree.First() != null && tree.First().Style != null && tree.First().Style.ContainsKey("color"))
                                //{
                                //    ForegroundColor = Color.FromName(tree.Last().Style["color"].ToString());
                                //    tree.Remove(tree.Last());
                                //}
                                //else
                                //{
                                //    ForegroundColor = NeutralColor;
                                //}
                                tree.EnumerateAll().Last(t => t.Type == Element.ElementType.Color && !t.Closed).Closed = true;
                                ConsumeWhiteSpace(ref s);
                                break;

                            case "/link":
                                tree.EnumerateAll().Last(t => t.Type == Element.ElementType.Link && !t.Closed).Closed = true;
                                ConsumeWhiteSpace(ref s);
                                break;

                            case "br":
                                tree.EnumerateAllNodes().Last(t => !((Element) t.Value).Closed).Value.Value += "\n\r";
                                break;

                            default:
                                if (tag.StartsWith("color"))
                                {
                                    tree.EnumerateAllNodes().Last(t => !((Element)t.Value).Closed && t.Value.Type != Element.ElementType.Literal).Add(new Element( GetColor(Regex.Match(tag, "(?:color=)(.+)").Groups[1].Value, NeutralColor), Element.ElementType.Color));

                                    //var t = new TextElement(TkParser.Type.Open, tree.Last().Style);
                                    //t.Style["color"] = ForegroundColor.CloneJson();
                                    //tree.Add(t.CloneJson());
                                } else if (tag.StartsWith("link")) {
                                    tree.EnumerateAllNodes().Last(t => !((Element)t.Value).Closed && t.Value.Type != Element.ElementType.Literal).Add(new Element(Regex.Match(tag, "(?:link=\"?)([^\"]+)(?:\"?)").Groups[1].Value, Element.ElementType.Link));
                                } else if (Regex.IsMatch(tag, "!-- *ignorerest *--")) {
                                    @break = true;
                                } else { 
                                    Console.WriteLine("ERROR: tag name '{0}' not recognized", tag);
                                }
                                break;
                        }
                        tree.EnumerateAllNodes().Last(t => !((Element)t.Value).Closed).Add(new Element("", Element.ElementType.Literal));

                        break;

                    case '&':
                        //StringBuilder sb = new StringBuilder();

                        //if (!Regex.IsMatch(new string((char)s.Peek(), 1), "[\\da-fA-F0-9&rR]")) { //if the next character is just something ordinary, no '&' nor 0-9a-fA-FrR,
                        //    goto default; //just print it
                        //} else if ((char)s.Peek() == '&') { //only when the next Character is '&',
                        //    s.Read(); //consume it
                        //    if (Regex.IsMatch(new string((char)s.Peek(), 1), "[\\da-fA-F0-9&rR]")) { // and whether the next character is 0-9a-fA-FrR (=we are in a color change)
                        //        tree.EnumerateAll().Last(t => !t.Closed).Value += "&"; ; //if it is, write that single '&' which was consumed but isn't part of the color definiton to the StringBuilder
                        //        sb.Append("&" + (char)s.Read());
                        //    } else {
                        //        tree.EnumerateAll().Last(t => !t.Closed).Value += "&&"; // otherwise, there were just to '&' in a row
                        //    }
                        //} else {
                        //    sb.Append("&" + (char)s.Read());
                        //}

                        var isColor = false;
                        var sb = new StringBuilder("&");
                        while ((char)s.Peek() == '&') { sb.Append((char)s.Read()); }
                        if (Regex.IsMatch(new string((char)s.Peek(), 1), "[\\da-fA-F0-9&rR]")) { sb.Remove(0, 1); isColor = true; }
                        tree.EnumerateAll().Last(t => !t.Closed).Value += sb.ToString();
                        if (isColor) { tree.EnumerateAllNodes().Last(t => !((Element)t.Value).Closed).Add(new Element(GetColor("&" + (char)s.Read(), NeutralColor), Element.ElementType.Color)); }

                        break;

                    default:
                        try {
                            tree.EnumerateAll().Last(t => !t.Closed).Value += @char;
                        } catch (Exception ex) {
                            Console.WriteLine(ex.Message);
                        }
                        //if (tagStack.Select(state => state.Type).Contains(ParserState.Box))
                        //{
                        //    boxdata += @char;
                        //}
                        //else if (tagStack.First().Type == ParserState.Text || tagStack.First().Type == ParserState.Link || tagStack.First().Type == ParserState.Root)
                            Console.Write(@char);
                        break;
                }
            }

            return tree;
        }

        private static string WordWrap(string text, int width)
        {
            var lines = text.Split('\n').ToList();
            for (int line = 0; line < lines.Count; line++)
            {
                if (lines[line].Length >= width)
                {
                    var first = lines[line].Substring(0, width);
                    var rest = lines[line].Substring(width);

                    lines[line] = first;
                    lines.Insert(line + 1, rest);
                }
            }

            return String.Join("\n", lines);
        }

        private static void ConsumeWhiteSpace(ref StreamReader s)
        {
            while (" \t\r\n".Contains((char)s.Peek())) { s.Read(); }
        }

        private void ConsumeUntil(ref string inbetween, ref StreamReader s, char endChar)
        {
            ConsumeUntil(ref inbetween, ref s, new[] { endChar });
        }

        private void ConsumeUntil(ref string tag, ref StreamReader s, char[] endChar)
        {
            for (; !endChar.Contains(tag == String.Empty ? '\b' : tag.Last()); tag += (char)s.Read()) { }
        }

        private Color GetColor(string color, Color? NeutralColor = null)
        {
            NeutralColor = NeutralColor ?? Color.Black;
            switch (((color.Length > 0 && color[0] == '&') ? color.Substring(1) : color).ToLowerInvariant()) //stripe a possible '&' at the beginning of color
            {
                case "black":
                case "0":
                    return Color.Black;
                case "darkblue":
                case "1":
                    return Color.DarkBlue;
                case "darkgreen":
                case "2":
                    return Color.DarkGreen;
                case "darkcyan":
                case "3":
                    return Color.DarkCyan;
                case "darkred":
                case "4":
                    return Color.DarkRed;
                case "darkmagenta":
                case "5":
                    return Color.DarkMagenta;
                case "darkyellow":
                case "greenyellow":
                case "6":
                    return Color.GreenYellow; //actually ConsoleColor.DarkYellow
                case "gray":
                case "grey": //britfags <3
                case "7":
                    return Color.Gray;
                case "darkgray":
                case "darkgrey":
                case "8":
                    return Color.DarkGray;
                case "blue":
                case "9":
                    return Color.Blue;
                case "green":
                case "a":
                    return Color.Green;
                case "cyan":
                case "b":
                    return Color.Cyan;
                case "red":
                case "c":
                    return Color.Red;
                case "magenta":
                case "d":
                    return Color.Magenta;
                case "yellow":
                case "e":
                    return Color.Yellow;
                case "white":
                case "f":
                    return Color.White;

                case "default":
                case "r":
                    return NeutralColor ?? Color.Black;

                default:
                        if(new Regex(@"\A(?:(?:#)(([a-fA-F0-9]){6}|([a-fA-F0-9]){3}))\Z").IsMatch(color)){
                            return System.Drawing.ColorTranslator.FromHtml(color); //swm.ColorConverter.ConvertFromString(color); for RGBA
                        }
                        throw new ArgumentOutOfRangeException(String.Format("undefined color identifier '{0}'", color));
            }
        }
    }
}
