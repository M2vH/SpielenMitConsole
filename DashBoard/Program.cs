﻿using System;
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
         * Keeping the Main() as clean as possible
         * 
         */
        #region GameThreads

        public static Thread thePlayerThread = new Thread(Player.StartPlayer);
        public static Thread theEnemyThread = new Thread(Enemy.StartEnemy);
        // public static Thread theSong = new Thread(Song.PlayMySong);


        #endregion

        /// <summary>
        /// Set to true, if player is keyboard controlled
        /// </summary>
        public static bool arrowControl;
        // Printing a welcome
        // set next bool to false to hide welcome during debugging
        public static bool showWelcome = true;


        static void Main(string[] args)
        {

#if MANUAL
        arrowControl = true;
#endif

            // Init the game
            // ToDo: make Game a class;
            Game.InitGame();

            // hide the cursor
            Console.CursorVisible = false;

            // we init the movement goTos for the enemy.asDancer
            Choice.InitChoices();

#if TEST

            // display the "M" and the 3 monster
            // to make a cool Screenshot for an icon;
            ScreenIcon.Show();

#endif
            
            // say Hello to the player
            ScreenWelcome.Show();

            // let the player choose his monster
            // and make user input possible
            ScreenChoose.Show();

            // Game is now ready for the action;
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
            // we start seperate Threads;
            theEnemyThread.Name = "EnemyThread";
            theEnemyThread.Start();

            thePlayerThread.Name = "PlayerThread";
            thePlayerThread.Start();

            // The Game is running
            // Wait for the threads to finish
            // Added a return in the functions called by threads.

            // Thread.Join() doesnt work, 
            // theEnemyThread.Join();
            // thePlayerThread.Join();

            // cause threads start timer threads
            // and AutoResetEvent gives us time back;
            while (Game.keepAlive)
            {
                // we keep the main thread alive
            }

            Game.CloseTheGame();
        }

    }
}
