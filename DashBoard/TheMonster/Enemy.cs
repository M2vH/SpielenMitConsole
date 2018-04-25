using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MonsterHunter
{
    class Enemy : Monster
    {
        // Enemy is child of monster
        // public Monster monster;
        int[] randomPosition;

        // 

        public Enemy(Design _design, string _name, bool _everywhere)
        {
            this.CreateEnemyFromDesign(_design, _name, _everywhere);
        }

        public void CreateEnemyFromDesign(Design _design, string _name, bool _everywhere)
        {
            randomPosition = RandomStartPos(_everywhere);
            //monster = new Monster
            //{
            outfit = _design; // ,
            name = _name;   //,
            pos_x = randomPosition[0];  //, // Program.x / 3,
            pos_y = randomPosition[1];  //, // (Program.y + Program.top) / 2,
            parts = _design.designElements;
            //};

            // return monster;
        }

        //public void CreateEnemyFromOponent()
        //{
        //    randomPosition = RandomStartPos();
        //    monster = CreateEnemy(Game.player);
        //}

        //  The Enemy Timer objects;
        /// <summary>
        /// The Timer object for asDancer movement
        /// </summary>
        public static Timer enemyTimer;

        /// <summary>
        /// The callback when asDancer move event is ready
        /// </summary>
        static AutoResetEvent resetEnemyTimer = new AutoResetEvent(true);

        //  <void> StartEnemyTimer(int _millis);
        /// <summary>
        /// Starts a Timer after 1000ms for monster movement.
        /// <remarks>1500 is really slow, 200 is maximum quick</remarks>
        /// </summary>
        /// <param name="_millis">Frequency of monster movement</param>
        public static void StartEnemyTimer(int _millis)
        {

            try
            {
                enemyTimer = new Timer(Monster.MoveMonster, resetEnemyTimer, 1000, _millis);
                resetEnemyTimer.WaitOne();

            }
            catch (ThreadAbortException ex)
            {

                System.Diagnostics.Debug.WriteLine("Catch: StartEnemyTimer " + ex);
            }
        }

        /// <summary>
        /// The delegate for the asDancer thread
        /// </summary>
        public static void StartEnemy()
        {
            // Start the timerbased asDancer movement
            try
            {
                // Enemy.StartEnemyTimer(500);
                Enemy.StartEnemyTimer(Game.enemy.outfit.stats.sPoints);

            }
            catch (ThreadAbortException ex)
            {

                System.Diagnostics.Debug.WriteLine("The thread was killed. " + ex);
            }
        }

        public static void StopEnemy()
        {
            try
            {
                // Program.theEnemyThread.Join();
                Game.enemy.HideMonster(Game.enemy.pos_x, Game.enemy.pos_y);
                Game.keepAlive = false;

            }
            catch (ThreadAbortException ex)
            {

                System.Diagnostics.Debug.WriteLine("Stop Enemy: " + ex);
            }
            // playSong = false;
            // Game.CloseTheGame();
        }

        /// <summary>
        /// Create an asDancer different from Player
        /// </summary>
        /// <remarks>Set asDancer start randomPosition to random randomPosition</remarks>
        /// <param name="_player">The player monster</param>
        /// <returns>Returns a monster different than parameter</returns>
        public static Monster CreateEnemy(Player _player)
        {
            //  get the design of player
            //  and select a different one
            int id = 0;
            Design enemyDesign = Game.theDesigns[id];
            while (enemyDesign.designName == _player.outfit.designName)
            {
                id = Game.rndm.Next(Game.theDesigns.Length - 1);
                enemyDesign = Game.theDesigns[id];
            }

            // store a random start randomPosition
            int[] here = RandomStartPos();
            Monster enemy = Player.CreateMonster(enemyDesign, here[0], here[1], "The incredible " + enemyDesign.designName);
            return enemy;
        }

        public Enemy(Player _player)
        {
            //  get the design of player
            //  and select a different one
            int id = 0;
            Design enemyDesign = Game.theDesigns[id];
            while (enemyDesign.designName == _player.outfit.designName)
            {
                id = Game.rndm.Next(Game.theDesigns.Length - 1);
                enemyDesign = Game.theDesigns[id];
            }

            this.outfit = enemyDesign;

            // store a random start randomPosition
            int[] here = RandomStartPos();
            this.pos_x = here[0];
            this.pos_y = here[1];
            this.name = "The incredible " + enemyDesign.designName;

        }



    }
}
