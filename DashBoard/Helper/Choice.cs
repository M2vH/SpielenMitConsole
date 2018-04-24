using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    struct Choice
    {
        public int[] coord;

        // make a choice
        public static Choice[] goTo = new Choice[5];

        public static void InitChoices()
        {
            goTo[0] = new Choice { coord = Direction.to.up };              //  1,2
            goTo[1] = new Choice { coord = Direction.to.right };           //  3,4,5,6,7,8
            goTo[2] = new Choice { coord = Direction.to.left };           //  9,10,11,12,13,14,15,16
            goTo[3] = new Choice { coord = Direction.to.down };            //  17,18
            goTo[4] = new Choice { coord = Direction.to.stay };            //  19
        }


    }
}
