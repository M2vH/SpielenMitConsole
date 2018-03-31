using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    /// <summary>
    /// A struct to generate a song.
    /// </summary>
    struct ASong
    {
        public Sound[] song;

        static Sound n_1;
        static Sound n_2;
        static Sound n_3;
        static Sound n_4;

        public Sound[] TheSong { get => song; }

        /// <summary>
        /// Erzeugt einen "Song".
        /// </summary>
        public void InitASong()
        {
            Sound[] theInitSong = new Sound[16];

            n_1 = new Sound { f = 37, d = 1 };
            n_2 = new Sound { f = 37, d = 1 };
            n_3 = new Sound { f = 37, d = 1 };
            n_4 = new Sound { f = 37, d = 1 };

            n_1.f = 600;
            n_2.f = 800;
            n_3.f = 400;
            n_4.f = 1200;

            n_1.d = 200;
            n_2.d = 200;
            n_3.d = 200;
            n_4.d = 300;

            // Note[]notes = new Note[16];
            theInitSong[0] = n_1;
            theInitSong[1] = n_2;
            theInitSong[2] = n_1;
            theInitSong[3] = n_2;
            theInitSong[4] = n_1;
            theInitSong[5] = n_2;
            theInitSong[6] = n_3;
            theInitSong[7] = n_4;

            theInitSong[8] = n_2;
            theInitSong[9] = n_3;
            theInitSong[10] = n_2;
            theInitSong[11] = n_3;
            theInitSong[12] = n_2;
            theInitSong[13] = n_3;
            theInitSong[14] = n_1;
            theInitSong[15] = n_4;

            song = theInitSong;

        }
    }
}
