using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    struct Background
    {
        /* The Background
        * ToDo: create struct with int x, int y, background {zeichen, foregroundColor}
        */
        public static Symbol[,] signs = new Symbol[Window.x+1,Window.y];
        // var to store the background in
        public static string[] background = new string[Window.y - 1];

        // create a new ForegroundColor for background printing
        public static ConsoleColor fieldColor = ConsoleColor.DarkMagenta;

        // Draw a random background
        /// <summary>
        /// Draw a random background from given symbols
        /// </summary>
        public static void DrawInMagenta()
        {
            // draw a background
            // Console.SetCursorPosition(0, top);

            Random random = new Random();
            string selection = ". : ;     ";
            int number;
            char symbol;

            // store the default ForegroundColor
            ConsoleColor oldcolor = Console.ForegroundColor;

            // print background in DarkGreen
            Console.ForegroundColor = fieldColor;

            // we to from line top to window.height ( -1 ? )
            for (int i = Window.top; i < Window.y - 1; i++)
            {
                string one_line = "";
                // we to from left to right
                for (int j = 0; j < Window.x; j++)
                {
                    // drag an int, because Random is always int
                    number = random.Next(0, selection.Length - 1);

                    // pull the zeichen out of the selection
                    symbol = (char)selection[number];

                    // print zeichen 
                    Console.SetCursorPosition(j, i);
                    Console.Write(symbol);

                    // and store it for recovery when hiding a monster
                    one_line += symbol;
                    background[i] = one_line;

                }
            }

            // Set back the ForegroundColor
            Console.ForegroundColor = oldcolor;

        }


    }
}
