using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashBoard
{
    class Program
    {
        // Set default screen params
        static int x = 101;
        static int y = 30;

        // set screen to default
        static void InitGame()
        {
            Console.Clear();
            Console.BufferWidth = x;
            Console.BufferHeight = y;
            Console.WindowWidth = x;
            Console.WindowHeight = y;

        }

        // move cursor into center
        static void Center()
        {
            Console.CursorLeft = x / 2;
            Console.CursorTop = y / 2;
        }

        // close the game
        static void Close()
        {
            Console.WriteLine("Press any key ...");
            Console.ReadLine();
        }

        // The Game
        static void Main(string[] args)
        {
            InitGame();
            Center();



            Close();
        }
    }
}
