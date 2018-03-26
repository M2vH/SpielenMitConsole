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
        public int f;
        public int d;
        // int[] sound;

        //public int[] GetNote()
        //{
        //    sound = new int[] { f, d };
        //    return sound;
        //}

        public void SetNote(int _f, int _d)
        {
            f = _f;
            d = _d;
        }


    }
}
