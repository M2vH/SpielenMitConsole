using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonsterHunter
{
    struct Player
    {
        /// <summary>
        /// Create a monster out of parts as parameter 
        /// <remarks>Actual max size is 5 symbols.</remarks>
        /// </summary>
        /// <param name="_head">The Head of the monster</param>
        /// <param name="_body">The Body of the monster</param>
        /// <param name="_legs">The Legs of the monster</param>
        /// <param name="_name">The Name. Displayed if winner</param>
        /// <returns></returns>
        public static Monster CreatePlayer(string _head, string _body, string _legs, string _name)
        {
            Monster player = new Monster
            {
                parts = new string[3],
                name = _name,
                pos_x = Window.x / 3,
                pos_y = (Window.y + Window.top) / 2,
            };
            player.parts[0] = _head;
            player.parts[1] = _body;
            player.parts[2] = _legs;

            return player;
        }

        /*  Function <Monster>CreatePlayer(Design _design, string _name)
         *  Creates a gamer monster with a given <Design> at default position
         */
        /// <summary>
        /// Create a Monster out of a Design
        /// </summary>
        /// <param name="_design">The Design</param>
        /// <param name="_name">The name</param>
        /// <returns></returns>
        public static Monster CreatePlayer(Design _design, string _name)
        {
            Monster monster = new Monster
            {
                outfit = _design,
                name = _name,
                pos_x = Window.x / 3,
                pos_y = (Window.y + Window.top) / 2,

            };
            return monster;
        }

        /*  <Monster> CreatePlayer(Design _design, int _x, int _y, string _name)
         *  Creates a Monster at a given position;
         */
        /// <summary>
        /// Create a Monster out of a given Design at a given position.
        /// </summary>
        /// <param name="_design">The Design</param>
        /// <param name="_x">The x-Coordinate of the position</param>
        /// <param name="_y">The y-Coordinate of the position</param>
        /// <param name="_name">The name displayed at the end of the game.</param>
        /// <returns></returns>
        public static Monster CreatePlayer(Design _design, int _x, int _y, string _name)
        {
            Monster monster = new Monster
            {
                outfit = _design,
                name = _name,
                pos_x = _x,
                pos_y = _y,
            };
            return monster;
        }

        public static void StartPlayer()
        {
            bool manual = true;
            try
            {
                  //ToDo:
                  //if (manual) the PlayTheGame()
                
                if (manual)
                {
                    // Game.PlayTheGame(Game.player);
                    Game.PlayThePlayer(Game.player);

                }
                else
                {
                  //else let the computer control the player monster

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
                // player.pos_x = 5;
                // player.pos_y = 5;
                // playSong = false;
                Game.CloseTheGame();
            }
            catch (ThreadAbortException ex)
            {
                System.Diagnostics.Debug.WriteLine("We killed the thread " + ex);
            }
        }



    }
}
