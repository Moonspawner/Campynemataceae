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

        public static string GetSite(string uri)
        {
			return GetFileFromNameServer(uri);
        }

        static string GetFileFromNameServer(string address)
        {
            var serveralias = address.Split('/')[0];
            var filename = address.Substring(Math.Min(serveralias.Length + 1, address.Length));
            filename = Path.Combine(filename, "index.tk").Replace('\\', '/');

            var server = new System.Net.WebClient().DownloadString("http://tk.steph.cf/dns.php?name=" + serveralias);
            if (!server.Trim().StartsWith("http://")) { server = "http://" + server; }
            server = server.Replace ("\n", ""); server = server.Replace("\r", "");
            return new System.Net.WebClient().DownloadString(Path.Combine(server, filename).Replace('\\', '/'));
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