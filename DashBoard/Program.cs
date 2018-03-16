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

        static int top = 0;

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
            Center();
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

            // reduce playgound
            top += 1;

            
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

            // reduce playground
            top += 1;
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

        // make a play
        // ! only cursor is moving !
        static void Move()
        {
            bool play = true;
            do
            {
                ConsoleKeyInfo key;
                key = Console.ReadKey(true);
                //char move = Convert.ToChar(key);
                //Console.Write(key.Key);
                switch (key.Key)
                {
                    // monster size is x = 5, y = 3
                    // min/max cursor positions are:
                    // left = 2;            // monster left is 2 pixel from middle
                    // right = x - 2 - 2;   // -2 for the monster size, -2 for the OutOfRange Exception
                    // upper = int top + 1;     // top is set when dashboard is printed;
                    // bottom = y - 1 - 1;  // -1 for the monster size, -1 for the excepiton
                    case ConsoleKey.W:
                        {
                            int pos_y = Console.CursorTop;
                            // if we are not at the top:
                            // top is set when board is created;
                            if (pos_y > top +1)    
                            {
                                Console.CursorTop = Console.CursorTop - 1;
                            }
                            break;
                        }
                    case ConsoleKey.D:
                        {
                            // remember where we actualy are
                            int pos_x = Console.CursorLeft;
                            // if we are not at the outer right
                            // move right;
                            if (pos_x < x - 4)
                            {
                                Console.CursorLeft += 1;
                            }
                            break;
                        }
                    case ConsoleKey.X:
                        {
                            // remember where we are
                            int pos_y = Console.CursorTop;
                            // if we are not at bottom:
                            if (pos_y < y - 2)
                            {
                                Console.CursorTop += 1;
                            }
                            break;
                        }
                    case ConsoleKey.A:
                        {
                            // remember where we are
                            int pos_x = Console.CursorLeft;
                            // if we are not at the outer left
                            if (pos_x > 2)
                            {
                                Console.CursorLeft -= 1;
                            }
                            break;
                        }
                    case ConsoleKey.L:
                        {
                            play = false;
                            break;
                        }
                    case ConsoleKey.H:
                        {
                            Center();
                            break;
                        }
                    default:
                        {
                            // Center();
                            break;
                        }

                }
            } while (play);
        }

        // make a monster
        // 
        /*
        
        (°°)
        /  \
         ][ 
        
        */

            // !! ToDo
            // set Cursor position to middle of Monster
        static void PrintTheMonster(int x, int y)
        {
            string parts = "(° °)" +  "#" +   // we use # to split string
                             "~   ~" + "#" +   // 
                             " ] [ " + "#"    ;

            var monster = parts.Split('#');
            // set position of cursor after every printed part of monster
            // params are the middle of the monster

            // [1] printing the head
            Console.SetCursorPosition(x-2, y-1);
            Console.Write(monster[0]);

            // [2] printing the arms
            //      set cursor
            Console.SetCursorPosition(Console.CursorLeft - monster[0].Length,Console.CursorTop +1);
            //      print arms
            Console.Write(monster[1]);

            // [3] printing the legs
            //      set cursor
            Console.SetCursorPosition(Console.CursorLeft - monster[0].Length, Console.CursorTop + 1);
            //      print legs
            Console.Write(monster[2]);

            // [4] set cursor position back to params
            Console.SetCursorPosition(x,y);

        }

        // Hide the Monster
        static void HideTheMonster(int x, int y)
        {
            string parts = "     " + "#" +   // we use # to split string
                             "     " + "#" +   // 
                             "     " + "#";

            var background = parts.Split('#');
            // set position of cursor after every printed part of monster
            // params are the middle of the monster

            // [1] printing the head
            Console.SetCursorPosition(x - 2, y - 1);
            Console.Write(background[0]);

            // [2] printing the arms
            //      set cursor
            Console.SetCursorPosition(Console.CursorLeft - background[0].Length, Console.CursorTop + 1);
            //      print arms
            Console.Write(background[1]);

            // [3] printing the legs
            //      set cursor
            Console.SetCursorPosition(Console.CursorLeft - background[0].Length, Console.CursorTop + 1);
            //      print legs
            Console.Write(background[2]);

            // [4] set cursor position back to params
            Console.SetCursorPosition(x, y);

        }

        // The Game
        static void Main(string[] args)
        {
            InitGame();
            PrintHead();
            Center();



            Move();

            // hide the cursor
            Console.CursorVisible = false;

            // Display a monster
            PrintTheMonster(Console.CursorLeft, Console.CursorTop);


            System.Threading.Thread.Sleep(2000);

            // Hide the monster (overwrite with space)
            HideTheMonster(Console.CursorLeft, Console.CursorTop);

            Console.CursorVisible = true;

            // Exit and close shell
            Close();
            
        }
    }
}
