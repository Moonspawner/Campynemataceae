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

namespace Engine
{
    class Funktionen
    {
        
        public static string Box_Zeichnen(string box)
        {
            box.TrimStart().TrimEnd();
            int Länge = box.Length;
            string Oben;
            string Mitte;
            string Unten;
            Länge++;
            Länge++;

            Oben = "╔";
            Unten = "╚";

            if (Länge > 78)
            {
                Console.WriteLine(Engine.Funktionen.Box_Zeichnen("Zu Großer inhhalt für <box>. Maximum: 77 Zeichen"));
                Console.ReadKey();
                Routine.RestartRoutine();
            }

            while (Länge > 0)
            {
                Oben = Oben + "═";
                Unten = Unten + "═";
                Länge--;
            }
            Oben = Oben + "═╗";
            Unten = Unten + "═╝";
            Mitte = "║" + " " + box + " " + " ║";

            string NachrichtOutput = Oben + "\n" + Mitte + "\n" + Unten;
            return NachrichtOutput;
	        
		 
	        
        }

        public static void Über()
        {
            Console.Clear();
            Console.Title = "TK-Browser :: Über Uns";
            Console.WriteLine("TK-Browser / Browser for Slow Network");
            Console.WriteLine("");
            Console.WriteLine("Entwickelt von Moonspawner");
            Console.WriteLine("Alexmitter - GitHub: http://github.com/Alexmitter");
            Console.WriteLine("PlaySteph310 - GitHub: http://github.com/PlaySteph310");
            Console.ReadLine();
            Console.Clear();
            Console.Title = "TK Browser :: StartPage";
        }
    }
}
