using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CallingEvents
{
    /*  we create the EventArgs.
    *  This Info is send every time
    *  the Event happens. 
    *  The monster won't need it.
    */

    //public class TimerTickedEventArgs : EventArgs
    //{
    //    public int tickCount;

    //    // The constructor
    //    public TimerTickedEventArgs (int _tickCount)
    //    {
    //        tickCount += _tickCount;
    //    }
    //}


    class Program
    {
        // The Timer objects;
        static Timer monsterMove;
        // we move forever
        static int countdown = 0;
        static int durchlauf = 0;
        static AutoResetEvent resetTimer = new AutoResetEvent(true);
        
        //  <void> StartTimer(int _millis);
        static void StartTimer(int _millis)
        {
            monsterMove = new Timer(MoveMonster, resetTimer, 1000, _millis);
            resetTimer.WaitOne();
        }
        
        
        //  the Timer CallBack Funktion
        //  This function is called,
        //  when Timer ticks happen.
        static void MoveMonster(Object _stateInfo)
        {
            AutoResetEvent resetEvent = (AutoResetEvent)_stateInfo;
            ++countdown;
            Console.WriteLine(countdown);
            if (countdown == 9)
            {
                ++durchlauf;
                countdown = 0;
                resetEvent.Set();
                if (durchlauf > 3)
                {
                    monsterMove.Dispose();
                }
            }
        }

        static void Main(string[] args)
        {
            StartTimer(500);
            Console.ReadLine();
        }
    }
}
