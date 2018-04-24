using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    class PlayerMod
    {
        private static int min = 10;
        private static int max = 60;
        private int attack = 0;
        private int defense = 0;
        private int speed = 0;

        private string modText = String.Format("Enter value from {0} to {1}: ", Min, Max);

        public int Attack
        {
            get => attack;
            //private set => attack = value;
        }

        private void SetAttack(int _value)
        {
            if (_value >= min && _value <= max)
            {
                attack = _value;
            }
            else
            {
                attack = min;
            }
        }

        public void SetAttack()
        {
            SetAttack(InputRequest.PrintModRequest("New Attack:  " + modText));
        }

        public int Defense
        {
            get => defense;
            private set
            {
                if (value >= min && value <= max)
                {
                    defense = value;
                }
                else
                {
                    defense = min;
                }

            }
        }

        public void SetDefense()
        {
            Defense = InputRequest.PrintModRequest("New Defense:  " + modText);
        }

        private const int speedFactor = 250 ;
        /*
         * 60 / 10 * 200 = 1200;
         * 60 / 60 * 200 = 200;
         * 60 / 10 * 100 = 600;    
         * 60 / 60 * 100 = 100            */

        public int Speed
        {
            get => speed;
            set {
                if (value >= min && value <= max)
                {
                    speed = max/value * speedFactor;
                }
                else
                {
                    speed = max/min * speedFactor;
                }

            }
        }
        public void SetSpeed()
        {
            Speed = InputRequest.PrintModRequest("New Speed:  " + modText);
        }

        public static int Min { get => min; set => min = value; }
        public static int Max { get => max; set => max = value; }

    }
}
