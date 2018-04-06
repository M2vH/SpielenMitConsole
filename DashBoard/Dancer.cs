using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    struct Dancer
    {
        public static Monster dancer_1;
        public static Monster dancer_2;
        public static Monster dancer_3;

        public static void InitDancer()
        {

            dancer_1 = new Monster()
            {
                outfit = Game.goble,
                name = "[ G ]",
                parts = Game.goble.designElements,
                pos_x = Monster.RandomStartPos(true)[0],
                pos_y = Monster.RandomStartPos(true)[1],
            };
            dancer_2 = new Monster()
            {
                outfit = Game.frodo,
                name = "[ F ]",
                parts = Game.frodo.designElements,
                pos_x = Monster.RandomStartPos(true)[0],
                pos_y = Monster.RandomStartPos(true)[1],
            };
            dancer_3 = new Monster()
            {
                outfit = Game.angry,
                name = "[ A ]",
                parts = Game.angry.designElements,
                pos_x = Monster.RandomStartPos(true)[0],
                pos_y = Monster.RandomStartPos(true)[1],
            };

            theDancer = new Monster[]{ dancer_1, dancer_2,dancer_3 };
        }

        public static Monster[] theDancer;

    }
}
