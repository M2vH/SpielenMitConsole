using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashBoard
{
    class Program
    {
        // Set default screen params
        static int x = 101;
        static int y = 30;

        // set screen to default
        static void InitGame()
        {
            Console.Clear();
            Console.WindowWidth = x;
            Console.WindowHeight = y;
            Console.BufferWidth = x;
            Console.BufferHeight = y;

        }

        // move cursor into center
        static void Center()
        {
            Console.CursorLeft = x / 2;
            Console.CursorTop = y / 2;
        }

        // close the game
        static void Close()
        {
            Console.Write("Press any key ...");
            Console.ReadLine();
            
        }

        /*  SECTION "BoardHead"
         *  + -------------------------- +
         *  | left                 right |
         *  + -------------------------- +
         */

        // init board_head
        //  "{0,50:---}"
        //  string.Format("{0,50}","---");
        //
        //  "{0,50}"
        //  int a = 50;
        //  "{0," + a +"}"
        //  string.Format("{0," + a +"}","---");



        static string[] symbols = { "+", "xxxxx xxxxx", "-", "|" };
        static char filler = '-';

        // print a line
        //  +--------------------+  //  length=100)
        static void DrawLine(int line)
        {
            //String drawline = "{0}{1,100}";
            //Console.Write(drawline, symbol[0], symbol[0].PadLeft(100, filler));
            // Convert it into String.Format();
            string symbol = (string)symbols[0];
            String drawline = String.Format("{0}{1,100}", symbol, symbol.PadLeft(100, filler));
            Console.SetCursorPosition(0, line);
            Console.Write(drawline);
        }

        //  print a box 
        //  (left and right border)
        //  |                    |  //  length = 100)
        static void DrawBox(int line)
        {
            string symbol = (string)symbols[3];
            String drawbox = String.Format("{0}{0,100}",symbol);
            Console.SetCursorPosition(0, line);
            Console.Write(drawbox);
        }

        //  print foo at the center of a line, 
        //  (center of line is calculated)
        //  |         foo        |
        static void CenterText(int line, string foo)
        {
            int start = (x - foo.Length) / 2;
            Console.SetCursorPosition(start, line);
            Console.Write(foo);
        }

        // print the board
        static void PrintHead()
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
            CenterText(2, "This is my text!");
            DrawBox(3);

            DrawLine(4);
        }


        // The Game
        static void Main(string[] args)
        {
            InitGame();
            PrintHead();
            Center();



            Close();
            
        }
    }
}
