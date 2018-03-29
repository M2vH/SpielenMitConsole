using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DashBoard
{
    /// <summary>
    /// A struct that stores the design.
    /// </summary>
    struct Design
    {
        /// <summary>
        /// Takes the name of the "design".
        /// <remarks>Use short names ;-)</remarks>
        /// </summary>
        public string designName;

        /// <summary>
        /// Takes the single parts of the monter.
        /// <remarks>Fill the first 3 with head to toe.</remarks>
        /// </summary>
        public string[] designElements;
        
        ///<summary>
        /// <value>The color of the monster.</value>
        /// <remarks>Must be a clear bright ConsoleColor</remarks>
        ///</summary>
        public ConsoleColor designColor;

        /// <summary>
        /// The monster spezific sound
        /// </summary>
        public Sound FightSound {
            get { return FightSound; }
            set { FightSound = value; }
        }
    }
}