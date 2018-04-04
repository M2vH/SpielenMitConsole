using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MonsterHunter
{
    struct GameTools
    {
        /*  The Timer
 *  We run a Timer and print a countdown in its own thread.
 */

        /// <summary>
        /// The countdown timer object
        /// </summary>
        static Timer countdown;

        // We call this function when the timer thread callback is ready
        static AutoResetEvent autoEvent = new AutoResetEvent(true);

        //  we count every timer callback call
        //  init invokeCount;
        /// <summary>
        /// Object to store how often Countdowntimer calls.
        /// </summary>
        static int invokeCount = 0;

        // we want to run for given amount of seconds;
        /// <summary>
        /// The maximum Countdown Time in seconds
        /// </summary>
        static int maxCount = 120;

        // we store countdown here;
        // and init remaining time with maximum seconds
        /// <summary>
        /// Object to store the extant time
        /// </summary>
        static int remainTime = maxCount;

        // the string we want to print
        /// <summary>
        /// The string in the printout of the Countdown
        /// </summary>
        static String timeText;

        // this function is the callback for the timer
        /// <summary>
        /// The callback function for countdown timer
        /// </summary>
        /// <remarks>Prints the Countdown string</remarks>
        /// <param name="stateInfo">The Timer Event handle</param>
        static void PrintTime(Object stateInfo)
        {
            // yes, we can
            ++invokeCount;
            --remainTime;

            timeText = String.Format("{0,-16}{1,5} : {2}", "Time remaining",
                                        arg1: (remainTime / 60).ToString(),
                                        arg2: (remainTime % 60).ToString("D2"));

            // Clear the dashboard with Key.Space
            string clear = String.Format("{0,50}", " ");
            Dashboard.CenterText(2, clear);

            // print the Countdown in the center of our dashboard
            Dashboard.CenterText(2, timeText);

            // We send signals to waiting threads
            AutoResetEvent secondAuto = (AutoResetEvent)stateInfo;

            if (invokeCount == maxCount)
            {
                secondAuto.Set();
                KillCountdown();
            }
        }


        // we clean up the thread
        /// <summary>
        /// Kill the Timer Thread when Key.L ends the game
        /// </summary>
        public static void KillCountdown()
        {
            countdown.Dispose();
        }

        /// <summary>
        /// Start Timer Countdown event every 1000ms
        /// </summary>
        public static void StartCountdown()
        {
            try
            {
                countdown = new Timer(PrintTime, autoEvent, 2000, 1000);
                autoEvent.WaitOne();
            }
            catch (ThreadAbortException ex)
            {
                System.Diagnostics.Debug.WriteLine("Catch: StartTimer " + ex);
            }

        }

    }
}
