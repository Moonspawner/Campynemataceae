using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TKBrowser
{
    public class UpdateModul
    {
        static string FileHash = "";
        static string ServerHash = "";
        public static  void StartUpdateProzess(string args, string ignore)
        {
            GetFileHash_Clay();
            GetServerHash_Clay();
            Download_Clay();
        }

        static void GetFileHash_Clay()
        {
            String sourceFileName = "clayruntime.dll";
            Byte[] shaHash;
            using (var shaForStream = new SHA1Managed())
            using (Stream sourceFileStream = File.Open(sourceFileName, FileMode.Open))
            using (Stream sourceStream = new CryptoStream(sourceFileStream, shaForStream, CryptoStreamMode.Read))
            {
                while (sourceStream.ReadByte() != -1) ;
                shaHash = shaForStream.Hash;
            }

            string FileHash = BitConverter.ToString(shaHash).Replace("-", string.Empty);   
        }
        static void GetServerHash_Clay()
        {
            ServerHash = new System.Net.WebClient().DownloadString("http://tk.steph.cf/clay/hash.php");
        }

        static void Download_Clay()
        {
            if(FileHash.ToUpper() != ServerHash.ToUpper())
            {

                Console.WriteLine("ClayRuntime wird Aktualisiert");

                WebClient webClient = new WebClient();
                webClient.DownloadFile("http://tk.steph.cf/clay/clayruntime.dll", @"clayruntime.dll");
               
            }
        }

    }
    
}
