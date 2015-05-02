using System;
using System.Media;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using BrowserForSlowNetwork;
using System.Net;

namespace BrowserForSlowNetwork
{
    static class CoreNetzwerk_
    {
        public static string URL_ = "";
        private static string Space = "";

        public static string GetSite(string URI)
        {
            GetNameserver(URI);
            GetSite();
            return Space;
        }

        static string adress;
        static string[] adress_alone;
        static string GetNameserver(string URI)
        {
			/* 

			Networkcode by PlaySteph310, with Magic by Alexmitter

			*/
            adress = URI;
			// remove http://
			if (adress.Contains("http://") == true)
			{
				adress.Replace("http://", "");
				// senseless code by Alexmitter > Console.WriteLine("Hello World");
			}
			// split link to single strings
			adress_alone = adress.Split ('/');
			// check domain
            string nameserver = "";
            new System.Net.WebClient().DownloadString("http://tk.steph.cf/dns.php?name=" + adress_alone[0]);
            if (nameserver != "")
            {
                for (int i = adress.Length; i > 1; i--)
                {
                    adress = adress + "/" + adress_alone[i];
                }
                Space = new System.Net.WebClient().DownloadString(URL_);
            }
            return Space;
        }

        static void GetSite()
        {
            ;
        }

        public static void GetSiteManuall(string name, string URL)
        {
            string URI = URL + "/" + name + ".tk";
            CoreClass.FileSpace = new System.Net.WebClient().DownloadString(URL);
        }

        static void Visualisation(string Meldung)
        {
            switch(Meldung)
            {
                case "nameserver_error":
                    Console.Clear();
                            Console.WriteLine("    ╔════════════════════════════════════════════════════════════════════════╗");
                            Console.WriteLine("    ║         Der Client kann sich nicht mit dem Server verbinden            ║");
                            Console.WriteLine("    ║ Prüfen sie ihre Internetverbindung und versuchen sie es später nochmal ║");
                            Console.WriteLine("    ╚════════════════════════════════════════════════════════════════════════╝");
                    break;
                case "site_error":
                        Console.Clear();
                        Console.Title = "Telekom Browser :: Fehler";
                        Console.WriteLine("");
                        Console.WriteLine("    ╔═════════════════════════════════════════════════════════════════════╗");
                        Console.WriteLine("    ║                Die Datei konnte nicht gefunden werden               ║");
                        Console.WriteLine("    ║                  Bitte überprüfen sie ihre Eingabe                  ║");
                        Console.WriteLine("    ╚═════════════════════════════════════════════════════════════════════╝");
                    break;


            }
        }

    }
}