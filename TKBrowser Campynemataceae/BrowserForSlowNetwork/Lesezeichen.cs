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
        public static Dictionary<ConsoleKey, string> bookmarks = new Dictionary<ConsoleKey, string>();
        public static int zähler = 112;
        public static bool beenden = false;
        public static string steph2 = "";
		public static void startLesezeichen()
		{
			// Prüft ob Datei existiert
			if (File.Exists("bookmarks.json"))
			{
				// Konvertiert Jsondatei in ein dictionary
				StreamReader file1 = new StreamReader("bookmarks.json");
				bookmarks = JsonConvert.DeserializeObject<Dictionary<ConsoleKey, string>>(file1.ReadToEnd());
				file1.Close();
			}
			else
			{
				// Zählt alle F-Tasten und fügt sie dem Dictionary hinzu
				for(int i = 112; i <= 123; i++)
				{
					ConsoleKey key = (ConsoleKey)i;
					bookmarks.Add (key, "");
				}
				// Konvertiert das Dictionary in eine JSON-File und speichert
				string json = JsonConvert.SerializeObject(bookmarks, Formatting.Indented);
				File.WriteAllText("bookmarks.json", json);
			}
			Menü ();
		}
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
            Console.WriteLine("    ╠══════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("    ║Beenden, Neu                                                          ║");
            Console.WriteLine("    ╚══════════════════════════════════════════════════════════════════════╝");
            Console.Write    ("    >>");
            // Überprüft erste Taste, ob es eine F-Taste ist
			var firstkey = Console.ReadKey();
			//ConsoleKey checkkey = firstkey;
			if (((int)firstkey.Key) >= 112 && ((int)firstkey.Key) <= 123)
			{
				CoreClass.FileSpace = CoreNetzwerk_.GetSite(bookmarks[firstkey.Key]);
				Engine.Parsing.Parser(CoreClass.FileSpace);
				CoreClass.Ausgabe();
				CoreClass.Startscreen ();
			}
			//Wenn keine F-taste dann soll er weiter schreiben können
			var line = new string(firstkey.KeyChar, 1) + Console.ReadLine();
            switch (line)
            {
			case "beenden":
				Beenden ();
				break;
			case "neu":
				Neu ();
                break;
            default:
                Menü();
                break;

            }
        }
        public static void Neu()
        {
            Console.Clear();
            Console.WriteLine("Drücken Sie eine F-Tasten.");
            ConsoleKey key = Console.ReadKey().Key;
			if (((int)key) <= 112 && ((int)key) >= 123)
			{
				return;
			}
            Console.WriteLine("");
			Console.WriteLine("Ihr Tastendruck: " + key);
			Console.WriteLine("Geben Sie jetzt den Shortcut an:");
			bookmarks[key] = Console.ReadLine();
            Console.WriteLine("Möchten Sie eine weitere Taste setzen? (J = Ja; N = Nein)");
			switch (Console.ReadLine())
			{
			case "N":
			case "Nein":
				Menü();
				break;
			case "J":
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
			string json = JsonConvert.SerializeObject(bookmarks, Formatting.Indented);
			File.WriteAllText("bookmarks.json", json);
            // Routine.StartRoutine(false);
        }
        public static void BeendenNix()
        {
            // Routine.StartRoutine(false);
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