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
    static class CoreClass
    {
        public static string FileSpace = "";
        public static string Ausgabeninhalt = "";
        public static string Eingabe = "";
        static List<IPlugin> plugins;
        public static int zähler = 0;
        public static int tester = 0; 
        public static int tester2 = 0;

        static public string ParsePath(string path) //function by dr4yyee
        {
            var newPath = new StringBuilder();
            var folders = path.Split(Path.DirectorySeparatorChar);
            foreach (var folder in folders) newPath.Append((Regex.IsMatch(folder, "%.+%")) ? Environment.GetEnvironmentVariable(Regex.Match(folder, "(?:%)(.+)(?:%)").Groups[1].Value) : folder).Append((folders[folders.Length - 1] == folder) ? string.Empty : new string(Path.DirectorySeparatorChar, 1));
            return newPath.ToString();
        }

        [STAThread]
        public static void Main(string[] args)
        {
            //Console.SetBufferSize(80,25);
            Routine.StartRoutine(true);
        }

        public static void Startscreen()
        {
            Console.Clear();
            System.Threading.Thread.Sleep(50);
            Console.Title = "TK-Browser :: StartPage";

            Console.WriteLine("    ╔══════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("    ║   Der TK-Browser - Surfen Sie auch mit einer langsamen Verbindung    ║");
            Console.WriteLine("    ╠══════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("    ║                                                                      ║");
            Console.WriteLine("    ║                                                                      ║");
            Console.WriteLine("    ║                                                                      ║");
            Console.WriteLine("    ╠══════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("    ║Lesezeichen                                                           ║");
            Console.WriteLine("    ╠══════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("    ║Geben Sie bitte die Datei ein, die Sie öffnen möchten.                ║");
            Console.WriteLine("    ║Mit 'Lesezeichen' öffnen sie die Lesezeichen-Menü.                    ║");
            Console.WriteLine("    ║                                                                      ║");
            Console.WriteLine("    ║                                                                      ║");
            Console.WriteLine("    ║                                                                      ║");
            Console.WriteLine("    ╠══════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("    ║                                                                      ║");
            Console.WriteLine("    ║                                                                      ║");
            Console.WriteLine("    ║                                                                      ║");
            Console.WriteLine("    ║                                                                      ║");
            Console.WriteLine("    ║                                                                      ║");
            Console.WriteLine("    ║                                                                      ║");
            Console.WriteLine("    ║Bitte URI Eingeben                                                    ║");
            Console.WriteLine("    ╚══════════════════════════════════════════════════════════════════════╝");
            //CoreTick.Manuell("startseite");

            Console.Write("    >> ");
            CoreClass.Eingabe = Console.ReadLine();
			if (CoreClass.Eingabe == "lesezeichen") {
				Lesezeichen.startLesezeichen ();
			}
        }


        public static void Init()
        {
            
            //LoadPlugins();
            Console.Title = "TK-Browser :: StartPage";
        }

        public static void Ausgabe()
        {
            Console.WriteLine(" ╔══════Neue Seite════════════════════════════════════════════════════════════╗");
            Console.WriteLine(" ║Drücken Sie eine Taste um zur Startseite zurückzukehren.                    ║");
            Console.WriteLine(" ╚════════════════════════════════════════════════════════════════════════════╝");
            Console.ReadKey();
            Console.Title = "Telekom Browser :: StartPage";
            Console.Clear();
        }

        

        public static void LoadPlugins() 
         { 
             var pluginPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)); 
            var info = new DirectoryInfo(pluginPath); 
             if (!info.Exists) { Directory.CreateDirectory(pluginPath); }


             
                plugins = 
                   info.GetFiles("*.dll").Select(file => Assembly.LoadFile(file.FullName)).SelectMany( 
                                      currentAssembly => (currentAssembly.GetTypes().Where(
                                                 type => type.GetInterfaces().Contains(typeof(IPlugin))).Select( 
                                                 type => (IPlugin)Activator.CreateInstance(type)))).ToList(); 
          }



        public static string Sicherheistschranke { get; set; }

        public static int beep1 { get; set; }
    }
}
