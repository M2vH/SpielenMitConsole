using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MonsterHunter
{
    /// <summary>
    /// A struct to generate a song.
    /// </summary>
    struct Song
    {
        public static bool playSong = true;

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
            n_3.f = 1600;
            n_4.f = 1200;

            n_1.d = 200;
            n_2.d = 200;
            n_3.d = 200;
            n_4.d = 400;

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

        #region Play a sound in the background async
        /*
         *      Das spielen von Sound im Hintergrund 
         *      funktioniert in der Console leider nicht.
         *      
         *      Trauriges Smiley
         */


        static Song backgroundSong = new Song();



        /// <summary>
        /// The asyncron Task of PlaySong(Song)
        /// </summary>
        /// <param name="_newSong"></param>
        /// <returns>Delegate of PlaySong()</returns>
        /// <param name="_endless">Set to true for endless noise ;-)</param>
        static Task PlaySongAsync(Sound[] _newSong, bool _endless)
        {
            return Task.Run(() => PlaySong(_newSong, _endless));
        }

        /// <summary>
        /// The PlaySong plays a given song
        /// </summary>
        /// <param name="newSong">The song to play</param>
        /// <param name="_endless">Set to true for endless sound</param>
        static void PlaySong(Sound[] newSong, bool _endless)
        {
            // AutoResetEvent songEvent = new AutoResetEvent(true);
            bool play = _endless;
            while (play)
            {
                int duration = 16;

                // block the threads for the next acustic applepie
                // songEvent.Reset();
                // lock (printlock)
                // {
                for (int i = 0; i < duration; i++)
                {
                    Console.Beep(newSong[i].f, newSong[i].d);
                }
                // }

                // Thread.Sleep(500);
                // songEvent.Set();
                play = Song.playSong;
            }
        }

        public static async void PlayThisSong(Sound[] _song, bool _endless)
        {
            await PlaySongAsync(_song, _endless);
        }
        #endregion

        public static void PlayMySong()
        {
            try
            {
                //  maybe some sound at the end ???
                backgroundSong.InitASong();

                //InitASong();

                // playSong = true;
                PlaySong(backgroundSong.TheSong, playSong);

            }
            catch (ThreadAbortException ex)
            {
                System.Diagnostics.Debug.WriteLine("We killed the song. " + ex);

            }
        }


    }
}
