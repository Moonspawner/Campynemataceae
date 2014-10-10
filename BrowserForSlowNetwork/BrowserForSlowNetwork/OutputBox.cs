using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using BrowserForSlowNetwork;
using System.Reflection;
using System.ComponentModel;
using System.Media;
using System.Diagnostics;

namespace BrowserForSlowNetwork
{
    class OutputBox
    {
        public static string NachrichtOutput;
        public static void Nachricht(string Nachricht2)
        {
            int Länge = Nachricht2.Length;
            string Oben;
            string Mitte;
            string Unten;
            Länge++;
            Länge++;

            Oben =  "    ╔";
            Unten = "    ╚";

            while(Länge > 0)
            {
                Oben = Oben + "═";
                Unten = Unten + "═";
                Länge--;
            }
            Oben = Oben +   "═╗";
            Unten = Unten + "═╝";
            Mitte = "    ║" + " " + Nachricht2 + " " + "║";

            NachrichtOutput = Oben + "\n" + Mitte + "\n" + Unten;
        }

        public static void Ausgabe()
        {
            Console.WriteLine(NachrichtOutput);
        }
    }
}
