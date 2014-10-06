using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using BrowserForSlowNetwork;
using System.Reflection;
using System.ComponentModel;
using System.Media;
using System.Diagnostics;

namespace BrowserForSlowNetwork
{
    class CoreTick
    {
        public static void StartCore()
        {
            Tick();
        }

        public static void RestartCore()
        {
            Console.Clear();
            Tick();
        }

        static void Tick()
        {
            CoreClass.Init();
            NetzwerkFunktionen.Aufrufen();
            NetzwerkFunktionen.Downloader();
            CoreClass.Tags();
            CoreClass.Ausgabe();
            RestartCore();
        }
    }
}
