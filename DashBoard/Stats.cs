using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashBoard
{
    /// <summary>
    /// Holds the Statistics of a Monster
    /// </summary>
    struct Stats
    {
        // Health Points    hPoints
        // Attack Points    aPoints

        // Defense Points   dPoints
        // Speed
        /// <summary>
        /// Health Points
        /// </summary>
        public int hPoints;
        public void SetHPoints(int _damage)
        {
            hPoints -= _damage;
        }
        public int GetHPoints()
        {
            return hPoints;
        }

        // public int HPoints { get => hPoints; set => hPoints = value; }
        //public int HPoints
        //{
        //    get
        //    {
        //        return hPoints;
        //    }
        //    set
        //    {
        //        hPoints = value;
        //    }
        //}


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
