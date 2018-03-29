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
        int hPoints;

        public int HPoints
        {
            get
            {
                return hPoints;
            }
            set
            {
                hPoints = value;
            }
        }


        /// <summary>
        /// Attack Points
        /// </summary>
        int aPoints;

        public int APoints { get => aPoints; set => aPoints = value; }

        /// <summary>
        /// The Strength of the Defense
        /// </summary>
        int dPoints;

        /// <summary>
        /// Public get; set; int dPoints
        /// </summary>
        public int DPoints { get => dPoints; set => dPoints = value; }


    }
}
