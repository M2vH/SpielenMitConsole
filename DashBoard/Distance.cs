using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MonsterHunter
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
            Dashboard.CenterText(3, text);
        }
        public void PrintTheDist()
        {
            CalcDistance_xy();
            text = String.Format("{0,-21}{1,5:D2}","Actual Distance", distance);
            //if (Program.enemyStats.GetHPoints() <= 0 && Program.playerStats.GetHPoints() <= 0)
            //{
            //    // blanc it out because game is over
            //    text = String.Format("{0,30}", " ");
            //}
            Dashboard.CenterText(3, text);
        }
        public int CalcDistance_xy()
        {
            diff_x = Math.Abs((int)(Game.player.pos_x - Game.enemy.pos_x));
            diff_y = Math.Abs((int)(Game.player.pos_y - Game.enemy.pos_y));

            distance = (int)Math.Sqrt((double)(diff_x * diff_x + diff_y * diff_y));
            return distance;
        }

        public int CalcDistance_y()
        {
            int diff_y = 0;
            diff_y = Math.Abs((int)(Game.player.pos_y - Game.enemy.pos_y));

            return diff_y;
        }

        int CalcDistance_xy(int[] me, int[] him)
        {
            int diff_x = Math.Abs((int)(me[0] - him[0]));
            int diff_y = Math.Abs((int)(me[1] - him[1]));

            return (int)Math.Sqrt((double)(diff_x * diff_x + diff_y * diff_y));
        }

        public void SetDistance(int _dist)
        {
            distance = _dist;
        }

        public int GetDistance()
        {
            CalcDistance_xy();
            return distance;
        }

        public int GetDistance(int[] me, int[] him)
        {
            return CalcDistance_xy(me,him);
        }

        public int Distancetest
        {
            get
            {
                CalcDistance_xy();
                return distance;
            }
            private set
            {
                distance = value;
            }
        }
    }
}
