using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TkParser
{
    static class TupleReplace
    {
        public static string Replace(this string subject, params Tuple<string, string>[] replacements)
        {
            foreach (var replacement in replacements) { subject = subject.Replace(replacement.Item1, replacement.Item2); }
            return subject;
        }
    }
}
