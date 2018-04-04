using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonsterHunter
{
    class Program
    {
        #region Alle Variablen werden hier deklariert
        // Set the default screen params
        /// <summary>
        /// The console window
        /// <value>width</value>
        /// </summary>
        public static int x = 101;
        /// <summary>
        /// The console window
        /// <value>height</value>
        /// </summary>
        public static int y = 30;

        /// <summary>
        /// The top y-coordinate of the field
        /// </summary>
        /// <remarks>Field is window (y) - dashboard. Value increases when dashboard is printed</remarks>
        public static int top = 0;

        /// <summary>
        /// The lock object to protect console printing
        /// </summary>
        /// <example>lock (printlock){ ...the protected code }</example>
        public static object printlock = new object();

        // declare 3 different types of monster...
        /// <summary>
        /// The "Goble".
        /// <remarks>The anxious monster. Medium speed, low attac, top resistant</remarks>
        /// </summary>
        static Design goble;
        /// <summary>
        /// The "Frodo".
        /// <remarks>The ugly monster. High speed, medium attac, low resistant</remarks>
        /// </summary>
        static Design frodo;
        /// <summary>
        /// The "Angry".
        /// <remarks>The bad monster. low speed, top attac, medium resistant</remarks>
        /// </summary>
        static Design angry;

        // ...and declare a variable to store them
        /// <summary>
        /// An Array to store the available typ of monster.
        /// </summary>
        static Design[] theDesigns;


        #endregion

        /// <summary>
        /// Init the game. 
        /// Set concole size and color.
        /// Set the layout of the monster.
        /// </summary>
        static void InitGame()
        {
            //  set screen to default
            //  Console Settings
            Console.Clear();
            Console.WindowWidth = x;
            Console.WindowHeight = y;
            Console.BufferWidth = x;
            Console.BufferHeight = y;
            Console.BackgroundColor = ConsoleColor.Black;

            #region Monster design
            //  Monster Designs;
            /*
            |   Goble   Frodo   Angry
            |
            |   (° °)   {0.0}   [-.-]
            |
            |    ~▓~    o-▓-o    '▓'
            |    ] [     { }     U U 
            |
             */

            goble = new Design
            {
                designName = "Goble",
                designColor = ConsoleColor.White,
                designElements = new string[] { "(°;°)", " ~▓~ ", " ] [ ", "O-▓-O" },
                FightSound = new Sound { ASound = sound_1 },
                // top resistance: 30
                stats = new Stats
                {
                    hPoints = 500,
                    aPoints = 40,
                    dPoints = 30,
                }
            };

            frodo = new Design
            {
                designName = "Frodo",
                designColor = ConsoleColor.Yellow,
                designElements = new string[] { "{O.O}", " /▓\\ ", " { } ", "o-▓-o" },
                FightSound = new Sound { ASound = sound_2 },
                // low resistance: 10
                stats = new Stats
                {
                    hPoints = 500,
                    aPoints = 50,
                    dPoints = 10,
                }
            };

            angry = new Design
            {
                designName = "Angry",
                designColor = ConsoleColor.Green,
                designElements = new string[] { "[-.-]", " '▓' ", " U U ", "0-▓-0" },
                FightSound = new Sound { ASound = sound_3 },
                // medium resistance: 20
                stats = new Stats
                {
                    hPoints = 500,
                    aPoints = 60,
                    dPoints = 20,
                },
            };
            #endregion

            theDesigns = new Design[] { goble, frodo, angry };

        }



        /// <summary>
        /// Center the cursor into center of field.
        /// </summary>
        static void Center()
        {
            Console.CursorLeft = x / 2;
            Console.CursorTop = y / 2;
        }

        /// <summary>
        /// Closing the game. Display "Press any key...".
        /// </summary>
        public static void Close()
        {
            // in a close fight both can die!
            if (enemyStats.GetHPoints() <= 0 && playerStats.GetHPoints() <= 0)
            {
                winner.name = "Nobody. Everybody is DEAD!";
            }

            // who is the winner?
            if (enemyStats.GetHPoints() <= 0)
            {
                winner = player;
                // Hide the looser
                enemy.monster.HideMonster(enemy.monster.pos_x, enemy.monster.pos_y);
                // Display the player again to avoid fractals
                player.PrintMonster();

                // stop the enemy timer
                enemyTimer.Dispose();
            }
            else
            {
                winner = enemy.monster;
                player.HideMonster(player.pos_x, player.pos_y);
                // Even he is hidden, we must put him aside.
                player.pos_x = 10;
                player.pos_y = 10;

                // display winner to avoid fractals
                enemy.monster.PrintMonster();

                // enemy stops moving when winner
                // slow down the enemy movement
                // enemyTimer.Change(0, 1500);
            }



            //  Center();   //
            int here = y - 5 - top / 2;
            lock (printlock)
            {
                playSong = false;
                ConsoleColor red = ConsoleColor.Red;
                Thread.Sleep(500);
                string grats = "The Winner is... " + winner.name;
                CenterText(here, grats, red);
                CenterText(++here, "Press ENTER to close game ", red);
                Thread.Sleep(2000);
                Console.ReadLine();
            }

        }

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

        static string[] symbols = { "+", "xxxxx xxxxx", "-", "|" };
        static char filler = '-';

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
            top += 1;


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
            top += 1;
        }

        //  print foo at the center of a line, 
        //  (center of line is calculated)
        //  |         foo        |
        public static void CenterText(int line, string foo)
        {
            int start = (x - foo.Length) / 2;

            lock (printlock)
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
            int start = (x - foo.Length) / 2;

            lock (printlock)
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
        static void PrintDashboard()
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
            CenterText(2, "You fight against: " + enemy.monster.name);
            DrawBox(3);

            DrawLine(4);
        }

        // ToDo: Question? Where are the CreateMonster functions placed best?

        /* Function <Monster>CreateMonster(string head, string body, string legs)
        // Instantiates a monster with parts as parameter
        */
        static Monster CreatePlayer(string _head, string _body, string _legs, string _name)
        {
            Monster player = new Monster
            {
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
        public static Random random = new Random();     // ToDo: Make sure, we do it only once
        public static int[] RandomStartPos()
        {
            int[] start = new int[2];

            // pos x min/max
            int x_min = x / 2 + 4;  // 4 steps right from middle
            int x_max = x - 4;      // 4 steps left from outer right

            // pos y min/max
            int y_min = top + 4;          // 4 steps below top
            int y_max = y - 4;      // 4 steps above bottom

            start[0] = random.Next(x_min, x_max);
            start[1] = random.Next(y_min, y_max);

            return start;
        }

        static bool play = true;
        // make a play
        // ! only cursor is moving !
        /// <summary>
        /// Start the game
        /// </summary>
        /// <remarks>Runs until Key.L is pressed</remarks>
        /// <param name="_player">The player monster</param>
        static void PlayTheGame(Monster _player)
        {

            try
            {

                /*  We receive a monster for the player with an existing design
                 */
                player = _player;

                while (play)
                {

                    // we check if player is dead
                    if (playerStats.GetHPoints() <= 0 || player.outfit.stats.GetHPoints() <= 0)
                    {
                        // we dont want to run anymore
                        play = false;
                        // we stop the countdown
                        KillCountdown();

                        // we DONT stop the enemy, because his thread will run
                        // until enemy is looser.
                        //StopEnemy();

                        // dont display a dead player
                        player.HideMonster(player.pos_x, player.pos_y);

                        // leave this loop
                        Close();
                        break;

                    }
                    // player is alive
                    else
                    {
                        // we check if he is winner
                        if (enemyStats.GetHPoints() <= 0)
                        {
                            // player has won;
                            // stop the clock;
                            KillCountdown();
                            Close();
                            break;

                        }

                        //  we need a monster;
                        //  PrintTheMonster(pos_x, pos_y);
                        player.PrintMonster(player.pos_x, player.pos_y);

                        ConsoleKeyInfo key;
                        key = Console.ReadKey(true);

                        // protect next steps against other threads
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
                                        KillCountdown();
                                        // Stop the enemy's moving
                                        enemyTimer.Change(0, 2000);
                                        break;
                                    }
                                case ConsoleKey.H:
                                    {
                                        // we lock this section
                                        lock (printlock)
                                        {
                                            // jump into middle of screen
                                            // player.HideMonster(player.pos_x, player.pos_y);
                                            // Center();
                                            // player.pos_x = Console.CursorLeft;
                                            // player.pos_y = Console.CursorTop;

                                            // we can fight;
                                            player.Fight(player);

                                            if (dist.distance < 4)
                                            {
                                                // player.HitMonster(player.outfit.stats, enemy.monster.outfit.stats);
                                                player.HitMonster(playerStats, enemyStats, true);
                                            }
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

                    } // end of else

                } // end of while
            }
            catch (ThreadAbortException ex)
            {

                System.Diagnostics.Debug.WriteLine("Catch: PlayTheGame " + ex);
            }


        } // end of function


        /* 
        * The Background
        * ToDo: create struct with int x, int y, background {symbol, foregroundColor}
        */

        // var to store the background in
        public static string[] background = new string[y - 1];

        // create a new ForegroundColor for background printing
        public static ConsoleColor gameBackground = ConsoleColor.DarkMagenta;

        // Draw a random background
        /// <summary>
        /// Draw a random background from given symbols
        /// </summary>
        static void DrawBackground()
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

        /// <summary>
        /// The countdown timer object
        /// </summary>
        static Timer countdown;

        // We call this function when the timer thread callback is ready
        static AutoResetEvent autoEvent = new AutoResetEvent(true);

        //  we count every timer callback call
        //  init invokeCount;
        /// <summary>
        /// Object to store how often Countdowntimer calls.
        /// </summary>
        static int invokeCount = 0;

        // we want to run for given amount of seconds;
        /// <summary>
        /// The maximum Countdown Time in seconds
        /// </summary>
        static int maxCount = 120;

        // we store countdown here;
        // and init remaining time with maximum seconds
        /// <summary>
        /// Object to store the extant time
        /// </summary>
        static int remainTime = maxCount;

        // the string we want to print
        /// <summary>
        /// The string in the printout of the Countdown
        /// </summary>
        static String timeText;

        // this function is the callback for the timer
        /// <summary>
        /// The callback function for countdown timer
        /// </summary>
        /// <remarks>Prints the Countdown string</remarks>
        /// <param name="stateInfo">The Timer Event handle</param>
        static void PrintTime(Object stateInfo)
        {
            // yes, we can
            ++invokeCount;
            --remainTime;

            timeText = String.Format("{0,-16}{1,5} : {2}", "Time remaining",
                                        arg1: (remainTime / 60).ToString(),
                                        arg2: (remainTime % 60).ToString("D2"));

            // Clear the dashboard with Key.Space
            string clear = String.Format("{0,50}", " ");
            CenterText(2, clear);

            // print the Countdown in the center of our dashboard
            CenterText(2, timeText);

            // We send signals to waiting threads
            AutoResetEvent secondAuto = (AutoResetEvent)stateInfo;

            if (invokeCount == maxCount)
            {
                secondAuto.Set();
                KillCountdown();
            }
        }


        // we clean up the thread
        /// <summary>
        /// Kill the Timer Thread when Key.L ends the game
        /// </summary>
        static void KillCountdown()
        {
            countdown.Dispose();
        }

        /// <summary>
        /// Fire Countdown event every 1000ms
        /// </summary>
        static void StartTimer()
        {
            try
            {
                countdown = new Timer(PrintTime, autoEvent, 2000, 1000);
                autoEvent.WaitOne();
            }
            catch (ThreadAbortException ex)
            {
                System.Diagnostics.Debug.WriteLine("Catch: StartTimer " + ex);
            }

        }

        /*  Create an enemy at random position
         *  
         *  [1] CreateEnemy()
         */
        /// <summary>
        /// Create an enemy different from Player
        /// </summary>
        /// <remarks>Set enemy start position to random position</remarks>
        /// <param name="_player">The player monster</param>
        /// <returns>Returns an enemy monster</returns>
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
        /// <summary>
        /// The Timer object for enemy movement
        /// </summary>
        static Timer enemyTimer;

        /// <summary>
        /// The callback when enemy move event is ready
        /// </summary>
        static AutoResetEvent resetMonsterTimer = new AutoResetEvent(true);

        //  <void> StartEnemyTimer(int _millis);
        /// <summary>
        /// Starts a Timer after 1000ms for monster movement.
        /// <remarks>1500 is really slow, 200 is maximum quick</remarks>
        /// </summary>
        /// <param name="_millis">Frequency of monster movement</param>
        static void StartEnemyTimer(int _millis)
        {

            try
            {
                enemyTimer = new Timer(MoveMonster, resetMonsterTimer, 1000, _millis);
                resetMonsterTimer.WaitOne();

            }
            catch (ThreadAbortException ex)
            {

                System.Diagnostics.Debug.WriteLine("Catch: StartEnemyTimer " + ex);
            }
        }


        //  the Timer CallBack Funktion
        //  This function is called,
        //  when Timer ticks happen.
        /// <summary>
        /// The Monster Timer Event Callback
        /// </summary>
        /// <remarks>Moves the monster. Is Callback function for Timer Event</remarks>
        /// <param name="_stateInfo">The Event Handle</param>
        static void MoveMonster(Object _stateInfo)
        {
            AutoResetEvent resetMoveMonster = (AutoResetEvent)_stateInfo;

            /*  
             *  We will be called every <int>_millis
             */

            // we store some data
            int[] move = { 0, 0 };
            int new_x = 0;
            int new_y = 0;
            int pos_x = enemy.monster.pos_x;
            int pos_y = enemy.monster.pos_y;
            bool moveIsPossible = true;

            try
            {

                while (moveIsPossible)
                {
                    // Is enemy still alive?
                    if (playerStats.GetHPoints() <= 0 || enemyStats.GetHPoints() <= 0)
                    {
                        moveIsPossible = false;
                        KillCountdown();
                        if (playerStats.GetHPoints() <= 0)
                        {
                            // player is dead
                            StopPlayer();
                            break;
                        }

                    }

                    move = RandomMove();
                    // erst hauen, dann laufen;
                    // und nur nebeneinander kämpfen;
                    if (dist.GetDistance() < 5 && dist.diff_y < 4)
                    {
                        int r = random.Next(2, 14);
                        if ((r % 2) == 0)
                        {
                            enemy.monster.Fight(enemy.monster);
                            // enemy.monster.HitMonster(enemyStats,playerStats);
                            enemy.monster.HitMonster(playerStats, enemyStats, false);
                        }
                    }
                    new_x = pos_x + move[0];
                    new_y = pos_y + move[1];

                    // min_x < new_x < max_x
                    // is next move inside the field?
                    if ((2 < new_x && new_x < x - 3) && (top + 1 < new_y && new_y < y - 2))
                    {
                        // we are inside field
                        enemy.monster.HideMonster(enemy.monster.pos_x, enemy.monster.pos_y);
                        enemy.monster.pos_x += move[0];
                        enemy.monster.pos_y += move[1];
                        enemy.monster.PrintMonster();


                        // check for health
                        // if both alive, print distance,
                        if (playerStats.GetHPoints() > 0 && enemyStats.GetHPoints() > 0)
                        {
                            dist.PrintTheDist();
                            break;
                        }
                        else
                        {
                            // someone is dead
                            KillCountdown();
                            play = false;
                            moveIsPossible = false;
                            // if player is NOT moving, he will not
                            // recognize he is dead.
                            // The hard way;
                            // we must catch exception
                            // StopPlayer();
                            break;
                        }


                    }
                    else
                    {
                        moveIsPossible = true;
                    }


                } // end of while
            }
            catch (ThreadAbortException ex)
            {

                System.Diagnostics.Debug.WriteLine("Catch: MovePlayer() " + ex);
            }


            // give waiting threads a chance to work
            resetMoveMonster.Set();

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
            choices[2] = new Choice { coord = go.left };           //  9,10,11,12,13,14,15,16
            choices[3] = new Choice { coord = go.down };            //  17,18
            choices[4] = new Choice { coord = go.stay };            //  19
        }

        /*  
         *  Get a weighted random direction
         *  
         *  ToDo: Create a weight formula
         *  
         */

        /// <summary>
        /// Calculates a weighted random enemy movement
        /// </summary>
        /// <remarks>Weighted movement into left part of field</remarks>
        /// <returns>An int array with next x,y coords</returns>
        static int[] RandomMove()
        {
            int[] goHere = new int[] { 0, 0 };
            int selected = random.Next(1, 20);

            if (selected >= 0 && selected < 3)  // 
            {
                goHere = choices[0].coord;
            }
            else if (selected >= 3 && selected < 9)
            {
                goHere = choices[1].coord;
            }
            else if (9 <= selected && selected < 17)
            {
                goHere = choices[2].coord;
            }
            else if (17 <= selected && selected < 20)
            {
                goHere = choices[3].coord;
            }
            else if (selected == 20)
            {
                goHere = choices[4].coord;
            }

            return goHere;
        }

        /* Init Player and Enemy
         * Player comes in random outfit
         * and Enemy is different then Player
         */

        public static Enemy enemy;
        public static Monster player;
        public static Monster winner;

        /// <summary>
        /// Inits a player and his enemy
        /// </summary>
        /// <remarks>The player is random and the enemy is different than player</remarks>
        static void InitPlayerAndEnemy()
        {
            // Create a player...
            //  [1]...from given Design
            //  player = CreatePlayer(angry, "Angry");
            //  [2]...from random design
            int r = random.Next(0, theDesigns.Length);
            player = CreatePlayer(theDesigns[r], theDesigns[r].designName);
#if DEBUG
            int reduction = 400;
            player.outfit.stats.SetHPoints(reduction);
#endif

            /* Create an enemy...
            *  [1] this works
            *  enemy = CreateEnemy(player);
            *  ...and print it;
            *  enemy.PrintMonster();
            *
            *  [2] test the <struct>Enemy
            *  Enemy enemy = new Enemy();  // This works
            *  enemy = new Enemy();    
            *  [2.1] CreateEnemyFromDesign
            *  enemy.CreateEnemyFromDesign(angry, "The angry"); // This works
            *  enemy.CreateEnemyFromOponent();

            *  [3] Print the Enemy
            *  enemy.monster.PrintMonster();

            *  we need a callback
            *  MoveTheMonster(enemy, 500, 500);
            */
            enemy = new Enemy();
            enemy.CreateEnemyFromOponent();
#if DEBUG
            enemy.monster.outfit.stats.SetHPoints(reduction);
#endif
            enemy.monster.PrintMonster();
        }

        /// <summary>
        /// An object to store the distance between player and enemy.
        /// </summary>
        public static Distance dist = new Distance();

        /* --> The Game  <--
         * 
         * Keep the Main() as clean as possible
         * 
         */

        /*  The sound machine
         *  Implementation of async fighting sound.
         *  ToDo: let each monster sound different.
         */
        public static int[] sound_1 = { 300, 750 };
        public static int[] sound_2 = { 600, 750 };
        public static int[] sound_3 = { 900, 750 };

        // The async call of the fight sound function
        /// <summary>
        /// The asyncronuos Task of the PlaySound.
        /// </summary>
        /// <remarks>Takes an number for intervals</remarks>
        /// <param name="_i">Sound id played "_i" times</param>
        /// <param name="_sound">Sound of monster design</param>
        /// <returns></returns>
        static Task PlaySoundAsync(int _i, Sound _sound)
        {
            return Task.Run(() => PlaySound(_i, _sound));
        }

        // The sound function
        /// <summary>
        /// The sound for the fight.
        /// </summary>
        /// <param name="_i">Sound is played _i times</param>
        /// <param name="_sound">Sound of monster design</param>
        static void PlaySound(int _i, Sound _sound)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            for (int i = 0; i < _i; i++)
            {
                // sw.Start();
                //  Console.Beep(sound_1[0], sound_1[1]);
                Console.Beep(_sound.ASound[0], _sound.ASound[1]);
                if (_i > 1)
                {
                    Thread.Sleep(sound_1[1]);
                }
                // sw.Stop();
                // CenterText(1,sw.ElapsedMilliseconds.ToString());
            }
        }

        // play it async
        /// <summary>
        /// Play a sound asyncronuos.
        /// </summary>
        /// <param name="_i">Sound id played "_i" times</param>
        /// <param name="_sound">Sound of monster design</param>
        public static async void MakeSomeNoise(int _i, Sound _sound)
        {
            if (_i < 1)
            {
                _i = 1;
            }
            await PlaySoundAsync(_i, _sound);
        }

        #region Play a sound in the background async
        /*
         *      Das spielen von Sound im Hintergrund 
         *      funktioniert in der Console leider nicht.
         *      
         *      Trauriges Smiley
         */


        static ASong backgroundSong = new ASong();



        /// <summary>
        /// The asyncron Task of PlaySong(ASong)
        /// </summary>
        /// <param name="_newSong"></param>
        /// <returns>Delegate of PlaySong()</returns>
        /// <param name="_endless">Set to true for endless noise ;-)</param>
        static Task PlaySongAsync(Sound[] _newSong, bool _endless)
        {
            return Task.Run(() => PlaySong(_newSong, _endless));
        }

        /// <summary>
        /// The PlaySong plays a given song
        /// </summary>
        /// <param name="newSong">The song to play</param>
        /// <param name="_endless">Set to true for endless sound</param>
        static void PlaySong(Sound[] newSong, bool _endless)
        {
            // AutoResetEvent songEvent = new AutoResetEvent(true);
            bool play = _endless;
            while (play)
            {
                int duration = 16;

                // block the threads for the next acustic applepie
                // songEvent.Reset();
                // lock (printlock)
                // {
                for (int i = 0; i < duration; i++)
                {
                    Console.Beep(newSong[i].f, newSong[i].d);
                }
                // }

                // Thread.Sleep(500);
                // songEvent.Set();
                play = playSong;
            }
        }

        public static async void PlayThisSong(Sound[] _song, bool _endless)
        {
            await PlaySongAsync(_song, _endless);
        }
        #endregion

        #region Display the Statistics

        public static Stats playerStats;    //  = new Stats();
        public static Stats enemyStats;     //   = new Stats();

        public static void InitStats()
        {
            playerStats = player.outfit.stats;
            enemyStats = enemy.monster.outfit.stats;
        }

        public static void UpdateStats(Stats _player, Stats _enemy)
        {
            playerStats = _player;
            enemyStats = _enemy;
        }

        public static void PrintStats()
        {
            //  string.Format("{0,20}","---");
            //  string output = String.Format("Text{0,10} text{1,10}", arg1, arg2);
            //  Console.SetCursorPosition(2,1);
            //  Console.Write(output);
            String clear = String.Format("{0,20}", " ");
            int atLine = 1;
            int left = 2;
            int right = 78;
            // int health = player.outfit.stats.HPoints;
            // int otherNumber = 455;
            string playerHealth = String.Format("{0,10}{1,10}", "Health", playerStats.hPoints);
            string playerDefense = String.Format("{0,10}{1,10}", "Defense", playerStats.dPoints);

            string enemyHealth = String.Format("{0,5}{1,15}", enemyStats.GetHPoints(), "Health");
            string enemyDefense = String.Format("{0,5}{1,15}", enemyStats.dPoints, "Defense");
            // lock (statsLock)
            // {

            lock (printlock)
            {

                ConsoleColor backup = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;

                Console.SetCursorPosition(left, atLine);
                Console.Write(clear);
                Console.SetCursorPosition(left, atLine);
                Console.Write(playerHealth);

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.SetCursorPosition(left, atLine + 1);
                Console.Write(clear);
                Console.SetCursorPosition(left, atLine + 1);
                Console.Write(playerDefense);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(right, atLine);
                Console.Write(clear);
                Console.SetCursorPosition(right, atLine);
                Console.Write(enemyHealth);

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.SetCursorPosition(right, atLine + 1);
                Console.Write(clear);
                Console.SetCursorPosition(right, atLine + 1);
                Console.Write(enemyDefense);

                Console.ForegroundColor = backup;
            }
            // }
        }

        public static void PrintStats(Stats _player, Stats _enemy)
        {
            //  string.Format("{0,20}","---");
            //  string output = String.Format("Text{0,10} text{1,10}", arg1, arg2);
            //  Console.SetCursorPosition(2,1);
            //  Console.Write(output);
            String clear = String.Format("{0,20}", " ");
            int atLine = 1;
            int left = 2;
            int right = 78;
            // int health = player.outfit.stats.HPoints;
            // int otherNumber = 455;
            string playerHealth = String.Format("{0,-10}{1,10}", "Health", _player.GetHPoints());
            string playerDefense = String.Format("{0,-10}{1,10}", "Defense", _player.dPoints);
            string playerAttack = String.Format("{0,-10}{1,10}", "Attack", _player.aPoints);

            string enemyHealth = String.Format("{0,-5}{1,15}", _enemy.GetHPoints(), "Health");
            string enemyDefense = String.Format("{0,-5}{1,15}", _enemy.dPoints, "Defense");
            string enemyAttack = String.Format("{0,-5}{1,15}", _enemy.aPoints, "Defense");

            // don't forget to update the Stats objects
            UpdateStats(_player, _enemy);

            lock (printlock)
            {

                ConsoleColor backup = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;

                Console.SetCursorPosition(left, atLine);
                Console.Write(clear);
                Console.SetCursorPosition(left, atLine);
                Console.Write(playerHealth);

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.SetCursorPosition(left, atLine + 1);
                Console.Write(clear);
                Console.SetCursorPosition(left, atLine + 1);
                Console.Write(playerDefense);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(left, atLine + 2);
                Console.Write(clear);
                Console.SetCursorPosition(left, atLine + 2);
                Console.Write(playerAttack);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(right, atLine);
                Console.Write(clear);
                Console.SetCursorPosition(right, atLine);
                Console.Write(enemyHealth);

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.SetCursorPosition(right, atLine + 1);
                Console.Write(clear);
                Console.SetCursorPosition(right, atLine + 1);
                Console.Write(enemyDefense);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(right, atLine + 2);
                Console.Write(clear);
                Console.SetCursorPosition(right, atLine + 2);
                Console.Write(enemyAttack);

                Console.ForegroundColor = backup;
            }
        }
        #endregion

        #region GameThread

        static Thread thePlayer = new Thread(StartPlayer);
        static Thread theEnemy = new Thread(StartEnemy);
        static Thread theSong = new Thread(PlayMySong);

        private static void PlayMySong()
        {
            try
            {
                //  maybe some sound at the end ???
                backgroundSong.InitASong();

                //InitASong();

                // playSong = true;
                PlaySong(backgroundSong.TheSong, playSong);

            }
            catch (ThreadAbortException ex)
            {
                System.Diagnostics.Debug.WriteLine("We killed the song. " + ex);

            }
        }

        #endregion

        static bool playSong = true;

        static void Main(string[] args)
        {
            // console preparation
            InitGame();

            // try printing a welcome
            TextOnScreen hello = new TextOnScreen();
            hello.FillWellcome();
            hello.PrintWelcome(x, y);

            // theSong.Start();

            string blanc = String.Format("{0}", "Press ENTER to start!");
            CenterText(25, blanc, ConsoleColor.Red);
            Console.CursorVisible = false;
            Console.ReadLine();
            // playSong = false;
            // theSong.Join();
            // we print a background in the field
            DrawBackground();

            // Init the Player and Enemy
            InitPlayerAndEnemy();

            // we init the gameStats
            InitStats();

            // we print the dashboard
            PrintDashboard();

            // we print the starting stats
            PrintStats(playerStats, enemyStats);

            // we init the movement choices of the enemy
            InitChoices();

            // hide the cursor
            Console.CursorVisible = false;

            #region backgroundSong
            // test the sound
            // PlaySound(2);

            //  Let's do it...
            //  [1] Sound at the beginning
            //  MakeSomeNoise(2);
            //  [2] Sound in the background
            // wir brauchen erst nen ASong
            // InitASong();
            // PlayThisSong(theBackgroundSong);
            #endregion

            // we start a Countdown Timer
            StartTimer();


            // next time we call it GameLoop
            // PlayTheGame(player);
            // we start a seperate Thread;
            thePlayer.Start();

            theEnemy.Start();


            // The Game is running
            // Relax for 1/2 a second

            Thread.Sleep(500);

            // show a nice cursor;
            // Nope
            // Console.CursorSize = 1;
            // Console.CursorVisible = true;

            // Waiting for the player Thread?
            // No, each will check for health
            // and call Close() to end the game.
            // thePlayer.Join();
            // theEnemy.Join();

            // A chance to Exit and close shell
            // Close();


        }

        private static void StartPlayer()
        {
            try
            {
                PlayTheGame(player);
            }
            catch (ThreadAbortException ex)
            {
                System.Diagnostics.Debug.WriteLine("Catch: StartPlayer " + ex);
            }
        }

        private static void StopPlayer()
        {
            try
            {
                thePlayer.Abort();
            }
            catch (ThreadAbortException ex)
            {
                System.Diagnostics.Debug.WriteLine("We killed the thread " + ex);
            }
            player.HideMonster(player.pos_x, player.pos_y);
            // player.pos_x = 5;
            // player.pos_y = 5;
            // playSong = false;
            Close();
        }

        /// <summary>
        /// The delegate for the enemy thread
        /// </summary>
        private static void StartEnemy()
        {
            // Start the timerbased enemy movement
            try
            {
                StartEnemyTimer(500);

            }
            catch (ThreadAbortException ex)
            {

                System.Diagnostics.Debug.WriteLine("The thread was killed. " + ex);
            }
        }

        private static void StopEnemy()
        {
            try
            {
                theEnemy.Join();
                enemy.monster.HideMonster(enemy.monster.pos_x, enemy.monster.pos_y);

            }
            catch (ThreadAbortException ex)
            {

                System.Diagnostics.Debug.WriteLine("Stop Enemy: " + ex);
            }
            // playSong = false;
            Close();
        }
    }
}
