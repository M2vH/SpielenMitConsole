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
    struct Note
    {
        /// <summary>
        /// The frequency.
        /// </summary>
        public int f;
        /// <summary>
        /// The duration of the note.
        /// </summary>
        public int d;
        // int[] sound;

        //public int[] GetNote()
        //{
        //    sound = new int[] { f, d };
        //    return sound;
        //}

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
