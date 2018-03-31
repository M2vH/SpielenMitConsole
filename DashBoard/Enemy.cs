using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            position = Program.RandomStartPos();
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
            position = Program.RandomStartPos();
            monster = Program.CreateEnemy(Program.player);
        }

        
    }
}
