using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DashBoard
{
    struct Distance
    {
        public int distance;
        public String text;
        public int diff_x;
        public int diff_y;

        public void PrintTheDist(int _dist)
        {
            text = String.Format("Actual Distance:{0,4}", _dist);
            //if (Program.enemyStats.GetHPoints() <= 0 && Program.playerStats.GetHPoints() <= 0)
            //{
            //    // blanc it out because game is over
            //    text = String.Format("{0,30}" , " ");
            //}
            Program.CenterText(3, text);
        }
        public void PrintTheDist()
        {
            CalcDistance();
            text = String.Format("{0,-21}{1,5:D2}","Actual Distance", distance);
            //if (Program.enemyStats.GetHPoints() <= 0 && Program.playerStats.GetHPoints() <= 0)
            //{
            //    // blanc it out because game is over
            //    text = String.Format("{0,30}", " ");
            //}
            Program.CenterText(3, text);
        }
        void CalcDistance()
        {
            diff_x = Math.Abs((int)(Program.player.pos_x - Program.enemy.monster.pos_x));
            diff_y = Math.Abs((int)(Program.player.pos_y - Program.enemy.monster.pos_y));

            distance = (int)Math.Sqrt((double)(diff_x * diff_x + diff_y * diff_y));
        }
        public void SetDistance(int _dist)
        {
            distance = _dist;
        }
        public int GetDistance()
        {
            CalcDistance();
            return distance;
        }

        public int Distancetest
        {
            get
            {
                CalcDistance();
                return distance;
            }
            private set
            {
                distance = value;
            }
        }
    }
}
