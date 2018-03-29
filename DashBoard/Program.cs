using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DashBoard
{
    class Program
    {
        #region Alle Variablen werden hier deklariert
        // Set the default screen params
        /// <summary>
        /// Set the console window
        /// <value>width</value>
        /// </summary>
        public static int x = 101;
        /// <summary>
        /// Set the console window
        /// <value>height</value>
        /// </summary>
        public static int y = 30;

        public static int top = 0;



        #endregion



        public static object printlock = new object();

        // declare 3 different types of monster...
        /// <summary>
        /// The "Goble".
        /// <remarks>The anxious monster. It's quick, weak, but resistant</remarks>
        /// </summary>
        static Design goble;
        static Design frodo, angry;
        
        // ...and declare a variable to store them
        /// <summary>
        /// An Array to store the available typ of monster.
        /// </summary>
        static Design[] theDesigns;

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

            goble = new Design {
                designName = "Goble",
                designColor = ConsoleColor.White,
                designElements = new string[] { "(°;°)", " ~▓~ ", " ] [ ", "0-▓-0" },
            };

            frodo = new Design {
                designName = "Frodo",
                designColor = ConsoleColor.Yellow,
                designElements = new string[] { "{O.O}", " /▓\\ ", " { } ", "0-▓-0" },
            };

            angry = new Design {
                designName = "Angry",
                designColor = ConsoleColor.Green,
                designElements = new string[] { "[-.-]", " '▓' ", " U U ", "0-▓-0" },
            };

            theDesigns = new Design[] { goble, frodo, angry };

        }

        /// <summary>
        /// Center the cursor.
        /// </summary>
        static void Center()
        {
            Console.CursorLeft = x / 2;
            Console.CursorTop = y / 2;
        }

        /// <summary>
        /// Closing the game. Display "Press any key...".
        /// </summary>
        static void Close()
        {
            Center();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Press any key ...");
            Console.ReadLine();

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

        /// <summary>
        /// We print the layout of the dashboard.
        /// </summary>
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

        // make a play
        // ! only cursor is moving !
        /// <summary>
        /// Start the game
        /// </summary>
        /// <remarks>Runs until Key.L is pressed</remarks>
        /// <param name="_player">The player monster</param>
        static void Move(Monster _player)
        {

            /* Instantiate a monster;
            
            Monster player = CreatePlayer("(° °)", " ~*~ ", " ] [ ", "The fast Goblin");
            */

            /*  We ceate a monster with an existing design
             *  
             */
            player = _player;

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
                                monsterMove.Dispose();
                                playSong = false;
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
                                    player.Fight();
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

        /* ToDo: create a background struct
        //  to store the colors
        */

        // Draw a random background
        /// <summary>
        /// Draws a background randomly out of given symbols
        /// </summary>
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

        /// <summary>
        /// The countdown timer object
        /// </summary>
        static Timer mytimer;

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
                KillTimer();
            }
        }


        // we clean up the thread
        /// <summary>
        /// Kill the Timer Thread when Key.L ends the game
        /// </summary>
        static void KillTimer()
        {
            mytimer.Dispose();
        }

        /// <summary>
        /// Fire Countdown event every 1000ms
        /// </summary>
        static void StartTimer() {
            mytimer = new Timer(PrintTime, autoEvent, 1000, 1000);

            autoEvent.WaitOne();

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
        static Timer monsterMove;

        /// <summary>
        /// The callback when enemy move event is ready
        /// </summary>
        static AutoResetEvent resetMonsterTimer = new AutoResetEvent(true);

        //  <void> StartEnemyTimer(int _millis);
        /// <summary>
        /// Starts a Timer after 1000ms for monster movement
        /// </summary>
        /// <param name="_millis">Frequency of monster movement</param>
        static void StartEnemyTimer(int _millis)
        {
            monsterMove = new Timer(MoveMonster, resetMonsterTimer, 1000, _millis);
            resetMonsterTimer.WaitOne();
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

            // Check, if move is possible
            int[] move = { 0,0};
            int new_x = 0;
            int new_y = 0;
            int pos_x = enemy.monster.pos_x;
            int pos_y = enemy.monster.pos_y;
            bool moveIsPossible = true;

            while (moveIsPossible)
            {
                move = RandomMove();
                // erst hauen, dann laufen;
                // und nur nebeneinander;
                if (dist.GetDistance() < 5 && dist.diff_y < 4)
                {
                    int r = random.Next(2, 14);
                    if ((r % 2) == 0)
                    {
                    enemy.monster.Fight();
                    }
                }
                new_x = pos_x + move[0];
                new_y = pos_y + move[1];

                // min_x < new_x < max_x
                if ((2 < new_x && new_x < x - 3  ) && (top + 1 < new_y && new_y < y - 2  ))
                {
                    enemy.monster.HideMonster(enemy.monster.pos_x, enemy.monster.pos_y);
                    enemy.monster.pos_x += move[0];
                    enemy.monster.pos_y += move[1];
                    enemy.monster.PrintMonster();

                    //  Print the distance. 
                    //  ToDo -> Distance Printout
                    //  put it into seperate timer
                    dist.PrintTheDist();
                    
                    break;
                }
                else
                {
                    moveIsPossible = true;
                }

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
         */

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

        /// <summary>
        /// Inits a player and an enemy
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
        public static int[] sound_2 = { 1640, 500 };
        public static int[] sound_3 = { 1020, 500 };

        // The async call of the fight sound function
        /// <summary>
        /// The asyncronuos Task of the PlaySound.
        /// </summary>
        /// <remarks>Takes an number for intervals</remarks>
        /// <param name="_i">Sound id played "_i" times</param>
        /// <returns></returns>
        static Task PlaySoundAsync(int _i)
        {
            return Task.Run(() => PlaySound(_i));
        }

        // The sound function
        /// <summary>
        /// The sound for the fight.
        /// </summary>
        /// <param name="_i">Sound is played _i times</param>
        static void PlaySound(int _i)
        {
            //  System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            for (int i = 0; i < _i; i++)
            {
                //  sw.Start();
                Console.Beep(sound_1[0], sound_1[1]);
                if (_i > 0)
                {
                    //  sw.Stop();
                    //  CenterText(1,sw.ElapsedMilliseconds.ToString());
                    Thread.Sleep(sound_1[1]);
                }
            }
        }

        // play it async
        /// <summary>
        /// Play a sound asyncronuos.
        /// </summary>
        /// <param name="_i">Sound id played "_i" times</param>
        public static async void MakeSomeNoise(int _i)
        {
            if (_i < 1)
            {
                _i = 1;
            }
            await PlaySoundAsync(_i);
        }

        #region Play a sound in the background async
        /*
         *      Das spielen von Sound im Hintergrund 
         *      funktioniert in der Console leider nicht.
         *      
         *      Trauriges Smiley
         */ 

        
        public static Note[] theBackgroundSong = new Note[16];

        static Note n_1 = new Note { f = 37, d = 1 };
        static Note n_2 = new Note { f = 37, d = 1 };           
        static Note n_3 = new Note { f = 37, d = 1 };
        static Note n_4 = new Note { f = 37, d = 1 };  

        /// <summary>
        /// Erzeugt einen "Song".
        /// </summary>
        public static void InitASong()
        {

            n_1.f = 1020;
            n_2.f = 2400;
            n_3.f = 1300;
            n_4.f = 1900;

            n_1.d = 500;
            n_2.d = 500;
            n_3.d = 500;
            n_4.d = 1000;

            // Note[]notes = new Note[16];
            theBackgroundSong[0] = n_1;
            theBackgroundSong[1] = n_2;
            theBackgroundSong[2] = n_1;
            theBackgroundSong[3] = n_2;
            theBackgroundSong[4] = n_1;
            theBackgroundSong[5] = n_2;
            theBackgroundSong[6] = n_3;
            theBackgroundSong[7] = n_4;

            theBackgroundSong[8] = n_2;
            theBackgroundSong[9] = n_3;
            theBackgroundSong[10] = n_2;
            theBackgroundSong[11] = n_3;
            theBackgroundSong[12] = n_2;
            theBackgroundSong[13] = n_3;
            theBackgroundSong[14] = n_1;
            theBackgroundSong[15] = n_4;

        }
        public static bool playSong = false;

        /// <summary>
        /// The asyncron version of PlaySong(Song)
        /// </summary>
        /// <param name="_newSong"></param>
        /// <returns></returns>
        static Task PlaySongAsync(Note[] _newSong)
        {
            return Task.Run( () => PlaySong(_newSong) );
        }

        static void PlaySong(Note[] newSong)
        {
            while (playSong)
            {
                int duration = 16;
                for (int i = 0; i < duration; i++)
                {
                    Console.Beep(newSong[i].f, newSong[i].d);
                }

                Thread.Sleep(500);
            }
        }

        public static async void PlayThisSong(Note[] _song)
        {
            await PlaySongAsync(_song);
        }
        #endregion



        static void Main(string[] args)
        {

            InitGame();
            // Init the Player and Enemy
            InitPlayerAndEnemy();

            PrintHead();

            DrawBackground();

            Center();

            // Init the choices
            InitChoices();

            // hide the cursor
            Console.CursorVisible = false;


            // test the sound
            // PlaySound(2);

            //  Let's do it...
            //  [1] Sound at the beginning
                //  MakeSomeNoise(2);
            //  [2] Sound in the background
                // wir brauchen erst nen Song
            InitASong();
            PlayThisSong(theBackgroundSong);
            
            // start Timer
            StartTimer();


            // Start the timerbased enemy movement
            StartEnemyTimer(1500);
            //  StartEnemyTimer(500);

            // next time we call it GameLoop
            Move(player);

            // Relax for 1/2 a second
            Thread.Sleep(500);

            // show a nice cursor;
            Console.CursorSize = 1;
            Console.CursorVisible = true;

            // A chance to Exit and close shell
            Close();
            
        }
    }
}
