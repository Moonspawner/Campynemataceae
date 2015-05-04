using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TkParser
{
    public class Element
    {
        public enum ElementType
        {
            Root,
            Text,
            Head,
            Title,
            Box,
            Beep,
            Color,
            Link,
            Literal
        }

        public object ContentObj { get; set; }
        public string Value { get; set; }
        public ElementType Type { get; set; }
        public Dictionary<string, object> Style { get; set; }
        public bool Closed { get; set; }
        public Element(object content = null, ElementType type = ElementType.Text, string stringcontent = "", Dictionary<string, object> style = null)
        {
            ContentObj = content;
            Type = type;
            Value = stringcontent;
            Style = style;
            if (Style == null) { Style = new Dictionary<string, object>(); }
            Closed = false;
        }
    }
}
