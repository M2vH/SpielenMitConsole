using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MonsterHunter
{
    /// <summary>
    /// The object to handle the user choice and input.
    /// </summary>
    class ScreenChoose
    {
        /// <summary>
        /// Displays the 'Choose Monster' screen 
        /// <remarks>...and handles the user input.</remarks>
        /// </summary>
        public static void Show()
        {
            // Display "Choose"-Screen with dancing monster;
            Timer[] timer = TextOnScreen.PrintText("TextFiles/Choose.txt", "", true);
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
                    TextOnScreen.PrintText("TextFiles/theAngro.txt");
                    break;
                case ConsoleKey.F:
                    TextOnScreen.PrintText("TextFiles/theFrodo.txt");
                    break;
                case ConsoleKey.G:
                    TextOnScreen.PrintText("TextFiles/theGoble.txt");
                    break;
                default:
                    TextOnScreen.PrintText("TextFiles/RandomGame.txt");
                    Thread.Sleep(4000);
                    break;

            }

            if (Game.isNotRandom)
            {

                keys = new List<ConsoleKey>{ConsoleKey.M, ConsoleKey.D };
                Game.modInput = InputRequest.PrintInputRequest("[M] - Mod the player, [D] - Use Default values", keys);

                if (Game.modInput == ConsoleKey.M)
                {

                    // Use the input values
                    Game.modifications.SetAttack();
                    Game.modifications.SetDefense();
                    Game.modifications.SetSpeed();

                    // tell the game we have modifications
                    Game.isModyfied = true;

                }

            }


        }
    }
}
