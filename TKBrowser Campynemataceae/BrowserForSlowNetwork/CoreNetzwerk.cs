
using System;
using System.Media;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using BrowserForSlowNetwork;

namespace BrowserForSlowNetwork
{
    static class CoreNetzwerk
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
            try
            {
                try
                {
                    NameServerFileSpace = new System.Net.WebClient().DownloadString("http://192.168.0.106/tknameserver/standart.tkn");

                }
                catch
                {
                    NameServerFileSpace = new System.Net.WebClient().DownloadString(NameServerIP);
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("    ╔═════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("    ║                 Keine Verbindung zum Nameserver                     ║");
                Console.WriteLine("    ║                                                                     ║");
                Console.WriteLine("    ║                           Wiederholen?                              ║");
                Console.WriteLine("    ╚═════════════════════════════════════════════════════════════════════╝");
                Console.ReadKey();
                Console.Clear();
                Routine.RestartRoutine();
            }

                Console.WriteLine(NameServerFileSpace);
            Console.ReadKey();
        }

        public static void Aufrufen()
        {
            Lesezeichen.LOL();
            //Prüft ob die Datei mit einem / endet, wenn ja leitet er auf die index.txt weiter

            StandartPages.EingangsSwitchString = CoreClass.Eingabe + "";
            StandartPages.EingangsSwitch();

            if (CoreClass.Eingabe == "")
            {
                CoreClass.Eingabe = "index";
            }
            else if (CoreClass.Eingabe == "Lesezeichen" | CoreClass.Eingabe == "lesezeichen")
            {
                Lesezeichen.Menü();
            }
            else
            {
                Parallel.Invoke(new Action[]{()=>{
                                                     if(!File.Exists(Path.Combine("sites", CoreClass.Eingabe) + ".tk")) {
                                                         Ping Sender = new Ping();
                                                         PingReply Result = Sender.Send("192.168.0.106");
                                                         if (Result.Status == IPStatus.Success)
                                                         {
                                                             URL = "http://192.168.0.106/tkbrowser/" + CoreClass.Eingabe + ".tk";
                                                         }
                                                         else
        {

        }
                                                         {
                                                             Ping Sender2 = new Ping();
                                                             PingReply Result2 = Sender.Send("alexmitter.tk");
                                                             if (Result2.Status == IPStatus.Success)
                                                             {
                                                                 URL = "http://alexmitter.tk/tkbrowser/" + CoreClass.Eingabe + ".tk";
                                                             }
                                                             else
                                                             {
                                                                 Console.WriteLine("    ╔════════════════════════════════════════════════════════════════════════╗");
                                                                 Console.WriteLine("    ║         Der Client kann sich nicht mit dem Server verbinden            ║");
                                                                 Console.WriteLine("    ║ Prüfen sie ihre Internetverbindung und versuchen sie es später nochmal ║");
                                                                 Console.WriteLine("    ╚════════════════════════════════════════════════════════════════════════╝");
                                                                 CoreClass.zähler = 1;
                                                                 CoreClass.tester2 = 1;
                                                                 Console.ReadLine();
                                                                 Console.Clear();
                                                                 Aufrufen();
                                                             }
                                                         }}
                                                         CoreClass.zähler = 1;
                                                         punkte2 = "..";
                },()=>{
                          CoreClass.zähler = 0;
                          System.Threading.Thread.Sleep(500);
                          Console.Clear();
                          Console.Title = "TK Browser :: Anpingen...";
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
                                                     var localfilename = Path.Combine("sites",  CoreClass.Eingabe) + ".tk";
                                                     if(File.Exists(localfilename)) {
                                                        CoreClass.FileSpace = File.ReadAllText(localfilename);
                                                     }else {
                                                        CoreClass.FileSpace = new System.Net.WebClient().DownloadString(URL);
                                                     }                                   
                                                     Console.Clear();
                                                     Console.Title = "";
                                                     Console.ForegroundColor = ConsoleColor.Green;
                                                     Engine.Funktionen.Box_Zeichnen(">> Datei: " + CoreClass.Eingabe);
                                                     Console.ForegroundColor = ConsoleColor.Gray;
                                                     Console.WriteLine("");
                                                 }
                                                 catch
                                                 {
                                                     if(CoreClass.tester2 == 0)
                                                     {
                                                         Console.Clear();
                                                         Console.Title = "TKBrowser :: Fehler";
                                                         Console.WriteLine("");
                                                         Console.WriteLine("    ╔═════════════════════════════════════════════════════════════════════╗");
                                                         Console.WriteLine("    ║                Die Datei konnte nicht gefunden werden               ║");
                                                         Console.WriteLine("    ║                  Bitte überprüfen sie ihre Eingabe                  ║");
                                                         Console.WriteLine("    ╚═════════════════════════════════════════════════════════════════════╝");
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
                      Console.Title = "TKBrowser :: Verbinden...";
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