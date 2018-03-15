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



        static string[] symbol = { "+", "xxxxx xxxxx", "-", "|" };
        static char filler = '-';

        // print a line
        //  +--------------------+ (length=100)
        static void DrawLine(int line)
        {
            //String drawline = "{0}{1,100}";
            //Console.Write(drawline, symbol[0], symbol[0].PadLeft(100, filler));
            // Convert it into String.Format();
            String drawline = String.Format("{0}{1,100}", symbol[0], symbol[0].PadLeft(100, filler));
            Console.SetCursorPosition(0, line);
            Console.Write(drawline);
        }


        // print the board
        static void PrintHead()
        {
            //  Define the strings
            String line = String.Format("{0}{1,50}{2,50}", symbol[0], symbol[1], symbol[2]);

            String line_2 = String.Format("{0}{1,50}{2,50}", symbol[0], symbol[2], symbol[1]);

            // we calc the center
            //  center is row/2 - text.length / 2

            int center = (x - symbol[1].Length) / 2;

            // concat it into string format
            //String centertext = String.Format("{0}{1,50}{2,50}", symbol[0], symbol[1], symbol[2]);
            String centertext = String.Format("{0}", symbol[1]);


            String textbox = String.Format("{0}{0,100}", symbol[3]);

            //  Print it out
            Console.SetCursorPosition(0, 0);
            
            // we have a DrawLine(int line);
            //Console.Write(drawline, symbol[0], symbol[0].PadLeft(100, filler));
            DrawLine(0);

            Console.SetCursorPosition(0, 1);
            Console.Write(line_2);

            Console.SetCursorPosition(0, 2);
            Console.Write(textbox);

            Console.SetCursorPosition(center, 2);
            Console.Write(centertext);

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
