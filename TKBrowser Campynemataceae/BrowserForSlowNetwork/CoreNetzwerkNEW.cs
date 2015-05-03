using System;
using System.Media;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BrowserForSlowNetwork;
using System.Net;
using System.Text.RegularExpressions;

namespace BrowserForSlowNetwork
{
    internal static class CoreNetzwerk_
    {
        private static string[] _nameServer = { "http://tk.steph.cf/dns.php?name={0}", "http://jfds.eu/tkbrowser/dns.php?name={0}" };

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
            var server = "";
            foreach (var nameServer in _nameServer) {
                server = new WebClient().DownloadString(string.Format(nameServer, serveralias)).Trim(' ', '\n', '\r');
                if (server != "") { break; }
            }
            
            if (!server.StartsWith("http://")) { server = "http://" + server; }
            var resolvedAddress = Path.Combine(server, filename).Replace('\\', '/');

            HttpWebResponse siteRequest;
            var siteContent = "";
            foreach (var appendage in new[] {"", ".tk", "/index.tk"}) {
                try {
                    siteRequest = ((HttpWebResponse)WebRequest.Create((resolvedAddress + appendage).Replace('\\', '/')).GetResponse());
                    siteContent = new StreamReader(siteRequest.GetResponseStream()).ReadToEnd();
                    if (!Regex.IsMatch(siteContent, "Apache.*at.*Port", RegexOptions.Compiled | RegexOptions.Singleline)) {
                        break;
                    }
                } catch (Exception e) {
                    Debug.WriteLine(e);
                }
            }

            return siteContent;
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