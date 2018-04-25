using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    class ScreenIcon
    {
        public static void Show()
        {
            TextOnScreen.PrintText("TextFiles/Icon.txt");

            Player player1 = new Player(Game.frodo, "Frodo");
            Player player2 = new Player(Game.angry, "Frodo");
            Player player3 = new Player(Game.goble, "Frodo");

            player1.PrintMonster(40, 12);
            player2.PrintMonster(44, 15);
            player3.PrintMonster(39, 17);


            InputRequest.PrintInputRequest(ConsoleKey.Enter);
        }
    }
}
