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
            CoreTick.StartCore(true);
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
            Console.Write("    >> ");
            CoreClass.Eingabe = Console.ReadLine();
        }


        public static void Init()
        {
            skripttimer = true;
            skriptausfüren = true;
            //LoadPlugins();
            Console.Title = "TK-Browser :: StartPage";
        }

        public static void Ausgabe()
        {
            Console.WriteLine("");
            Console.Write("Drücken Sie eine Taste um zum Startmenü zurückzukehren.");
            Console.ReadKey();
            Console.Title = "Telekom Browser :: StartPage";
            Console.Clear();
        }

        public static bool inbatch;
        public static bool tellnoskript;
        public static string CodeInDatei;

        public static string WirklichAusfüren;
        public static string Sicherheitsschranke;
        public static bool Sicherheistschranke2;
        public static void CodeWriter()
        {
            Console.Clear();

            if (skripttimer == true)
            {
                skripttimer = false;
                if (CodeInDatei != "")
                {
                    Console.WriteLine("\n\n\n\n\n\n");
                    Console.WriteLine("    ╔═════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("    ║                              VORSICHT                               ║");
                    Console.WriteLine("    ║                                                                     ║");
                    Console.WriteLine("    ║    Diese Seite versuchtein Skript auf ihrem Computer auszufüren     ║");
                    Console.WriteLine("    ║       Das kann Schwere Schäden auf ihrem Computer verursachen       ║");
                    Console.WriteLine("    ║                                                                     ║");
                    Console.WriteLine("    ║           j = JA       n = Nein     b = Skriptcode Zeigen           ║");
                    Console.WriteLine("    ╚═════════════════════════════════════════════════════════════════════╝");
                    Console.Write("                                     >");
                    Sicherheitsschranke = Console.ReadLine();
                    Console.Clear();


                    if (Sicherheitsschranke == "b")
                    {
                        Console.Clear();
                        Console.WriteLine("    ╔═════════════════════════════════════════════════════════════════════╗");
                        Console.WriteLine("    ║                          Skriptcode = Batch                         ║");
                        Console.WriteLine("    ╚═════════════════════════════════════════════════════════════════════╝\n");
                        Console.WriteLine(CodeInDatei);
                        Console.ReadKey();
                        Console.WriteLine("Wollen sie diesen Code Wirklich ausfüren?\nj = JA n = NEIN");
                        WirklichAusfüren = Console.ReadLine();
                        if (WirklichAusfüren != "j")
                        {
                            return;
                        }
                        CoreClass.CodeWriter();
                         
                    }
                    skriptausfüren = true;
                    if (Sicherheitsschranke != "j")
                    {
                        return;
                    }
                    Sicherheistschranke2 = true;
                }
                if (CodeInDatei != "\n")
                {
                    if (Sicherheistschranke2 != true)
                    {
                        Console.WriteLine("\n\n\n\n\n\n");
                        Console.WriteLine("    ╔═════════════════════════════════════════════════════════════════════╗");
                        Console.WriteLine("    ║                              VORSICHT                               ║");
                        Console.WriteLine("    ║                                                                     ║");
                        Console.WriteLine("    ║    Diese Seite versuchtein Skript auf ihrem Computer auszufüren     ║");
                        Console.WriteLine("    ║       Das kann Schwere Schäden auf ihrem Computer verursachen       ║");
                        Console.WriteLine("    ║                                                                     ║");
                        Console.WriteLine("    ║           j = JA       n = Nein     b = Skriptcode Zeigen           ║");
                        Console.WriteLine("    ╚═════════════════════════════════════════════════════════════════════╝");
                        Sicherheitsschranke = Console.ReadLine();

                        if (Sicherheitsschranke == "b")
                        {
                            Console.Clear();
                            Console.WriteLine("    ╔═════════════════════════════════════════════════════════════════════╗");
                            Console.WriteLine("    ║                          Skriptcode = Batch                         ║");
                            Console.WriteLine("    ╚═════════════════════════════════════════════════════════════════════╝\n");
                            Console.WriteLine(CodeInDatei);
                            Console.ReadKey();
                            Console.WriteLine("Wollen sie diesen Code Wirklich ausfüren?\nj = JA n = NEIN");
                            WirklichAusfüren = Console.ReadLine();
                            if (WirklichAusfüren != "j")
                            {
                                return;
                            }
                            CoreClass.CodeWriter();
                        }

                        skriptausfüren = true;
                        if (Sicherheitsschranke != "j")
                        {
                            return;
                        }
                    }
                }
            }
            Sicherheistschranke2 = false;
            Console.Clear();
            try
            {
                if (File.Exists("skript.bat"))
                {
                    File.Delete("skript.bat");
                }

                CodeInDatei = CodeInDatei.Replace("\n", "\r\n");
                using (StreamWriter writer = new StreamWriter("skript.bat"))
                {
                    writer.Write(CodeInDatei);
                }

                //new StreamWriter("skript.bat").Write(CodeInDatei);
                    SkriptStarter();
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Fehler beim Schreiben der Skript Datei, bitte Kontaktieren sie den Administrator");
                Console.ReadKey();
                CoreTick.RestartCore();

            }
        }
        public static bool skripttimer;
        public static bool skriptausfüren;
        public static void SkriptStarter()
        {
            if (skriptausfüren == true)
            {
                if (tellnoskript != true)
                {
                    Console.WriteLine("    ╔═════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("    ║                           Skript Output                             ║");
                    Console.WriteLine("    ╚═════════════════════════════════════════════════════════════════════╝");
                }
                var process = new Process();
                var startinfo = new ProcessStartInfo("cmd.exe", @"/C skript.bat");
                startinfo.RedirectStandardOutput = true;
                startinfo.UseShellExecute = false;
                process.StartInfo = startinfo;
                process.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
                process.Start();
                process.BeginOutputReadLine();
                process.WaitForExit();
                if (tellnoskript != true)
                {
                    Console.WriteLine("    ╔═════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("    ║                         Skript Output Ende                          ║");
                    Console.WriteLine("    ╚═════════════════════════════════════════════════════════════════════╝");
                }
                skriptausfüren = false;
            }
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
