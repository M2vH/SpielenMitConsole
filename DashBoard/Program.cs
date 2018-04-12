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
        public static bool manual = true;


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

            bool skipWelcome = false;

            if (!skipWelcome)
            {
                TextOnScreen hello = new TextOnScreen();
                hello.PrintStart();

            }

            TextOnScreen message = new TextOnScreen();

            message.PrintText("Choose.txt", "", true);


            // theSong.Start();

            // playSong = false;
            // theSong.Join();
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
