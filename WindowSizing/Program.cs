using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowSizing
{
    class Program
    {
        static void Wait(int time)
        {
            System.Threading.Thread.Sleep(time * 1000);
        }

        private const string PrintBuffer = "Buffer ist {0} Zeichen breit\n und {1} Zeichen hoch.";

        static void Main(string[] args)
        {
            // Fensterbuffergröße abrufen.
            // Wenn Buffer größer als Fenster,
            // dann werden Zeichen am rechten Rand nicht angezeigt.

            // Fensterbuffer abrufen
            int buffwidth, buffheight;
            buffwidth = Console.BufferWidth;

            // BufferHeight ist 9001.
            buffheight = Console.BufferHeight;

            Console.WriteLine(PrintBuffer, buffwidth, buffheight);

            // Fenstergröße abrufen
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            Console.WriteLine("Breite startet bei {0} und Höhe ist {1}.", width, height);


            // Setze Window auf halbe Breite;
            int halfthewidth = width / 2;

            Console.SetWindowSize(halfthewidth, height);

            Console.WriteLine("Breite auf {0} halbiert.", halfthewidth);


            // Game Init
            int game_buff_width, game_buff_height;
            game_buff_width = 101;
            game_buff_height = 30;

            Wait(1);

            // Mit einem Wisch ist alles weg.
            Console.Clear();
            Console.BufferWidth = game_buff_width;
            Console.BufferHeight = game_buff_height;
            Console.WindowWidth = game_buff_width;
            Console.WindowHeight = game_buff_height;

            // Trommelwirbel
            Wait(1);


            // Cursorposition:
            // Setze Cursor in die Mitte;
            // Speicher aktuelle Cursorposition;
            var cur_pos_x = Console.CursorLeft;
            var cur_pos_y = Console.CursorTop;
            
            // Cursor auf halbe Breite
            int x = game_buff_width/2;
            // und halbe Höhe
            int y = game_buff_height/2;
            
            Console.SetCursorPosition(x, y);

            // Warte 1 Sekunde...
            System.Threading.Thread.Sleep(1000);

            //



            // Ausstieg aus dem Programm
            // dotline .....
            var dotline = " ...";
            // Abstand zum Text
            string dist = "20";
            int d = 20;

            // {0,20:}
            // rechter Bund mit 20 Zeichen Abstand
            Console.WriteLine("Press any key {0}\n", dotline);
            Console.CursorLeft = x;
            Console.CursorTop = y;

            // Wir schreiben über den Text.
            Wait(2);
            Console.Write("XXXXX");
            Console.CursorVisible = false;
            Console.ReadLine();
        }
    }
}
