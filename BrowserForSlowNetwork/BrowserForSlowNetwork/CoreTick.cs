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
    class CoreTick
    {
        static string FehlerCode;

        public static void StartCore()
        {
            Tick();
        }

        public static void RestartCore()
        {
            Console.Clear();
            Tick();
        }

        static void Tick()
        {
            try
            {
                CoreClass.Init();
            }
            catch(Exception ex1)
            {
                FehlerCode = "Erzeugen der Program Variablen";
                TickfehlerAusgabe();
            }

            try
            {
                CoreNetzwerk.Aufrufen();
            }
            catch (Exception ex2)
            {
                FehlerCode = "Starten wichtiger Browser Komponeten";
                TickfehlerAusgabe();
            }

            try
            {
                CoreNetzwerk.Downloader();
            }
            catch (Exception ex3)
            {
                FehlerCode = "Aufrufen des Netzwerkcodes";
                TickfehlerAusgabe();
            }

            try
            {
                CoreClass.Tags();
            }
            catch (Exception ex4)
            {
                FehlerCode = "Lesen des Seitencodes";
                TickfehlerAusgabe();
            }

            try
            {
                CoreClass.Ausgabe();
            }
            catch (Exception ex5)
            {
                FehlerCode = "Ausgabe der Seitenkomponenten";
                TickfehlerAusgabe();
            }
            
            RestartCore();
        }

        static void TickfehlerAusgabe()
        {
            switch (FehlerCode)
            {
                case "Erzeugen der Program Variablen":
                    Console.Clear();
                    Console.WriteLine("    ╔═════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("    ║                  Schwerer fehler im Browser Tick                    ║");
                    Console.WriteLine("    ╚═════════════════════════════════════════════════════════════════════╝");
                    Console.WriteLine("Fehler: ", FehlerCode);
                    Console.ReadKey();

                    break;
                case "Starten wichtiger Browser Komponeten":
                    Console.Clear();
                    Console.WriteLine("    ╔═════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("    ║                  Schwerer fehler im Browser Tick                    ║");
                    Console.WriteLine("    ╚═════════════════════════════════════════════════════════════════════╝");
                    Console.WriteLine("Fehler: ", FehlerCode);
                    Console.ReadKey();

                    break;
                case "Aufrufen des Netzwerkcodes":
                    Console.Clear();
                    Console.WriteLine("    ╔═════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("    ║                  Schwerer fehler im Browser Tick                    ║");
                    Console.WriteLine("    ╚═════════════════════════════════════════════════════════════════════╝");
                    Console.WriteLine("Fehler: ", FehlerCode);

                    Console.ReadKey();

                    break;
                case "Lesen des Seitencodes":
                    Console.Clear();
                    Console.WriteLine("    ╔═════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("    ║                  Schwerer fehler im Browser Tick                    ║");
                    Console.WriteLine("    ╚═════════════════════════════════════════════════════════════════════╝");
                    Console.WriteLine("Fehler: ", FehlerCode);
                    Console.ReadKey();

                    break;
                case "Ausgabe der Seitenkomponenten":
                    Console.Clear();
                    Console.WriteLine("    ╔═════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("    ║                  Schwerer fehler im Browser Tick                    ║");
                    Console.WriteLine("    ╚═════════════════════════════════════════════════════════════════════╝");
                    Console.WriteLine("Fehler: ", FehlerCode);
                    Console.ReadKey();
                    break;
                default:
                    RestartCore();
                    break;

            }
        }
    }
}
