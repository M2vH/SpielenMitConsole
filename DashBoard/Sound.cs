using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MonsterHunter
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
        public static int f = 0;
        /// <summary>
        /// The duration of the note.
        /// </summary>
        public static int d = 0;

        public int Freq()
        {
            return f;
        }

        public int Dur()
        {
            return d;
        }

        public Sound(int _f, int _d)
        {
            f = _f;
            d = _d;
        }

        public Sound GetSound()
        {
            return new Sound(f, d);
        }

        public int[] GetSoundArray()
        {
            return new int[] { f, d };
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

        /*  The sound machine
 *  Implementation of async fighting sound.
 *  ToDo: let each monster sound different.
 */
        public static Sound low = new Sound(300,300);
        public static Sound mid = new Sound(900, 300);
        public static Sound high = new Sound(1500, 300);

        public static int F { get => f; set => f = value; }
        public static int D { get => d; set => d = value; }

        // The async call of the fight sound function
        /// <summary>
        /// The asyncronuos Task of the PlaySound.
        /// </summary>
        /// <remarks>Takes an number for intervals</remarks>
        /// <param name="_i">Sound id played "_i" times</param>
        /// <param name="_sound">Sound of monster design</param>
        /// <returns></returns>
        static Task PlaySoundAsync(int _i, Sound _sound)
        {
            return Task.Run(() => PlaySound(_i, _sound));
        }

        // The sound function
        /// <summary>
        /// The sound for the fight.
        /// </summary>
        /// <param name="_i">Sound is played _i times</param>
        /// <param name="_sound">Sound of monster design</param>
        public static void PlaySound(int _i, Sound _sound)
        {
            //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            //lock (Game.printlock)
            //{
                for (int i = 0; i < _i; i++)
                {
                    // sw.Start();
                    //  Console.Beep(low[0], low[1]);
                    Console.Beep(_sound.GetSoundArray()[0], _sound.GetSoundArray()[1]);
                    if (_i > 1)
                    {
                        Thread.Sleep(_sound.GetSoundArray()[1]);
                    }
                    // sw.Stop();
                    // CenterText(1,sw.ElapsedMilliseconds.ToString());
                }
            //}
        }

        // play it async
        /// <summary>
        /// Play a sound asyncronuos.
        /// </summary>
        /// <param name="_i">Sound id played "_i" times</param>
        /// <param name="_sound">Sound of monster design</param>
        public static async void MakeSomeNoise(int _i, Sound _sound)
        {
            if (_i < 1)
            {
                _i = 1;
            }
            await PlaySoundAsync(_i, _sound);
        }



    }
}
