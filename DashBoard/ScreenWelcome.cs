using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    class ScreenWelcome
    {
        public static void Show()
        {
            if (Program.showWelcome)
            {
                TextOnScreen.PrintStart();
                InputRequest.PrintInputRequest(ConsoleKey.Enter);
            }


        }
    }
}
