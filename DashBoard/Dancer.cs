using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    struct Dancer
    {
        public Enemy enemy;

        public void InitDancer(Design design, string title)
        {
            enemy = new Enemy();
            enemy.CreateEnemyFromDesign(design, title, true);
            enemy.monster.parts = design.designElements;
        }
    }
}
