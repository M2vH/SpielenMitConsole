using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    /// <summary>
    /// Holds the static parameter of the console window.
    /// </summary>
    struct Window
    {
        // Set the default screen params
        /// <summary>
        /// The console window
        /// <value>width</value>
        /// </summary>
        public const int x = 101;
        /// <summary>
        /// The console window
        /// <value>height</value>
        /// </summary>
        public const int y = 30;

        /// <summary>
        /// The top y-coordinate of the field
        /// </summary>
        /// <remarks>Field is window (y) - dashboard. Value increases when dashboard is printed</remarks>
        public static int top = 0;

    }
}
