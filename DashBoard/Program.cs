using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DashBoard
{
    class Program
    {
        // Set default screen params
        static int x = 101;
        static int y = 30;

        static int top = 0;

        static object printlock = new object();

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

            lock (printlock)
            {
                Console.SetCursorPosition(start, line);
                Console.Write(foo);
            }
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
                // store the actual cursor position;
                int pos_y = Console.CursorTop;
                int pos_x = Console.CursorLeft;

                // we need a monster;
                PrintTheMonster(pos_x, pos_y);

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
                            // if we are not at the top:
                            // ( ! top is set when board is created !);
                            if (pos_y > top +1)    
                            {
                                HideTheMonster(pos_x, pos_y);
                                Console.CursorTop = Console.CursorTop - 1;
                            }
                            break;
                        }
                    case ConsoleKey.D:
                        {
                            // if we are not at the outer right
                            // move right;
                            if (pos_x < x - 4)
                            {
                                HideTheMonster(pos_x, pos_y);
                                Console.CursorLeft += 1;
                            }
                            break;
                        }
                    case ConsoleKey.X:
                        {
                            // if we are not at bottom:
                            if (pos_y < y - 3)
                            {
                                HideTheMonster(pos_x, pos_y);
                                Console.CursorTop += 1;
                            }
                            break;
                        }
                    case ConsoleKey.A:
                        {
                            // if we are not at the outer left
                            if (pos_x > 2)
                            {
                                HideTheMonster(pos_x, pos_y);
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
        static void PrintTheMonster(int pos_x, int pos_y)
        {
            string parts = "(° °)" + "#" +   // we use # to split string
                           " ~x~ " + "#" +   // 
                           " ] [ " + "#"    ;

            var monster = parts.Split('#');
            // set position of cursor after every printed part of monster
            // params are the middle of the monster

            lock (printlock)
            {
                // [1] printing the head
                Console.SetCursorPosition(pos_x - 2, pos_y - 1);
                Console.Write(monster[0]);

                // [2] printing the arms
                //      set cursor
                Console.SetCursorPosition(Console.CursorLeft - monster[0].Length, Console.CursorTop + 1);
                //      print arms
                Console.Write(monster[1]);

                // [3] printing the legs
                //      set cursor
                Console.SetCursorPosition(Console.CursorLeft - monster[0].Length, Console.CursorTop + 1);
                //      print legs
                Console.Write(monster[2]);

                // [4] set cursor position back to params
                Console.SetCursorPosition(pos_x, pos_y);
            }
        }

        // Hide the Monster
        static void HideTheMonster(int pos_x, int pos_y)
        {
            // store upper left corner of monster
            int new_x = pos_x - 2;
            int new_y = pos_y - 1;

            // place to store the background
            string monster_string = "";

            // get the chars out of the background array
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    // build the string
                    // get the corresponding char
                    monster_string += background[new_y + j][new_x + i];
                }
                // add the delimeter;
                // we use it like a newline later
                monster_string += "#";
            }

            // put it in the old monster printing function
            string parts = monster_string;

            //string parts = "     " + "#" +   // we use # to split string
            //                 "     " + "#" +   // 
            //                 "     " + "#";

            var blanc_bg = parts.Split('#');
            // set position of cursor after every printed part of monster
            // params are the middle of the monster
            // Set cursor to background color

            // [0] set background color;
            //      store default color;
            ConsoleColor color_backup = Console.ForegroundColor; 
            Console.ForegroundColor = darkgreen;

            lock (printlock)
            {

                // [1] printing the head
                Console.SetCursorPosition(pos_x - 2, pos_y - 1);
                Console.Write(blanc_bg[0]);

                // [2] printing the arms
                //      set cursor
                Console.SetCursorPosition(Console.CursorLeft - blanc_bg[0].Length, Console.CursorTop + 1);
                //      print arms
                Console.Write(blanc_bg[1]);

                // [3] printing the legs
                //      set cursor
                Console.SetCursorPosition(Console.CursorLeft - blanc_bg[0].Length, Console.CursorTop + 1);
                //      print legs
                Console.Write(blanc_bg[2]);

                // [4] set cursor position back to params
                Console.SetCursorPosition(pos_x, pos_y);

                // [5] set CursorColor back to default
                Console.ForegroundColor = color_backup;
            }
        }

        // The Background
        // store the background in 2-dimensional char array
        static string[] background = new string[y-1];

        // create a new ForegroundColor
        static ConsoleColor darkgreen = ConsoleColor.DarkGreen;

        // Draw a random background
        static void DrawBackground()
        {
            // draw a background
            // random o, O, Q and a lot of ' ';
            // Console.SetCursorPosition(0, top);

            Random random = new Random();
            string selection = "o O Q    ";
            int number;
            char symbol;

            // store the default ForegroundColor
            ConsoleColor oldcolor = Console.ForegroundColor;

            // print background in DarkGreen
            Console.ForegroundColor = darkgreen;

            // we go from line top to window.height ( -1 ? )
            for (int i = top; i < y - 1; i++)
            {
                string one_line = "";
                // we go from left to right
                for (int j = 0; j < x; j++)
                {
                    // drag an int, because Random is always int
                    number = random.Next(0, selection.Length - 1);

                    // pull the symbol out of the selection
                    symbol = (char)selection[number];

                    // print symbol 
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


        static Timer mytimer;
        // The Game
        static void Main(string[] args)
        {
            InitGame();
            PrintHead();

            DrawBackground();

            Center();
            // hide the cursor
            Console.CursorVisible = false;

            //  * We start a counter *
            //  * ToDo *
            //  Put counter in his own Thread

            //  
            var autoEvent = new AutoResetEvent(true);

            //  init invokeCount;
            int invokeCount = 0;

            // we run for sixty seconds;
            int maxCount = 120;

            // we display remaining time
            int remainTime = maxCount;

            //TimeSpan startTime = TimeSpan.FromSeconds(maxCount);
            //TimeSpan remainTime, playTime;

            void PrintTime(Object stateInfo)
            {
                ++invokeCount;
                --remainTime;    
                
                // String timeString = String.Format("{0:D2}m :{1:D2}s", remainTime.Minutes, remainTime.Seconds);
                // we print the timer with CenterText(int line, string text);

                // String timeText = String.Format("{0}{1,-15}", "Time remaining", timeString);

                String timeText = String.Format("{0}{1,5} : {2}", "Time remaining", 
                                            arg1: (remainTime / 60).ToString(), 
                                            arg2: (remainTime % 60).ToString());
                
                // CenterText(2, "Hello...        " + (++invokeCount).ToString() ) ;

                CenterText(2, timeText);

                // We start a timer thread
                AutoResetEvent secondAuto = (AutoResetEvent)stateInfo;
                
                if (invokeCount == maxCount)
                    {
                        secondAuto.Set();
                        KillTimer();
                    }
			}

            void KillTimer()
            {
                mytimer.Dispose();
            }

            // *alt*
            //  second param <autoevent>

            mytimer = new Timer(PrintTime, autoEvent, 1000, 1000);

            autoEvent.WaitOne();


            Move();

            // show the cursor;
            Console.CursorVisible = true;

            // Relax for 2 seconds
            Thread.Sleep(2000);

            // Exit and close shell
            Close();
            
        }
    }
}
