using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Parsing_
    {
        //Neuer Parser von Alexmitter
        //Codename Xylospongium
        string Speicher;

        bool Body;
        bool Head;

        bool title;
        bool text;

        
        public void Parser1(string Eingang)
        {
            foreach(var Zeile in Eingang.Split('\n'))
            {
                //Zustände
                switch(Zeile.Trim())
                {
                    case "<head>":
                        Head = true;
                        continue;
                    case @"<\head>":
                        Head = false;
                        continue;
                    case "<body>":
                        continue;
                    case "<\body>":
                        continue;
                    default:
                        break;
                }

                if(Head == true)
                {
                    if(Zeile.Trim() == "<title>")
                    {
                        title = true;
                        continue;
                    }
                    if(Zeile.Trim() == "</title>")
                    {
                        title = false;
                        continue;
                    }
                    if (title == true)
                    {
                        Speicher = "title:" + Zeile.TrimStart() +"\n";
                        continue;
                    }

                }
                if(Body == true)
                {
                    //text
                    if(Zeile.Trim() == "<text>")
                    {
                        text = true;
                        continue;
                    }
                    if (Zeile.Trim() == "<\text>")
                    {
                        text = false;
                        continue;
                    }
                    if(text == true)
                    {
                        Speicher = "text:" + Zeile + "\n";
                    }
                    //box
                    if (Zeile.Trim() == "<box>")
                    {
                        text = true;
                        continue;
                    }
                    if (Zeile.Trim() == "<\box>")
                    {
                        text = false;
                        continue;
                    }
                    if (text == true)
                    {
                        Speicher = "box:" + Zeile + "\n";
                    }
                }


            }

            Console.WriteLine(Speicher);
        }
        
    }
}
