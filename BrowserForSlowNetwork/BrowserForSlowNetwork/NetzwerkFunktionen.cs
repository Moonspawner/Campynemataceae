using System;
using System.Media;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using BrowserForSlowNetwork;

namespace DSLBrowser
{
    static class NetzwerkFunktionen
    {
        public static string URL = "";
        private static string punkte2 = "..";


            public static string NameServerIP = "http://alexmitter.tk/tknameserver/standart.tkn";
            public static string NameServerFileSpace;
            public static string NameServerBekommenURI;
        public static void NameServerConnect()
        {
            if (NameServerBekommenURI != "")
            {
                NameServerBekommenURI = "";
            }
            Console.WriteLine("Versuche zu Nameserver zu verbinden...");
            NameServerFileSpace = new System.Net.WebClient().DownloadString(NameServerIP);
            Console.WriteLine(NameServerFileSpace);
            Console.ReadKey();





        }

        public static void Aufrufen()
        {
            Console.Clear();
            Console.Title = "Telekom Browser :: StartPage";
            Console.WriteLine("    ╔═════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("    ║Der Telekom Browser - Surfen Sie auch mit einer langsamen Verbindung.║");
            Console.WriteLine("    ║                                                                     ║");
            Console.WriteLine("    ║Geben Sie bitte die Datei ein, die Sie öffnen möchten.               ║");
            Console.WriteLine("    ║Mit 'Lesezeichen' öffnen sie die Lesezeichen-Menü.                   ║");
            Console.WriteLine("    ╚═════════════════════════════════════════════════════════════════════╝");
            Console.Write("    >> ");
            CoreClass.Eingabe = Console.ReadLine();
            //Prüft ob die Datei mit einem / endet, wenn ja leitet er auf die index.txt weiter
            switch(CoreClass.Eingabe){
                case"about:us":
                    About.ÜberUns();
                    break;
                default:
                    break;
                
            }
            
            
            if (CoreClass.Eingabe == "")
            {
                CoreClass.Eingabe = "index";
            }
            if (CoreClass.Eingabe == "about:us")
            {

                About.ÜberUns();
                Aufrufen();
            }
            else if (CoreClass.Eingabe == "Lesezeichen" | CoreClass.Eingabe == "lesezeichen")
            {
                Lesezeichen.Menü();
            }
            else
            {
                Parallel.Invoke(new Action[]{()=>{
                                                     Ping Sender = new Ping();
                                                     PingReply Result = Sender.Send("alexmitter.tk");
                                                     if (Result.Status == IPStatus.Success)
                                                     {
                                                         URL = "http://alexmitter.tk/tkbrowser/" + CoreClass.Eingabe + ".tk";
                                                     }
                                                     else
                                                     {
                                                         Ping Sender2 = new Ping();
                                                         PingReply Result2 = Sender.Send("188.193.113.201");
                                                         if (Result2.Status == IPStatus.Success)
                                                         {
                                                             URL = "http://188.193.113.201/tkbrowser/" + CoreClass.Eingabe + ".tk";
                                                         }
                                                         else 
                                                         {
                                                             Console.WriteLine("");
                                                             Console.WriteLine("Der Client kann sich nicht mit dem Server verbinden. Prüfen sie ihre Internetverbindung und versuchen sie es später nochmal.");
                                                             CoreClass.zähler = 1;
                                                             CoreClass.tester2 = 1;
                                                             Console.ReadLine();
                                                             Console.Clear();
                                                             Aufrufen();
                                                         }
                                                     }
                                                     CoreClass.zähler = 1;
                                                     punkte2 = "..";
                },()=>{
                          CoreClass.zähler = 0;
                          System.Threading.Thread.Sleep(500);
                          Console.Clear();
                          Console.Title = "Telekom Browser :: Anpingen...";
                          while(CoreClass.zähler == 0)
                          {
                              punkte2 += ".";
                              Console.WriteLine("Adresse wird angepingt"+punkte2);
                              System.Threading.Thread.Sleep(1000);
                              if (CoreClass.zähler == 0) {
                                  Console.Clear();
                              }
                          }
                          punkte2 = "..";
                          Console.Title = "";
                }});
            }
        }

        public static void Downloader()
        {
            SoundPlayer player = new SoundPlayer("modem.wav");
            Parallel.Invoke(new Action[]{()=>{
                                                 try
                                                 {
                                                     CoreClass.FileSpace = new System.Net.WebClient().DownloadString(URL);
                                                     Console.Clear();
                                                     Console.Title = "";
                                                     Console.ForegroundColor = ConsoleColor.Green;
                                                     Console.WriteLine(">> Datei: "+ CoreClass.Eingabe);                //Was soll das darstellen?(nachricht von Alexmitter)
                                                     Console.ForegroundColor = ConsoleColor.Gray;
                                                     Console.WriteLine("");
                                                 }
                                                 catch
                                                 {
                                                     if(CoreClass.tester2 == 0)
                                                     {
                                                         Console.Clear();
                                                         Console.Title = "Telekom Browser :: Fehler";
                                                         Console.WriteLine("Die Datei konnte nicht gefunden werden. Bitte überprüfen sie ihre Eingabe.");
                                                         player.Stop();
                                                         CoreClass.zähler = 1;
                                                         CoreClass.tester = 1;
                                                         Console.ReadLine();
                                                         Console.Clear();
                                                         Aufrufen();
                                                     }
                                                 }
                                                 CoreClass.zähler = 1;
            },()=>{
                      CoreClass.zähler = 0;
                      System.Threading.Thread.Sleep(500);
                      if (CoreClass.tester == 0)
                      {
                          Console.Clear();
                      }
                      player.Play();
                      Console.Title = "Telekom Browser :: Verbinden...";
                      while(CoreClass.zähler == 0)
                      {
                          punkte2 += ".";
                          Console.WriteLine("Verbindung wird aufgebaut"+punkte2);
                          System.Threading.Thread.Sleep(1000);
                          if (CoreClass.zähler == 0) 
                          {
                              Console.Clear();
                          }
                      }
                      player.Stop();
                      punkte2 = "..";
                      Console.Title = "";
            }});
        }
    }
}