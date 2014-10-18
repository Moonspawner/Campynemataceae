using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace BrowserForSlowNetwork
{
    class Lesezeichen
    {
        public static Dictionary<ConsoleKey, string> dictionary = new Dictionary<ConsoleKey, string>();
        public static int zähler = 112;
        //public static StreamReader file1 = new StreamReader("bookmarks.json");
        public static bool beenden = false;
        public static string steph2 = "";
        public static void Menü()
        {
            Console.Clear();
            Console.Title = "TK Browser :: Lesezeichen";
            Console.WriteLine("    ╔══════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("    ║                            Lesezeichen                               ║");
            Console.WriteLine("    ╠══════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("    ║                                                                      ║");
            Console.WriteLine("    ║                                                                      ║");
            Console.WriteLine("    ║ -Neu                                                                 ║");
            Console.WriteLine("    ║  Erstelle ein Neues Lesezeichen                                      ║");
            Console.WriteLine("    ║                                                                      ║");
            Console.WriteLine("    ║ -Beenden                                                             ║");
            Console.WriteLine("    ║  Zurück zur Startseite                                               ║");
            Console.WriteLine("    ║                                                                      ║");
            Console.WriteLine("    ║                                                                      ║");
            Console.WriteLine("    ║                                                                      ║");
            Console.WriteLine("    ║                                                                      ║");
            Console.WriteLine("    ║                                                                      ║");
            Console.WriteLine("    ║                                                                      ║");
            Console.WriteLine("    ║                                                                      ║");
            Console.WriteLine("    ║                                                                      ║");
            Console.WriteLine("    ║                                                                      ║");
            Console.WriteLine("    ║                                                                      ║");
            Console.WriteLine("    ║                                                                      ║");
            Console.WriteLine("    ╠══════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("    ║Beenden, Neu                                                          ║");
            Console.WriteLine("    ╚══════════════════════════════════════════════════════════════════════╝");
            Console.Write    ("    >>");
            var key2 = Console.ReadKey();
           // dictionary = JsonConvert.DeserializeObject<Dictionary<ConsoleKey, string>>(file1.ReadToEnd());
            switch (key2.Key)
            {
                case ConsoleKey.F1:
                    steph2 = dictionary[ConsoleKey.F1];
                    CoreClass.Eingabe = steph2;
                    CoreNetzwerk.Aufrufen();
                    break;
                case ConsoleKey.F2:
                    steph2 = dictionary[ConsoleKey.F2];
                    CoreClass.Eingabe = steph2;
                    CoreNetzwerk.Aufrufen();
                    break;
                case ConsoleKey.F3:
                    steph2 = dictionary[ConsoleKey.F3];
                                        CoreClass.Eingabe = steph2;
                    CoreNetzwerk.Aufrufen();
                    break;
                case ConsoleKey.F4:
                    steph2 = dictionary[ConsoleKey.F4];
                                        CoreClass.Eingabe = steph2;
                    CoreNetzwerk.Aufrufen();
                    break;
                case ConsoleKey.F5:
                    steph2 = dictionary[ConsoleKey.F5];
                                        CoreClass.Eingabe = steph2;
                    CoreNetzwerk.Aufrufen();
                    break;
                case ConsoleKey.F6:
                    steph2 = dictionary[ConsoleKey.F6];
                                        CoreClass.Eingabe = steph2;
                    CoreNetzwerk.Aufrufen();
                    break;
                case ConsoleKey.F7:
                    steph2 = dictionary[ConsoleKey.F7];
                                        CoreClass.Eingabe = steph2;
                    CoreNetzwerk.Aufrufen();
                    break;
                case ConsoleKey.F8:
                    steph2 = dictionary[ConsoleKey.F8];
                                        CoreClass.Eingabe = steph2;
                    CoreNetzwerk.Aufrufen();
                    break;
                case ConsoleKey.F9:
                    steph2 = dictionary[ConsoleKey.F9];
                                        CoreClass.Eingabe = steph2;
                    CoreNetzwerk.Aufrufen();
                    break;
                case ConsoleKey.F10:
                    steph2 = dictionary[ConsoleKey.F10];
                                        CoreClass.Eingabe = steph2;
                    CoreNetzwerk.Aufrufen();
                    break;
                case ConsoleKey.F11:
                    steph2 = dictionary[ConsoleKey.F11];
                                        CoreClass.Eingabe = steph2;
                    CoreNetzwerk.Aufrufen();
                    break;
                case ConsoleKey.F12:
                    steph2 = dictionary[ConsoleKey.F12];
                                        CoreClass.Eingabe = steph2;
                    CoreNetzwerk.Aufrufen();
                    break;
                default:

                    break;
            }
            var line = new string(key2.KeyChar, 1) + Console.ReadLine();
            switch (line)
            {
                case "beenden":
                    if (beenden == true)
                    {
                        Beenden();
                    }
                    else
                    {
                        BeendenNix();
                    }
                    break;
                case "neu":
                    if (beenden == false)
                    {
                        beenden = true;
                    }
                    Neu();
                    break;
                default:
                    Menü();
                    break;

            }
        }
        public static void Neu()
        {
            Console.Clear();
            Console.WriteLine("Drücken Sie einer der F Tasten.");
            ConsoleKey key = Console.ReadKey().Key;
            Console.WriteLine("");
            if (File.Exists("bookmarks.json"))
            {
               // dictionary = JsonConvert.DeserializeObject<Dictionary<ConsoleKey, string>>(file1.ReadToEnd());
            }
            else
            {
                while (zähler <= 123)
                {
                    ConsoleKey zählerkey = (ConsoleKey)zähler;
                    dictionary.Add(zählerkey, "");
                    zähler++;
                }
                string jsonnew = JsonConvert.SerializeObject(dictionary, Formatting.Indented);
                File.WriteAllText("bookmarks.json", jsonnew);
                //file1.Close();
            }
            //StreamReader sr = new StreamReader("bookmarks.json");
            //var testen = sr.ReadToEnd();
            if (key == ConsoleKey.F1)
            {
                Console.WriteLine("Ihr Tastendruck: " + key);
                /*if (testen.Contains("F1"))
                {
                    Console.Clear();
                    Console.WriteLine("Die Taste " + key + " wurde schon belegt. Möchten sie die Taste neu setzen?");
                    Console.WriteLine("J = Ja; N = Nein");
                    var prüfen = Console.ReadLine();
                    switch (prüfen)
                    {
                        case "j":
                            break;
                        case "n":
                            Neu();
                            break;
                    }
                }*/
                Console.WriteLine("Geben Sie jetzt den Shortcut an:");
                dictionary[ConsoleKey.F1] = Console.ReadLine();
            }
            else if (key == ConsoleKey.F2)
            {
                Console.WriteLine("Ihr Tastendruck: " + key);
                Console.WriteLine("Geben Sie jetzt den Shortcut an:");
                dictionary[ConsoleKey.F2] = Console.ReadLine();
            }
            else if (key == ConsoleKey.F3)
            {
                Console.WriteLine("Ihr Tastendruck: " + key);
                Console.WriteLine("Geben Sie jetzt den Shortcut an:");
                dictionary[ConsoleKey.F3] = Console.ReadLine();
            }
            else if (key == ConsoleKey.F4)
            {
                Console.WriteLine("Ihr Tastendruck: " + key);
                Console.WriteLine("Geben Sie jetzt den Shortcut an:");
                dictionary[ConsoleKey.F4] = Console.ReadLine();
            }
            else if (key == ConsoleKey.F5)
            {
                Console.WriteLine("Ihr Tastendruck: " + key);
                Console.WriteLine("Geben Sie jetzt den Shortcut an:");
                dictionary[ConsoleKey.F5] = Console.ReadLine();
            }
            else if (key == ConsoleKey.F6)
            {
                Console.WriteLine("Ihr Tastendruck: " + key);
                Console.WriteLine("Geben Sie jetzt den Shortcut an:");
                dictionary[ConsoleKey.F6] = Console.ReadLine();
            }
            else if (key == ConsoleKey.F7)
            {
                Console.WriteLine("Ihr Tastendruck: " + key);
                Console.WriteLine("Geben Sie jetzt den Shortcut an:");
                dictionary[ConsoleKey.F7] = Console.ReadLine();
            }
            else if (key == ConsoleKey.F8)
            {
                Console.WriteLine("Ihr Tastendruck: " + key);
                Console.WriteLine("Geben Sie jetzt den Shortcut an:");
                dictionary[ConsoleKey.F8] = Console.ReadLine();
            }
            else if (key == ConsoleKey.F9)
            {
                Console.WriteLine("Ihr Tastendruck: " + key);
                Console.WriteLine("Geben Sie jetzt den Shortcut an:");
                dictionary[ConsoleKey.F9] = Console.ReadLine();
            }
            else if (key == ConsoleKey.F10)
            {
                Console.WriteLine("Ihr Tastendruck: " + key);
                Console.WriteLine("Geben Sie jetzt den Shortcut an:");
                dictionary[ConsoleKey.F10] = Console.ReadLine();
            }
            else if (key == ConsoleKey.F11)
            {
                Console.WriteLine("Ihr Tastendruck: " + key);
                Console.WriteLine("Geben Sie jetzt den Shortcut an:");
                dictionary[ConsoleKey.F11] = Console.ReadLine();
            }
            else if (key == ConsoleKey.F12)
            {
                Console.WriteLine("Ihr Tastendruck: " + key);
                Console.WriteLine("Geben Sie jetzt den Shortcut an:");
                dictionary[ConsoleKey.F12] = Console.ReadLine();
            }
            else
            {
                Console.Clear();
                Console.Title = "TK Browser :: Fehler!";
                Console.WriteLine("Sie haben keine der F-Tasten gedrückt! Sie werden in 5 Sekunden wieder zum Lesezeichenmenü weitergeleitet...");
                System.Threading.Thread.Sleep(5000);
                Menü();
            }
            Console.WriteLine("Möchten Sie eine weitere Taste setzen? (J = Ja; N = Nein)");
            switch (Console.ReadLine())
            {
                case "N":
                    Menü();
                    break;
                case "Nein":
                    Menü();
                    break;
                case "J":
                    Neu();
                    break;
                case "Ja":
                    Neu();
                    break;
                default:
                    Menü();
                    break;
            }
        }
        public static void Beenden()
        {
            string json = JsonConvert.SerializeObject(dictionary, Formatting.Indented);
            if (File.Exists("bookmarks.json"))
            {
                if (dictionary[ConsoleKey.F1] == "")
                {
                    dictionary[ConsoleKey.F1] = "";
                }
                if (dictionary[ConsoleKey.F2] == "")
                {
                    dictionary[ConsoleKey.F2] = "";
                }
                if (dictionary[ConsoleKey.F3] == "")
                {
                    dictionary[ConsoleKey.F3] = "";
                }
                if (dictionary[ConsoleKey.F4] == "")
                {
                    dictionary[ConsoleKey.F4] = "";
                }
                if (dictionary[ConsoleKey.F5] == "")
                {
                    dictionary[ConsoleKey.F5] = "";
                }
                if (dictionary[ConsoleKey.F6] == "")
                {
                    dictionary[ConsoleKey.F6] = "";
                }
                if (dictionary[ConsoleKey.F7] == "")
                {
                    dictionary[ConsoleKey.F7] = "";
                }
                if (dictionary[ConsoleKey.F8] == "")
                {
                    dictionary[ConsoleKey.F8] = "";
                }
                if (dictionary[ConsoleKey.F9] == "")
                {
                    dictionary[ConsoleKey.F9] = "";
                }
                if (dictionary[ConsoleKey.F10] == "")
                {
                    dictionary[ConsoleKey.F10] = "";
                }
                if (dictionary[ConsoleKey.F11] == "")
                {
                    dictionary[ConsoleKey.F11] = "";
                }
                if (dictionary[ConsoleKey.F12] == "")
                {
                    dictionary[ConsoleKey.F12] = "";
                }
                if (dictionary[ConsoleKey.F1] == "")
                {
                    dictionary[ConsoleKey.F12] = "";
                }
                //file1.Close();
                File.WriteAllText("bookmarks.json", json);
            }
            else
            {
                File.WriteAllText("bookmarks.json", json);
            }
            CoreTick.StartCore(false);
        }
        public static void BeendenNix()
        {
            CoreTick.StartCore(false);
        }
        public static void LOL()
        {
            if (steph2 == "")
            {
                CoreClass.Startscreen();
            }
            else
            {
                return;
            }
        }
    }
}