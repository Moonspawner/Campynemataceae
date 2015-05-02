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
    static class CoreNetzwerk
    {
        public static string URL_ = "";
        private static string punkte2_ = "..";


        public static string NameServerIP_ = "http://alexmitter.tk/tknameserver/standart.tkn";
        public static string NameServerFileSpace_;
        public static string NameServerBekommenURI_;
        public static void Aufrufen_()
        {
			/* 

			Networkcode by PlaySteph310

			*/
			string adress = CoreClass.Eingabe;
			string nameserver = "";
			// remove http://
			if (adress.Contains("http://") == true)
			{
				adress.Replace("http://", "");
				// senseless code by Alexmitter > Console.WriteLine("Hello World");
			}
			// split link to single strings
			string[] adress_alone = adress.Split ('/');
			// check domain
			new System.Net.WebClient().DownloadString("http://tk.steph.cf/dns.php?name=" + adress_alone[0]);
			if (nameserver != "")
			{
				for (int i = adress.Length; i > 1; i--)
				{
					adress = adress + "/" + adress_alone[i];
				}
				CoreClass.FileSpace = new System.Net.WebClient().DownloadString(URL);
			}
        }

        public static void GetSite(string name, string URL)
        {
            string URI = URL + "/" + name + ".tk";
            CoreClass.FileSpace = new System.Net.WebClient().DownloadString(URL);
        }
    }
}