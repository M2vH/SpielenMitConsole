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
        public void CreateEnemyFromOponent()
        {
            position = Monster.RandomStartPos();
            monster = Program.CreateEnemy(Game.player);
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




    }
}
