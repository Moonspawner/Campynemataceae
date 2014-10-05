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


namespace DSLBrowser
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
            Reload();
        }


        public static void Reload()
        {
            skripttimer = true;
            skriptausfüren = true;
            LoadPlugins();
            Console.Title = "TK-Browser :: StartPage";
            NetzwerkFunktionen.Aufrufen();
            NetzwerkFunktionen.Downloader();
            Tags();
            Ausgabe();
            CoreClass.Reload();
        }

        public static void Ausgabe()
        {
            Console.WriteLine("");
            Console.Write("Drücken Sie eine Taste um zum Startmenü zurückzukehren.");
            Console.ReadKey();
            Console.Title = "Telekom Browser :: StartPage";
            Console.Clear();
        }


        public static string CodeInDatei;
        public static void Tags()
        {
            bool incode = false;
            bool inhead = false;
            bool incode2 = false;

            var tags = new List<string>();

            var code = new List<string>();
            string text = "";
            bool intitle = false;
            //bool readtitle = false;

            

            foreach (var zeile in FileSpace.Split('\n'))
            {
                if (plugins != null)
                {
                    foreach (var plugin in plugins)
                    {
                        plugin.ZeilenAbruf(zeile, ref text, incode || incode2, inhead, intitle);
                    }
                }
                if (zeile.Trim() == "<head>")
                {
                    inhead = true;
                }
                if (zeile.Trim() == "</head>")
                {
                    inhead = false;
                    continue;
                }
                if (inhead == true)
                {

                    if (zeile.Trim() == "<title>")
                    {
                        intitle = true;
                    }
                    if (zeile.Trim() == "</title>")
                    {
                        intitle = false;
                    }
                    else if (intitle == true)
                    {
                        Console.Title = zeile;
                    }
                }
                if(inhead==false)
                {
                    if (zeile.Trim() == "<text>")
                    {
                        continue;
                    }
                    if (zeile.Trim() == "</text>")
                    {
                        continue;
                    }

                    if (zeile.Trim() == "<beep>")
                    {

                    }
                    if (zeile.Trim() == "</beep>")
                    {

                    }

                    //Schinken
                    if (incode2 == true)
                    {
                        if (zeile != "<batch>" && zeile != "</batch>")
                        {
                            CodeInDatei = CodeInDatei + zeile + "\n";
                        }
                    }

                    if (zeile.Trim() == "<batch>")
                    {
                        CodeInDatei = "";
                        incode2 = true;
                        incode = true;
                    }
                    else if (zeile.Trim() == "</batch>")
                    {
                       CodeWriter();
                        //datei ausführen und den consolen-output hier hin ausgeben
                        incode2 = false;
                        incode = false;
                    }
                    else if (incode)
                    {
                        if (true) { ;}
                    }
                    else
                    {
                        text += zeile + "\n";
                    }
                }
            }

            FileSpace = text;
            tags = Regex.Split(FileSpace, "<|>").ToList();
            foreach (var tag in tags.Select(t => t.Trim()))
            {
                if (tag.StartsWith("color="))
                {
                    Color(tag);
                }
                else if (tag.StartsWith("/color"))
                {
                    Console.ResetColor();
                }
                else
                {
                    //Ausgabe
                    Console.WriteLine(tag);
                }
            }
            
        }
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
                Reload();
            }
        }
        public static bool skripttimer;
        public static bool skriptausfüren;
        public static void SkriptStarter()
        {
            if (skriptausfüren == true)
            {
                Console.WriteLine("    ╔═════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("    ║                           Skript Output                             ║");
                Console.WriteLine("    ╚═════════════════════════════════════════════════════════════════════╝");
                var process = new Process();
                var startinfo = new ProcessStartInfo("cmd.exe", @"/C skript.bat");
                startinfo.RedirectStandardOutput = true;
                startinfo.UseShellExecute = false;
                process.StartInfo = startinfo;
                process.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
                process.Start();
                process.BeginOutputReadLine();
                process.WaitForExit();
                Console.WriteLine("    ╔═════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("    ║                         Skript Output Ende                          ║");
                Console.WriteLine("    ╚═════════════════════════════════════════════════════════════════════╝");
                skriptausfüren = false;
            }
        }

        public static void Color(string tagcontent)
        {
            switch (tagcontent) //<Color=&2> TextReader bla bla </Color>
            {
                case "color=&0":
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case "color=&1":
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;
                case "color=&2":
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case "color=&3":
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                case "color=&4":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case "color=&5":
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                case "color=&6":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case "color=&7":
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case "color=&8":
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case "color=&9":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "color=&a":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "color=&b":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case "color=&c":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "color=&d":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case "color=&e":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "color=&f":
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "color=&r":
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
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
    }
}
