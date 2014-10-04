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
        //Nach Brocken
        public static Dictionary<ConsoleKey, string> dictionary = new Dictionary<ConsoleKey, string>();
        public static void Menü()
        {
            Console.Clear();
            Console.Title = "TK Browser :: Lesezeichen";
            Console.WriteLine("Sie sind im Lesezeichenmenü.");
            Console.WriteLine("Was möchten Sie machen?");
            Console.WriteLine("");
            Console.WriteLine("Beenden, Neu");
            var prüfen = Console.ReadLine();
            switch(prüfen)
            {
                case "beenden":
                    Beenden();
                    break;
                case "neu":
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
            StreamReader sr = new StreamReader("bookmarks.json");
            var testen = sr.ReadToEnd();
            if (key == ConsoleKey.F1)
            {
                Console.WriteLine("Ihr Tastendruck: " + key);
                if (testen.Contains("F1"))
                {
                    Console.Clear();
                    Console.WriteLine("Die Taste "+key+" wurde schon belegt. Möchten sie die Taste neu setzen?");
                    Console.WriteLine("J = Ja; N = Nein");
                    var prüfen = Console.ReadLine();
                    switch(prüfen)
                    {
                        case "j":
                            break;
                        case "n":
                            Neu();
                            break;
                    }
                }
                Console.WriteLine("Geben Sie jetzt den Shortcut an:");
                dictionary.Add(ConsoleKey.F1, Console.ReadLine());
            }
            else if (key == ConsoleKey.F2)
            {
                Console.WriteLine("Ihr Tastendruck: " + key);
                Console.WriteLine("Geben Sie jetzt den Shortcut an:");
                dictionary.Add(ConsoleKey.F2, Console.ReadLine());
            }
            else if (key == ConsoleKey.F3)
            {
                Console.WriteLine("Ihr Tastendruck: " + key);
                Console.WriteLine("Geben Sie jetzt den Shortcut an:");
                dictionary.Add(ConsoleKey.F3, Console.ReadLine());
            }
            else if (key == ConsoleKey.F4)
            {
                Console.WriteLine("Ihr Tastendruck: " + key);
                Console.WriteLine("Geben Sie jetzt den Shortcut an:");
                dictionary.Add(ConsoleKey.F4, Console.ReadLine());
            }
            else if (key == ConsoleKey.F5)
            {
                Console.WriteLine("Ihr Tastendruck: " + key);
                Console.WriteLine("Geben Sie jetzt den Shortcut an:");
                dictionary.Add(ConsoleKey.F5, Console.ReadLine());
            }
            else if (key == ConsoleKey.F6)
            {
                Console.WriteLine("Ihr Tastendruck: " + key);
                Console.WriteLine("Geben Sie jetzt den Shortcut an:");
                dictionary.Add(ConsoleKey.F6, Console.ReadLine());
            }
            else if (key == ConsoleKey.F7)
            {
                Console.WriteLine("Ihr Tastendruck: " + key);
                Console.WriteLine("Geben Sie jetzt den Shortcut an:");
                dictionary.Add(ConsoleKey.F7, Console.ReadLine());
            }
            else if (key == ConsoleKey.F8)
            {
                Console.WriteLine("Ihr Tastendruck: " + key);
                Console.WriteLine("Geben Sie jetzt den Shortcut an:");
                dictionary.Add(ConsoleKey.F8, Console.ReadLine());
            }
            else if (key == ConsoleKey.F9)
            {
                Console.WriteLine("Ihr Tastendruck: " + key);
                Console.WriteLine("Geben Sie jetzt den Shortcut an:");
                dictionary.Add(ConsoleKey.F9, Console.ReadLine());
            }
            else if (key == ConsoleKey.F10)
            {
                Console.WriteLine("Ihr Tastendruck: " + key);
                Console.WriteLine("Geben Sie jetzt den Shortcut an:");
                dictionary.Add(ConsoleKey.F10, Console.ReadLine());
            }
            else if (key == ConsoleKey.F11)
            {
                Console.WriteLine("Ihr Tastendruck: " + key);
                Console.WriteLine("Geben Sie jetzt den Shortcut an:");
                dictionary.Add(ConsoleKey.F11, Console.ReadLine());
            }
            else if (key == ConsoleKey.F12)
            {
                Console.WriteLine("Ihr Tastendruck: " + key);
                Console.WriteLine("Geben Sie jetzt den Shortcut an:");
                dictionary.Add(ConsoleKey.F12, Console.ReadLine());
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
            var prüfen2 = Console.ReadLine();
            switch(prüfen2)
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
            File.WriteAllText("lesezeichen.json", json);
        }
    }
}
