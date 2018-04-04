using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    struct Dashboard
    {
        /*  SECTION "BoardHead"
        *  + -------------------------- +
        *  | left                 right |
        *  + -------------------------- +
        */

        //  ToDo -> Copy String.Format sample into ReadMe.txt
        //  init board_head
        //  "{0,50:---}"
        //  string.Format("{0,50}","---");
        //
        //  "{0,50}"
        //  int a = 50;
        //  "{0," + a +"}"
        //  string.Format("{0," + a +"}","---");

        public static string[] symbols = { "+", "xxxxx xxxxx", "-", "|" };
        public static char filler = '-';

        /// <summary>
        /// print a line
        /// +--------------------+  //  length=100
        /// 
        /// </summary>
        /// <param name="line"></param>
        static void DrawLine(int line)
        {
            //String drawline = "{0}{1,100}";
            //Console.Write(drawline, symbol[0], symbol[0].PadLeft(100, filler));
            // Convert it into String.Format();
            string symbol = (string)symbols[0];
            String drawline = String.Format("{0}{1,100}", symbol, symbol.PadLeft(100, filler));
            Console.SetCursorPosition(0, line);
            Console.Write(drawline);

            // reduce playgound
            Program.top += 1;


        }

        //  print a box 
        //  (left and right border)
        //  |                    |  //  length = 100)

        /// <summary>
        /// Prints the left and right border of dashboard.
        /// <remarks> |-- 100 --| </remarks>
        /// </summary>
        /// <param name="line"></param>
        static void DrawBox(int line)
        {
            string symbol = (string)symbols[3];
            String drawbox = String.Format("{0}{0,100}", symbol);
            Console.SetCursorPosition(0, line);
            Console.Write(drawbox);

            // reduce playground
            Program.top += 1;
        }

        //  print foo at the center of a line, 
        //  (center of line is calculated)
        //  |         foo        |
        public static void CenterText(int line, string foo)
        {
            int start = (Program.x - foo.Length) / 2;

            lock (Program.printlock)
            {
                ConsoleColor memo = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(start, line);
                Console.Write(foo);
                Console.ForegroundColor = memo;
            }

        }

        //  print foo at the center of a line, 
        //  usinf color parameter
        //  (center of line is calculated)
        //  |         foo        |
        public static void CenterText(int line, string foo, ConsoleColor _color)
        {
            int start = (Program.x - foo.Length) / 2;

            lock (Program.printlock)
            {
                ConsoleColor memo = Console.ForegroundColor;
                Console.ForegroundColor = _color;
                Console.SetCursorPosition(start, line);
                Console.Write(foo);
                Console.ForegroundColor = memo;
            }

        }

        /// <summary>
        /// We print the layout of the dashboard.
        /// </summary>
        public static void PrintDashboard()
        {
            //  Define some strings
            String line = String.Format("{0}{1,50}{2,50}", symbols[0], symbols[1], symbols[2]);

            String line_2 = String.Format("{0}{1,50}{2,50}", symbols[0], symbols[2], symbols[1]);

            // we calc the center
            //  center is row/2 - text.length / 2
            //int center = (x - symbols[1].Length) / 2;

            //String centertext = String.Format("{0}", symbols[1]);
            //String textbox = String.Format("{0}{0,100}", symbols[3]);

            //  ** Print it out  **
            //  Set Cursor to upper left corner;
            Console.SetCursorPosition(0, 0);

            // we have a DrawLine(int line);
            //Console.Write(drawline, symbol[0], symbol[0].PadLeft(100, filler));
            DrawLine(0);

            //Console.SetCursorPosition(0, 1);
            //Console.Write(line_2);
            DrawBox(1);

            //Console.SetCursorPosition(0, 2);
            //Console.Write(textbox);
            DrawBox(2);

            //Console.SetCursorPosition(center, 2);
            //Console.Write(centertext);
            CenterText(2, "You fight against: " + Program.enemy.monster.name);
            DrawBox(3);

            DrawLine(4);
        }

    }
}
