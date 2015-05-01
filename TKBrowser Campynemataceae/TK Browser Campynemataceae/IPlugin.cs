using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserForSlowNetwork
{
    public interface IPlugin
    {
        //Nach Brocken
        void ZeilenAbruf(string zeile, ref string filespace, bool incode = false, bool inhead = false, bool intitle = false);
    }
}
