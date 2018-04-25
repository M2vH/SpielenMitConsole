using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter.Screens
{
    class ScreenIcon
    {
        public static void Show()
        {
            TextOnScreen.PrintText("TextFiles/Icon.txt");

            Player player = new Player(Game.frodo, "Frodo");
            player.PrintMonster(30, 10);


            InputRequest.PrintInputRequest(ConsoleKey.Enter);
        }
    }
}
