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
            Console.WindowWidth = Window.x;
            Console.WindowHeight = Window.y;
            Console.BufferWidth = Window.x;
            Console.BufferHeight = Window.y;
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

            Game.goble = new Design
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

            Game.frodo = new Design
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

            Game.angry = new Design
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

            Game.theDesigns = new Design[] { Game.goble, Game.frodo, Game.angry };

        }



        /// <summary>
        /// Center the cursor into center of field.
        /// </summary>
        static void Center()
        {
            Console.CursorLeft = Window.x / 2;
            Console.CursorTop = Window.y / 2;
        }


       
        // ToDo: Question? Where are the CreateMonster functions placed best?



        /* 
        * The Background
        * ToDo: create struct with int x, int y, background {symbol, foregroundColor}
        */

        // var to store the background in
        public static string[] background = new string[Window.y - 1];

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

            // we to from line top to window.height ( -1 ? )
            for (int i = Window.top; i < Window.y - 1; i++)
            {
                string one_line = "";
                // we to from left to right
                for (int j = 0; j < Window.x; j++)
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
            Design enemyDesign = Game.theDesigns[id];
            while (enemyDesign.designName == _player.outfit.designName)
            {
                id = Game.random.Next(Game.theDesigns.Length - 1);
                enemyDesign = Game.theDesigns[id];
            }

            // store a random start position
            int[] here = Monster.RandomStartPos();
            Monster enemy = Player.CreatePlayer(enemyDesign, here[0], here[1], "The incredible " + enemyDesign.designName);
            return enemy;
        }

        /*  
         *  The Monster Movement Timing
         */




        /* Init Player and Enemy
         * Player comes in random outfit
         * and Enemy is different then Player
         */

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



        #region GameThread

        static Thread thePlayer = new Thread(Player.StartPlayer);
        static Thread theEnemy = new Thread(StartEnemy);
        static Thread theSong = new Thread(Song.PlayMySong);


        #endregion


        static void Main(string[] args)
        {
            // console preparation
            InitGame();

            // try printing a welcome
            TextOnScreen hello = new TextOnScreen();
            hello.FillWellcome();
            hello.PrintWelcome(Window.x, Window.y);

            // theSong.Start();

            string blanc = String.Format("{0}", "Press ENTER to start!");
            Dashboard.CenterText(25, blanc, ConsoleColor.Red);
            Console.CursorVisible = false;
            Console.ReadLine();
            // playSong = false;
            // theSong.Join();
            // we print a background in the field
            DrawBackground();

            // Init the Player and Enemy
            Game.InitPlayerAndEnemy();

            // we init the gameStats
            Game.InitStats();

            // we print the dashboard
            Dashboard.PrintDashboard();

            // we print the starting stats
            Game.PrintStats(Game.playerStats, Game.enemyStats);

            // we init the movement goTo of the enemy
            Choice.InitChoices();

            // hide the cursor
            Console.CursorVisible = false;

            #region backgroundSong
            // test the sound
            // PlaySound(2);

            //  Let's do it...
            //  [1] Sound at the beginning
            //  MakeSomeNoise(2);
            //  [2] Sound in the background
            // wir brauchen erst nen Song
            // InitASong();
            // PlayThisSong(theBackgroundSong);
            #endregion

            // we start a Countdown Timer
            Game.StartCountdown();


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
            // and call CloseTheGame() to end the game.
            // thePlayer.Join();
            // theEnemy.Join();

            // A chance to Exit and close shell
            // CloseTheGame();


        }

        /// <summary>
        /// The delegate for the enemy thread
        /// </summary>
        private static void StartEnemy()
        {
            // Start the timerbased enemy movement
            try
            {
                Enemy.StartEnemyTimer(500);

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
                Game.enemy.monster.HideMonster(Game.enemy.monster.pos_x, Game.enemy.monster.pos_y);

            }
            catch (ThreadAbortException ex)
            {

                System.Diagnostics.Debug.WriteLine("Stop Enemy: " + ex);
            }
            // playSong = false;
            Game.CloseTheGame();
        }
    }
}
