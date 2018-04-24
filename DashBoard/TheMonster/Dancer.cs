using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    struct Dancer
    {
        public Enemy asDancer;

        public void InitDancer(Design design, string title)
        {
            asDancer = new Enemy(design, title, true);
            // asDancer.CreateEnemyFromDesign(design, title, true);
            // asDancer.monster.parts = design.designElements;
        }
    }
}
