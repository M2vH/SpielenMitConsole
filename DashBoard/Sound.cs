using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashBoard
{
    /// <summary>
    /// Enthält Frequenz und Dauer.
    /// </summary>
    /// <remarks>Beide Parameter müssen vom Typ int sein.</remarks>
    struct Sound
    {
        /// <summary>
        /// The frequency.
        /// </summary>
        public int f;
        /// <summary>
        /// The duration of the note.
        /// </summary>
        public int d;

        public int[] ASound
        {
            get { return ASound; }
            set { ASound = value; }
        }
        /// <summary>
        /// Create a note
        /// </summary>
        /// <param name="_f">frequency</param>
        /// <param name="_d">duration</param>
        public void SetNote(int _f, int _d)
        {
            f = _f;
            d = _d;
        }



    }
}
