using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MonsterHunter
{
    struct Enemy
    {
        // Enemy ist auch ein Monster
        public Monster monster;
        int[] position;

        // 
        public void CreateEnemyFromDesign(Design _design, string _name)
        {
            position = Monster.RandomStartPos();
            monster = new Monster
            {
                outfit = _design,
                name = _name,
                pos_x = position[0], // Program.x / 3,
                pos_y = position[1], // (Program.y + Program.top) / 2,

            };

            // return monster;
        }

        public void CreateEnemyFromDesign(Design _design, string _name, bool _everywhere)
        {
            position = Monster.RandomStartPos(_everywhere);
            monster = new Monster
            {
                outfit = _design,
                name = _name,
                pos_x = position[0], // Program.x / 3,
                pos_y = position[1], // (Program.y + Program.top) / 2,

            };

            // return monster;
        }

        public void CreateEnemyFromOponent()
        {
            position = Monster.RandomStartPos();
            monster = Enemy.CreateEnemy(Game.player);
        }

        //  The Enemy Timer objects;
        /// <summary>
        /// The Timer object for enemy movement
        /// </summary>
        public static Timer enemyTimer;

        /// <summary>
        /// The callback when enemy move event is ready
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
        /// The delegate for the enemy thread
        /// </summary>
        public static void StartEnemy()
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

        public static void StopEnemy()
        {
            try
            {
                // Program.theEnemy.Join();
                Game.enemy.monster.HideMonster(Game.enemy.monster.pos_x, Game.enemy.monster.pos_y);
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
        /// Create an enemy different from Player
        /// </summary>
        /// <remarks>Set enemy start position to random position</remarks>
        /// <param name="_player">The player monster</param>
        /// <returns>Returns a monster different than parameter</returns>
        public static Monster CreateEnemy(Monster _player)
        {
            //  get the design of player
            //  and select a different one
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





    }
}
