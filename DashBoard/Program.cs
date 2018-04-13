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
        /* --> The Game  <--
         * 
         * Keep the Main() as clean as possible
         * 
         */
        #region GameThreads

        public static Thread thePlayer = new Thread(Player.StartPlayer);
        public static Thread theEnemy = new Thread(Enemy.StartEnemy);
        // public static Thread theSong = new Thread(Song.PlayMySong);


        #endregion

        /// <summary>
        /// Set to true, if player is keyboard controlled
        /// </summary>
        public static bool manual = false;


        static void Main(string[] args)
        {

            // console preparation
            //Dancer theDancer = new Dancer();
            //theDancer.InitDancer();


            Game.InitGame();

            // hide the cursor
            Console.CursorVisible = false;

            // we init the movement goTo of the isEnemy
            Choice.InitChoices();

            // Printing a welcome
            // set next to false to hide welcome during debugging
            bool quickCheck = false;

            if (!quickCheck)
            {
                TextOnScreen.PrintStart();
                InputRequest.PrintInputRequest(ConsoleKey.Enter);
            }

            // Display "Choose"-Screen with dancing monster;
            Timer[] timer = TextOnScreen.PrintText("Choose.txt", "", true);
            // Request the key input;
            // create the List of excepted keys;
            List<ConsoleKey> keys = new List<ConsoleKey>
            {
                ConsoleKey.G,
                ConsoleKey.A,
                ConsoleKey.F
                ,ConsoleKey.Spacebar // keep this for random gameplay
            };
            // ...and request the input
            Game.choosenPlayer =
            InputRequest.PrintInputRequest("", timer, keys);

            // Start Player mod menue
            switch (Game.choosenPlayer)
            {
                case ConsoleKey.A:
                    TextOnScreen.PrintText("theAngro.txt");
                    break;
                case ConsoleKey.F:
                    TextOnScreen.PrintText("theFrodo.txt");
                    break;
                case ConsoleKey.G:
                    TextOnScreen.PrintText("theGoble.txt");
                    break;
                default:
                    TextOnScreen.PrintText("RandomGame.txt");
                    Thread.Sleep(4000);
                    break;

            }

            if (Game.isNotRandom)
            {

                keys = new List<ConsoleKey>
            {
                ConsoleKey.M,
                ConsoleKey.D
            };
                Game.modInput = InputRequest.PrintInputRequest("[M] - Mod the player, [D] - Default values", keys);

                if (Game.modInput == ConsoleKey.M)
                {
                    string modAttack = "Enter value from 0 to 100:  ";
                    // we mod the player
                    // store the input
                    int attack = -1;
                    int defense = -1;
                    int speed;
                    attack = InputRequest.PrintModRequest("Attack - " + modAttack);
                    defense = InputRequest.PrintModRequest("Defense - " + modAttack);
                    speed = InputRequest.PrintModRequest("Speed - " + modAttack);





                    Console.ReadLine();
                }

            }






            // we print a background in the field
            Background.DrawInMagenta();

            // Init the Player and Enemy
            Game.InitPlayerAndEnemy();

            // we init the gameStats
            Game.InitStats();

            // we print the dashboard
            Dashboard.PrintDashboard();

            // we print the starting stats
            Game.PrintStats(Game.playerStats, Game.enemyStats);

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
            theEnemy.Name = "EnemyThread";
            theEnemy.Start();

            thePlayer.Name = "PlayerThread";
            thePlayer.Start();

            // Player.StartPlayer();
            // theEnemy.Join();
            // thePlayer.Join();

            // The Game is running
            // Wait for the threads to finish
            // ToDo: Add a return in the functions called by threads.
            // thePlayer.Join();
            while (Game.keepAlive)
            {

            }
            theEnemy.Join();
            thePlayer.Join();

            Game.CloseTheGame();
        }

    }
}
