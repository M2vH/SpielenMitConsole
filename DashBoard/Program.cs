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


        static void Main(string[] args)
        {
            // console preparation
            Game.InitGame();

            // hide the cursor
            Console.CursorVisible = false;

            // we init the movement goTo of the enemy
            Choice.InitChoices();

            // Printing a welcome
            TextOnScreen hello = new TextOnScreen();
            hello.PrintStart();


            // Create a Monster from Design
            Design goble = Game.goble;
            Monster choice_1 = new Monster();
            int[] pos_1 = Monster.RandomStartPos(true);
            choice_1.outfit = goble;
            choice_1.parts = choice_1.outfit.designElements;
            choice_1.name = "[G]oble";
            choice_1.pos_x = pos_1[0];
            choice_1.pos_y = pos_1[1];

            TextOnScreen gameOver = new TextOnScreen();
            gameOver.PrintText("Choose.txt", "Type the KEY and press ENTER ", choice_1);



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
            thePlayer.Start();

            theEnemy.Start();


            // The Game is running
            // Relax for 1/2 a second

            Thread.Sleep(500);

        }

    }
}
