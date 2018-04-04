using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MonsterHunter
{
    
struct Game
    {
        public static Enemy enemy;
        public static Monster player;
        public static Monster winner;

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
            Dashboard.CenterText(2, clear);

            // print the Countdown in the center of our dashboard
            Dashboard.CenterText(2, timeText);

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
        public static void KillCountdown()
        {
            countdown.Dispose();
        }

        /// <summary>
        /// Start Timer Countdown event every 1000ms
        /// </summary>
        public static void StartCountdown()
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
        #region Display the Statistics

        public static Stats playerStats;    //  = new Stats();
        public static Stats enemyStats;     //   = new Stats();

        public static void InitStats()
        {
            playerStats = Game.player.outfit.stats;
            enemyStats = Game.enemy.monster.outfit.stats;
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

            lock (Program.printlock)
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

            lock (Program.printlock)
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
        /// <summary>
        /// Closing the game. Display "Press any key...".
        /// </summary>
        public static void CloseTheGame()
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
                Program.enemyTimer.Dispose();
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

                // enemy stops moving when winner;
                // slow down the enemy movement
                // enemyTimer.Change(0, 1500);
            }


            int here = Program.y - 5 - Program.top / 2;
            lock (Program.printlock)
            {
                Song.playSong = false;
                ConsoleColor red = ConsoleColor.Red;
                Thread.Sleep(500);
                string grats = "The Winner is... " + winner.name;
                Dashboard.CenterText(here, grats, red);
                Dashboard.CenterText(++here, "Press ENTER to close game ", red);
                Thread.Sleep(2000);
                Console.ReadLine();
            }

        }



    }
}
