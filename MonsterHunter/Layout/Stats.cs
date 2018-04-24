using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    /// <summary>
    /// Holds the Statistics of a Monster
    /// </summary>
    struct Stats
    {
        // Health Points    hPoints
        // Attack Points    aPoints
        // Defense Points   dPoints
        // Speed Points     sPoints     // controlles the movement speed

            /// <summary>
            /// Speed points.
            /// <remarks>Converts into move every millisecond</remarks>
            /// </summary>
        public int sPoints;
            
        /// <summary>
        /// Health Points
        /// </summary>
        public int hPoints;

        /// <summary>
        /// Reduce health points
        /// </summary>
        /// <param name="_damage">amount of healthpoints to reduce</param>
        public void SetHPoints(int _damage)
        {
            int absoluteDamage = Math.Abs(_damage);
            if (hPoints < absoluteDamage)
            {
                hPoints = 0;
            }
            else
            {
                hPoints -= absoluteDamage;

#if DEBUG
                System.Diagnostics.Debug.WriteLine("absolute Damage:  -" + absoluteDamage + " healthpoints.");
#endif
            }
        }
        public int GetHPoints()
        {
            return hPoints;
        }


        /// <summary>
        /// Attack Points
        /// </summary>
        public int aPoints;

        //public int APoints { get => aPoints; set => aPoints = value; }

        /// <summary>
        /// The Strength of the Defense
        /// </summary>
        public int dPoints;

        //public int DPoints { get => dPoints; set => dPoints = value; }
    }
}
