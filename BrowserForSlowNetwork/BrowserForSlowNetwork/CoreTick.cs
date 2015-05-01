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
        static bool Gforce;

        public static void StartCore(bool force)
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
            if (Gforce == true)
            {
                CoreClass.Init();
                CoreNetzwerk.Aufrufen();
                CoreNetzwerk.Downloader();
                Engine.Parser();
                CoreClass.Ausgabe();
                RestartCore();
                
            }
            else
            {
                try
                {
                CoreClass.Init();
                }
                catch(Exception ex1)
                {
                   FehlerSwitch = 1;
                   FehlerCode = ex1.ToString();
                   TickfehlerAusgabe();
                }

                try
                {
                    CoreNetzwerk.Aufrufen();
                }
                    catch (Exception ex2)
                {
                     FehlerSwitch = 2;
                FehlerCode = ex2.ToString();
                TickfehlerAusgabe();
                }

                try
                {
                    CoreNetzwerk.Downloader();
                }
                catch (Exception ex3)
                {
                    FehlerSwitch = 3;
                    FehlerCode = ex3.ToString();
                    TickfehlerAusgabe();
                }

                try
                {
                    Engine.Parser();
                }
                catch (Exception ex4)
                {
                    FehlerSwitch = 4;
                    FehlerCode = ex4.ToString();
                    TickfehlerAusgabe();
                }

                try
                {
                    CoreClass.Ausgabe();
                }
                catch (Exception ex5)
                {
                    FehlerSwitch = 5;
                    FehlerCode = ex5.ToString();
                    TickfehlerAusgabe();
                }
            }
            
            RestartCore();
        }
        static int FehlerSwitch;
        static void TickfehlerAusgabe()
        {
            switch (FehlerSwitch)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("    ╔═════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("    ║                  Schwerer fehler im Browser Tick                    ║");
                    Console.WriteLine("    ╚═════════════════════════════════════════════════════════════════════╝");
                    Console.WriteLine("Fehler: ", FehlerCode);
                    Console.ReadKey();

                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("    ╔═════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("    ║                  Schwerer fehler im Browser Tick                    ║");
                    Console.WriteLine("    ╚═════════════════════════════════════════════════════════════════════╝");
                    Console.WriteLine("Fehler: ", FehlerCode);
                    Console.ReadKey();

                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("    ╔═════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("    ║                  Schwerer fehler im Browser Tick                    ║");
                    Console.WriteLine("    ╚═════════════════════════════════════════════════════════════════════╝");
                    Console.WriteLine("Fehler: ", FehlerCode);

                    Console.ReadKey();

                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("    ╔═════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("    ║                  Schwerer fehler im Browser Tick                    ║");
                    Console.WriteLine("    ╚═════════════════════════════════════════════════════════════════════╝");
                    Console.WriteLine("Fehler: ", FehlerCode);
                    Console.ReadKey();

                    break;
                case 5:
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
