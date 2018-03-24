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
        public static int x = 101;
        public static int y = 30;

        public static int top = 0;

        public static object printlock = new object();

        // init 3 different types of monster...
        static Design goble, frodo, angry;
        // ...and store them
        static Design[] theDesigns;

        static void InitGame()
        {
            // set screen to default
            //  Console Settings
            Console.Clear();
            Console.WindowWidth = x;
            Console.WindowHeight = y;
            Console.BufferWidth = x;
            Console.BufferHeight = y;
            Console.BackgroundColor = ConsoleColor.Black;

            //  Monster Designs;
            /*
            |   Goble   Frodo   Angry
            |
            |   (° °)   {0.0}   [-.-]
            |    ~*~    o-U-o    _+_ 
            |    ] [     { }     U U 
            |
             */

            goble = new Design {
                designName = "Goble",
                designColor = ConsoleColor.White,
                designElements = new string[] { "(° °)", " ~*~ ", " ] [ " },
            };

            frodo = new Design {
                designName = "Frodo",
                designColor = ConsoleColor.Yellow,
                designElements = new string[] { "{0.0}", "o-U-o", " { } " },
            };

            angry = new Design {
                designName = "Angry",
                designColor = ConsoleColor.DarkYellow,
                designElements = new string[] { "[-.-]", " _+_ ", " U U " },
            };

            theDesigns = new Design[] { goble, frodo, angry };

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
            String drawbox = String.Format("{0}{0,100}", symbol);
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

            // Console.CursorVisible = false;
            // ToDo: Ask, why this displays the cursor;
            // Expected behaviour was NO cursor;
            lock (printlock)
            {
                Console.SetCursorPosition(start, line);
                Console.Write(foo);
            }
            // Console.CursorVisible = true;

        }

        // print the head of the board
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

        // ToDo: Question? Where are the CreateMonster functions placed best?

        /* Function <Monster>CreateMonster(string head, string body, string legs)
        // Instantiates a monster with parts as parameter
        */
        static Monster CreatePlayer(string _head, string _body, string _legs, string _name)
        {
            Monster player = new Monster {
                parts = new string[3],
                name = _name,
                pos_x = x / 3,
                pos_y = (y + top) / 2,
            };
            player.parts[0] = _head;
            player.parts[1] = _body;
            player.parts[2] = _legs;

            return player;
        }

        /*  Function <Monster>CreatePlayer(Design _design, string _name)
        //  Creates a gamer monster with a given <Design> at default position
        */
        static Monster CreatePlayer(Design _design, string _name)
        {
            Monster monster = new Monster
            {
                outfit = _design,
                name = _name,
                pos_x = x / 3,
                pos_y = (y + top) / 2,

            };
            return monster;
        }

        /*  <Monster> CreatePlayer(Design _design, int _x, int _y, string _name)
         *  Creates a Monster at a given position;
         */
        static Monster CreatePlayer(Design _design, int _x, int _y, string _name)
        {
            Monster monster = new Monster
            {
                outfit = _design,
                name = _name,
                pos_x = _x,
                pos_y = _y,
            };
            return monster;
        }

        /*  Function < int[] > RandomStartPos()
         *  Calculates a random position in the right side of the field
         *  Returns an int[]
         */
        static Random random = new Random();
        public static int[] RandomStartPos()
        {
            int[] start = new int[2];

            // pos x min/max
            int x_min = x / 2 + 4;  // 4 steps right from middle
            int x_max = x - 4;      // 4 steps left from outer right

            // pos y min/max
            int y_min = 4;          // 4 steps below top
            int y_max = y - 4;      // 4 steps above bottom

            start[0] = random.Next(x_min, x_max);
            start[1] = random.Next(y_min, y_max);

            return start;
        }

        // make a play
        // ! only cursor is moving !
        static void Move(Monster _player)
        {

            /* Instantiate a monster;
            
            Monster player = CreatePlayer("(° °)", " ~*~ ", " ] [ ", "The fast Goblin");
            */

            /*  We ceate a monster with an existing design
             *  
             */
            Monster player = _player;

            bool play = true;
            do
            {
                //  we need a monster;
                //  PrintTheMonster(pos_x, pos_y);
                player.PrintMonster(player.pos_x, player.pos_y);

                ConsoleKeyInfo key;
                key = Console.ReadKey(true);

                lock (printlock)
                {
                    switch (key.Key)
                    {
                        // monster size is x = 5, y = 3
                        // min/max cursor positions are:
                        // left = 2;            // monster left is 2 pixel from middle
                        // right = x - 2 - 2;   // -2 for the monster size, -2 for the OutOfRange Exception
                        // upper = int top + 1;     // top is set when dashboard is printed;
                        // bottom = y - 1 - 1;  // -1 for the monster size, -1 for the excepiton
                        case ConsoleKey.W:
                        case ConsoleKey.UpArrow:
                            {
                                // if we are not at the top:
                                // ( ! top is set when board is created !);
                                if (player.pos_y > top + 1)
                                {
                                    player.HideMonster(player.pos_x, player.pos_y);
                                    // Console.CursorTop = Console.CursorTop - 1;
                                    player.pos_y--;
                                }
                                break;
                            }
                        case ConsoleKey.D:
                        case ConsoleKey.RightArrow:
                            {
                                // if we are not at the outer right
                                // move right;
                                if (player.pos_x < x - 4)
                                {
                                    player.HideMonster(player.pos_x, player.pos_y);
                                    // Console.CursorLeft += 1;
                                    player.pos_x++;
                                }
                                break;
                            }
                        case ConsoleKey.S:
                        case ConsoleKey.DownArrow:
                            {
                                // if we are not at bottom:
                                if (player.pos_y < y - 3)
                                {
                                    player.HideMonster(player.pos_x, player.pos_y);
                                    // Console.CursorTop += 1;
                                    player.pos_y++;
                                }
                                break;
                            }
                        case ConsoleKey.A:
                        case ConsoleKey.LeftArrow:
                            {
                                // if we are not at the outer left
                                if (player.pos_x > 2)
                                {
                                    player.HideMonster(player.pos_x, player.pos_y);
                                    // Console.CursorLeft -= 1;
                                    player.pos_x--;
                                }
                                break;
                            }
                        case ConsoleKey.L:
                            {
                                play = false;
                                KillTimer();
                                // Stop the enemy's moving
                                // monsterMove.Dispose();
                                break;
                            }
                        case ConsoleKey.H:
                            {
                                // we lock this section
                                lock (printlock)
                                {
                                    player.HideMonster(player.pos_x, player.pos_y);
                                    Center();
                                    player.pos_x = Console.CursorLeft;
                                    player.pos_y = Console.CursorTop;
                                }
                                break;
                            }
                        case ConsoleKey.Spacebar:
                            {
                                // let the monster fight;
                                // ToDo: create a monster.fight()
                                break;
                            }
                        default:
                            {
                                // Center();
                                break;
                            }

                    }
                } // end of lock

            } while (play);
        }


        /* 
        * The Background
        * store the background in 2-dimensional char array
        */

        // var to store the background in
        public static string[] background = new string[y - 1];

        // create a new ForegroundColor for background printing

        public static ConsoleColor gameBackground = ConsoleColor.DarkMagenta;

        /* ToDo create a background struct
        //  to store the colors
        */

        // Draw a random background
        static void DrawBackground()
        {
            // draw a background
            // random o, O, Q and a lot of ' ';
            // Console.SetCursorPosition(0, top);

            Random random = new Random();
            string selection = ". : ;     ";
            int number;
            char symbol;

            // store the default ForegroundColor
            ConsoleColor oldcolor = Console.ForegroundColor;

            // print background in DarkGreen
            Console.ForegroundColor = gameBackground;

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



        /*  The Timer
         *  We run a Timer and print a countdown in its own thread.
         */

        static Timer mytimer;

        // We call this function when the timer thread callback is ready
        static AutoResetEvent autoEvent = new AutoResetEvent(true);

        //  we count every timer callback call
        //  init invokeCount;
        static int invokeCount = 0;

        // we want to run for given amount of seconds;
        static int maxCount = 120;

        // we store countdown here;
        // and init remaining time with maximum seconds
        static int remainTime = maxCount;

        // the string we want to print
        static String timeText;

        // this function is the callback for the timer
        static void PrintTime(Object stateInfo)
        {
            // yes, we can
            ++invokeCount;
            --remainTime;

            timeText = String.Format("{0}{1,5} : {2}", "Time remaining",
                                        arg1: (remainTime / 60).ToString(),
                                        arg2: (remainTime % 60).ToString());

            // print the Countdown in the center of our dashboard
            CenterText(2, timeText);

            // We send signals to waiting threads
            AutoResetEvent secondAuto = (AutoResetEvent)stateInfo;

            if (invokeCount == maxCount)
            {
                secondAuto.Set();
                KillTimer();
            }
        }


        // we clean up the thread
        static void KillTimer()
        {
            mytimer.Dispose();
        }

        static void StartTimer() {
            mytimer = new Timer(PrintTime, autoEvent, 1000, 1000);

            autoEvent.WaitOne();

        }

        /*  ToDo: Create an enemy at random position
         *  
         *  [1] CreateEnemy()
         */
        public static Monster CreateEnemy(Monster _player)
        {
            //  get the design of player
            //  and select different one
            int id = 0;
            Design enemyDesign = theDesigns[id];
            while (enemyDesign.designName == _player.outfit.designName)
            {
                id = random.Next(theDesigns.Length - 1);
                enemyDesign = theDesigns[id];
            }

            // store a random start position
            int[] here = RandomStartPos();
            Monster enemy = CreatePlayer(enemyDesign, here[0], here[1], "The incredible " + enemyDesign.designName);
            return enemy;
        }

        /*  
         *  The Monster Movement Timing
         */

        //  The Monster Timer objects;
        static Timer monsterMove;

        static AutoResetEvent resetMonsterTimer = new AutoResetEvent(true);

        //  <void> StartMonsterTimer(int _millis);
        static void StartMonsterTimer(int _millis)
        {
            monsterMove = new Timer(MoveMonster, resetMonsterTimer, 1000, _millis);
            resetMonsterTimer.WaitOne();
        }

        
        //  the Timer CallBack Funktion
        //  This function is called,
        //  when Timer ticks happen.
        static void MoveMonster(Object _stateInfo)
        {
            AutoResetEvent resetMoveMonster = (AutoResetEvent)_stateInfo;

            /*  
             *  We will be called every _millis
             */
 
            // give waiting threads a chance to work
            resetMoveMonster.Set();

            // if shit happens
            // monsterMove.Dispose();

            //  Get random direction;
            //  Calc new position;
            //  set position of monster

        }

        /*
         *  Monster Movement choices
         *  
         *  Looks like the most stupid way
         *  
         */
        static Direction go = new Direction
        {
            up = new int[] { 0, -1 },
            right_up = new int[2] { 1, -1 },
            right = new int[] { 1, 0 },
            right_down = new int[] { 1, 1 },
            down = new int[] { 0, 1 },
            left_down = new int[] { -1, 1 },
            left = new int[] { -1, 0 },
            left_up = new int[] { -1, -1 },
            stay = new int[] { 0, 0 }
        };

        // make a choice
        static Choice[] choices = new Choice[5];

        static void InitChoices()
        {
            choices[0] = new Choice { coord = go.up };              //  1,2
            choices[1] = new Choice { coord = go.right };           //  3,4,5,6,7,8
            choices[2] = new Choice { coord = go.left };           //  9,10,11,12,13,14
            choices[3] = new Choice { coord = go.down };            //  15,16
            choices[4] = new Choice { coord = go.stay };            //  17
        }

        /*
         *  Get a weighted random direction
         */

        static int[] RandomMove()
        {
            int[] goHere = new int[] { 0, 0 };
            int selected = random.Next(1, 18);

            if (0 < selected || selected < 3)
            {
                goHere = choices[1].coord;
            }
            else if (3 <= selected || selected < 9)
            {
                goHere = choices[2].coord;
            }
            else if (9 <= selected || selected < 15)
            {
                goHere = choices[3].coord;
            }
            else if (15 <= selected || selected < 17)
            {
                goHere = choices[4].coord;
            }
            else if (selected == 17)
            {
                goHere = choices[5].coord;
            }

            return goHere;
        }

        /*
         *  We move a monster in random direction
         */


        /* static void MoveTheMonster(Monster _enemy, int _due, int _speed)
        {
            AutoResetEvent reset = new AutoResetEvent(true);
            object bla = new object();
            monsterMove = new Timer(_enemy.MoveMe, reset, _due, _speed);
            int[] xy = RandomMove();
            // int[] xy = new
            _enemy.HideMonster(_enemy.pos_x, _enemy.pos_y);
            _enemy.pos_x = xy[0];
            _enemy.pos_y = xy[1];
            //_enemy.MoveMe(bla);
            reset.WaitOne();
        }
        */

        /* --> The Game  <--
         * 
         * Keep the Main() as clean as possible
         * 
         */

        public static Enemy enemy;
        public static Monster player;

        static void Main(string[] args)
        {
            
            InitGame();
            PrintHead();

            DrawBackground();

            Center();
            
            // Init the choices
            InitChoices();

            // hide the cursor
            Console.CursorVisible = false;

            // Create a player...
            //  [1]...from given Design
            //  player = CreatePlayer(goble, "Goble");
            //  [2]...from random design
            int r = random.Next(0, theDesigns.Length);
            player = CreatePlayer(theDesigns[r], theDesigns[r].designName);

            /* Create an enemy...
            *  [1] this works
            *  enemy = CreateEnemy(player);
            *  ...and print it;
            *  enemy.PrintMonster();
            */
            
            
            //  [2] test the <struct>Enemy
            //  Enemy enemy = new Enemy();  // This works
            enemy = new Enemy();    
            //  [2.1] CreateEnemyFromDesign
            //  enemy.CreateEnemyFromDesign(angry, "The angry"); // This works
            enemy.CreateEnemyFromOponent();

            //  [3] Print the Enemy
            enemy.monster.PrintMonster();



            //  we need a callback
            //  MoveTheMonster(enemy, 500, 500);


            // start Timer
            StartTimer();

            // >> Loop for enemy movement comes here
            //  StartMonsterTimer(500);

            // check the random move selection
            int[] test = RandomMove();

            // next time we call it GameLoop
            Move(player);

            // Relax for 2 seconds
            Thread.Sleep(2000);

            // show a nice cursor;
            Console.CursorSize = 1;
            Console.CursorVisible = true;

            // A chance to Exit and close shell
            Close();
            
        }
    }
}
