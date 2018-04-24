using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonsterHunter
{
    class Player : Monster
    {
        // A constructor
        public Player(Design _design, string _name)
        {
            outfit = _design;
            name = _name;
            pos_x = Window.x / 3;
            pos_y = (Window.y + Window.top) / 2;
            parts = _design.designElements;
        }



        /*  <Monster> CreateMonster(Design _design, int _x, int _y, string _name)
         *  Creates a Monster at a given randomPosition;
         */
        /// <summary>
        /// Create a Monster out of a given Design at a given position.
        /// </summary>
        /// <param name="_design">The Design</param>
        /// <param name="_x">The x-Coordinate of the position</param>
        /// <param name="_y">The y-Coordinate of the position</param>
        /// <param name="_name">The name displayed at the end of the game.</param>
        /// <returns></returns>
        public static Monster CreateMonster(Design _design, int _x, int _y, string _name)
        {
            Monster monster = new Monster
            {
                outfit = _design,
                name = _name,
                pos_x = _x,
                pos_y = _y,
            };
            monster.parts = _design.designElements;
            return monster;
        }

        public static void StartPlayer()
        {
            try
            {
                if (Program.manual)
                {
                    Game.PlayTheGame(Game.player);

                }
                else
                {
                    //else let the computer control the player player
                    // set start randomPosition to random in left half of field;
                    Game.player.pos_x = Game.random.Next(15, (Window.x / 2 - 5));
                    Game.player.pos_y = Game.random.Next(Window.top + 5, (Window.y - 5));

                    //// Test:
                    // Game.PlayThePlayer(Game.player); // works, but overloaded
                    // Game.PlayThePlayer();    // works, but leeks of Timer
                    // Game.StartAutoPlayerTimer(500);
                    Game.StartAutoPlayerTimer(Game.player.outfit.stats.sPoints);

                }
            }
            catch (ThreadAbortException ex)
            {
                System.Diagnostics.Debug.WriteLine("Catch: StartPlayer " + ex);
            }
        }

        public static void StopPlayer()
        {
            try
            {
                // thePlayer.Abort();
                Game.player.HideMonster(Game.player.pos_x, Game.player.pos_y);
                Game.keepAlive = false;
                // player.pos_x = 5;
                // player.pos_y = 5;
                // playSong = false;
                // Game.CloseTheGame();
            }
            catch (ThreadAbortException ex)
            {
                System.Diagnostics.Debug.WriteLine("We killed the thread " + ex);
            }
        }


    }
}
