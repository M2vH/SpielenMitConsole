using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    struct Dancer
    {
        public Random rndm;
        public Monster dancer_1;
        public Monster dancer_2;
        public Monster dancer_3;

        public void InitDancer()
        {
            rndm = new Random();
            dancer_1 = new Monster
            {
                outfit = Game.goble,
                name = "[ G ]",
                // pos_x = Monster.RandomStartPos(true)[0],
                // pos_y = Monster.RandomStartPos(true)[1],

                pos_x = rndm.Next(2, 98),
                pos_y = Game.random.Next(8, 25),

                //pos_x = 25,
                //pos_y = 15,
            };
            dancer_1.parts = Game.goble.designElements;

            dancer_2 = new Monster
            {
                outfit = Game.frodo,
                name = "[ F ]",
                parts = Game.frodo.designElements,
                // pos_x = Monster.RandomStartPos(true)[0],
                // pos_y = Monster.RandomStartPos(true)[1],
                pos_x = 50,
                pos_y = 15,
            };
            dancer_3 = new Monster
            {
                outfit = Game.angry,
                name = "[ A ]",
                parts = Game.angry.designElements,
                //pos_x = Monster.RandomStartPos(true)[0],
                //pos_y = Monster.RandomStartPos(true)[1],
                pos_x = 75,
                pos_y = 15,
            };

            theDancer = new Monster[]{ dancer_1, dancer_2,dancer_3 };
        }

        public Monster[] theDancer;

    }
}
