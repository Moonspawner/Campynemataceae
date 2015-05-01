using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrowserForSlowNetwork;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace FastSkript
{
    public class FastSkriptMain : IPlugin
    {
        #region Win32
        public enum BeepType
        {
            SimpleBeep = -1,
            IconAsterisk = 0x00000040,
            IconExclamation = 0x00000030,
            IconHand = 0x00000010,
            IconQuestion = 0x00000020,
            Ok = 0x00000000,
        }
        [DllImport("user32.dll")]
        public static extern bool MessageBeep(BeepType beepType);
        [DllImport("kernel32.dll")]
        public static extern bool Beep(int frequency, int duration);
        #endregion
        //Nach Brocken

        bool InZeile;
        bool inbeep;
        bool inbutton;

        int BeepWert;
        int BeepDuration;

        public void ZeilenAbruf(string zeile, ref string filespace, bool incode = false, bool inhead = false, bool intitle = false)
        {
            switch (zeile.Trim())
            {
                //<BEEP>
                case "<beep>":
                    inbeep = true;
                    return;
                case "</beep>":
                    inbeep = false;
                    break;
                //<BUTTON>
                case "<button>":
                    inbutton = true;
                    break;
                case "</button>":
                    inbutton = false;
                    break;
            }

            if (inbeep == true)
            {
                BeepWert = int.Parse(new Regex("[0-9]+").Match(new Regex(@"freq=([0-9]+)").Match(zeile).Value).Value);
                BeepDuration = int.Parse(new Regex("[0-9]+").Match(new Regex(@"ms=([0-9]+)").Match(zeile).Value).Value);

                Beep(BeepWert, BeepDuration);
            }

        }
    }
}
