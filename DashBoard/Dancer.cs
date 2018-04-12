using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    struct Dancer
    {
        public Enemy isEnemy;

        public void InitDancer(Design design, string title)
        {
            isEnemy = new Enemy();
            isEnemy.CreateEnemyFromDesign(design, title, true);
            isEnemy.monster.parts = design.designElements;
        }
    }
}
