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
                FightSound = Sound.low ,
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
                FightSound = Sound.mid ,
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
                FightSound = Sound.high,
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





        /* --> The Game  <--
         * 
         * Keep the Main() as clean as possible
         * 
         */



        #region GameThreads

        public static Thread thePlayer = new Thread(Player.StartPlayer);
        public static Thread theEnemy = new Thread(Enemy.StartEnemy);
        public static Thread theSong = new Thread(Song.PlayMySong);


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

        }

    }
}
