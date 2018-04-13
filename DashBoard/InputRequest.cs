using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MonsterHunter
{
    struct InputRequest
    {
        /// <summary>
        /// Prints a 'Press ENTER to start!'
        /// <remarks>...and waits for keyboard.</remarks>
        /// </summary>
        public static ConsoleKey PrintInputRequest(ConsoleKey _key)
        {
            List<ConsoleKey> list = new List<ConsoleKey>();
            list.Add(_key);
            string blanc = String.Format("{0}", "Press ENTER to start!");
            return PrintInputRequest(blanc, list);
        }

        public static ConsoleKey PrintInputRequest(List<ConsoleKey> _keys)
        {
            string blanc = String.Format("{0}", "Press ENTER to start!");
            return PrintInputRequest(blanc, _keys);
        }
        public static ConsoleKey PrintInputRequest(string _input, List<ConsoleKey> _keys)
        {
            Timer[] empty = new Timer[0];
            return PrintInputRequest(_input, empty, _keys);
        }

        public static ConsoleKey PrintInputRequest(string _input, ConsoleKey _key)
        {
            List<ConsoleKey> _keys = new List<ConsoleKey>();
            _keys.Add(_key);
            Timer[] empty = new Timer[0];
            return PrintInputRequest(_input, empty, _keys);
        }

        public static ConsoleKey PrintInputRequest(string _input, Timer[] timer, List<ConsoleKey> _keys)
        {
            string blanc = String.Format("{0}", _input);
            Dashboard.CenterText(25, blanc, ConsoleColor.Red);
            Console.CursorVisible = false;
            Game.choiceIsMade = false;
            ConsoleKey key = new ConsoleKey();
            // store the selection
            while (!Game.choiceIsMade)
            {
                key = Console.ReadKey().Key;
                if ( _keys.Contains(key) )
                {
                    Game.keyPressed = key;
                    Game.choiceIsMade = true;
                }

            }
            // Console.ReadLine();
            for (int i = 0; i < timer.Length; i++)
            {
                timer[i].Dispose();
            }

            return key;
        }

        public static int PrintModRequest(string _input)
        {
            // 'blanc' the input area
            string blanc = new string(' ', 50);
            Dashboard.CenterText(25, blanc, ConsoleColor.Black);

            // The request
            string input = String.Format("{0}", _input);
            Dashboard.CenterText(25, input, ConsoleColor.White);
            Console.CursorVisible = true;
            Console.ForegroundColor = ConsoleColor.White;
            int theResult = 0;
            // bool isValid = true;
            while (!(int.TryParse(Console.ReadLine(), out theResult)))
            {
            }
            Console.CursorVisible = false;

            return theResult;
        }


    }
}
