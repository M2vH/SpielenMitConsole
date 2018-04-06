using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace MonsterHunter
{
    struct TextOnScreen
    {
        /// <summary>
        /// Path to the file.txt
        /// </summary>
        private static string path = "Welcome.txt";

        private static string[] fileContent;

        private static int size = 0;

        /// <summary>
        /// Holds the symbols to be printed instead of spaces in ASCII
        /// </summary>
        private static char[] fill = new char[] { '.', ':', ',', ' ', ' ' };

        /// <summary>
        /// The list with all lines
        /// </summary>
        private List<string> lines;

        public string Path { get => path; set => path = value; }
        public char[] Fill { get => fill; set => fill = value; }
        public List<string> Lines { get => lines; }

        /// <summary>
        /// Fills the lines marked with '-' in Welcome.txt with spaces
        /// </summary>
        public void FillTheList()
        {
            fileContent = File.ReadAllLines(path);
            lines = new List<string>();

            size = 0;
            foreach (string line in fileContent)
            {
                if (size < line.Length)
                {
                    size = line.Length;
                }

                if (line.StartsWith("-"))
                {
                    lines.Add(String.Format("{0," + size + "}", " "));
                }
                else
                {
                    lines.Add(line);
                }
            }
        }

        /// <summary>
        /// Fills the list with content from given path/to/file.txt
        /// </summary>
        /// <param name="_path">the path/to/file.txt</param>
        public void FillTheList(string _path)
        {
            path = _path;
            FillTheList();
        }
        /// <summary>
        /// Fills the lines marked with '-' with spaces.
        /// </summary>
        /// <param name="_welcome">The text from a file</param>
        public void FillTheList(string[] _welcome)
        {
            fileContent = _welcome;
            FillTheList();

        }

        /// <summary>
        /// Print the text in the middle of the screen.
        /// </summary>
        /// <param name="_x">x-size of the screen</param>
        /// <param name="_y">size of the screeny-</param>
        public void PrintColorBackground(int _x, int _y)
        {
            char storage = '#';

            // make some color on the screen
            for (int i = 0; i < _x; i++)
            {
                for (int j = 0; j < _y - 1; j++)
                {
                    Symbol symbol = new Symbol();
                    Console.SetCursorPosition(i, j);
                    Console.ForegroundColor = (ConsoleColor)Game.random.Next(1, 7);
                    storage = Fill[Game.random.Next(0, 4)];
                    Console.Write(storage);
                    symbol.Sign = storage;
                    symbol.Color = (int)Console.ForegroundColor;
                    Background.signs[i, j] = symbol;

                }
            }


        }

        public void PrintStringBackground(int _x, int _y)
        {
            // make some color on the screen
            for (int i = 0; i < _y - 1; i++)
            {
                for (int j = 0; j < _x; j++)
                {
                    // Console.SetCursorPosition(i, j);
                    Console.ForegroundColor = (ConsoleColor)Game.random.Next(1, 7);

                    Console.Write(Fill[Game.random.Next(0, 4)]);

                }
                Console.WriteLine();
            }


        }

        /// <summary>
        /// Print the ASCII text at given position
        /// </summary>
        /// <param name="_lines"></param>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        public void PrintASCII(List<string> _lines, int _x, int _y)
        {
            Fill = new char[] { '.', ':', ',', ' ', ' ' };
            int new_x = (_x - size) / 2;
            // all line in field - ( welcome lines / 2 )
            int new_y = _y - Lines.Count - Lines.Count / 2;

            int x = new_x;
            int y = new_y;

            // print the ASCII text
            foreach (string line in _lines)
            {
                char[] symbols = new char[line.Length];
                symbols = line.ToCharArray();

                foreach (char symbol in symbols)
                {
                    Console.SetCursorPosition(x++, y);
                    Console.ForegroundColor = (ConsoleColor)Game.random.Next(1, 7);

                    if (symbol == ' ')
                    {
                        Console.Write(Fill[Game.random.Next(0, 5)]);
                    }
                    else
                    {
                        ConsoleColor store = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(symbol);
                        Console.ForegroundColor = store;
                    }
                }
                x = new_x;
                y++;
            }

        }

        /// <summary>
        /// Prints a 'Press ENTER to start!'
        /// <remarks>...and waits for keyboard.</remarks>
        /// </summary>
        public void PrintEnter()
        {
            string blanc = String.Format("{0}", "Press ENTER to start!");
            Dashboard.CenterText(25, blanc, ConsoleColor.Red);
            Console.CursorVisible = false;
            Console.ReadLine();

        }

        public void PrintEnter(string _input)
        {
            string blanc = String.Format("{0}", _input);
            Dashboard.CenterText(25, blanc, ConsoleColor.Red);
            Console.CursorVisible = false;

            // store the selection
            while (!Game.choiceIsMade)
            {
                ConsoleKey key = Console.ReadKey().Key;
                if ((key == ConsoleKey.A || key == ConsoleKey.F) || key == ConsoleKey.G)
                {
                    Game.choosenPlayer = key;
                    Game.choiceIsMade = true;
                }

            }
        }

        public void PrintEnter(string _input, Timer[] timer)
        {
            string blanc = String.Format("{0}", _input);
            Dashboard.CenterText(25, blanc, ConsoleColor.Red);
            Console.CursorVisible = false;

            // store the selection
            while (!Game.choiceIsMade)
            {
                ConsoleKey key = Console.ReadKey().Key;
                if ((key == ConsoleKey.A || key == ConsoleKey.F) || key == ConsoleKey.G)
                {
                    Game.choosenPlayer = key;
                    Game.choiceIsMade = true;
                }

            }
            // Console.ReadLine();
            for (int i = 0; i < timer.Length; i++)
            {
                timer[i].Dispose();
            }
        }

        public void PrintStart()
        {
            FillTheList();
            //Console.Clear();
            PrintColorBackground(Window.x, Window.y);
            PrintASCII(Lines, Window.x, Window.y);
            PrintEnter();
        }

        public void PrintText(string _path)
        {
            FillTheList(_path);
            //Console.Clear();
            PrintColorBackground(Window.x, Window.y);
            PrintASCII(lines, Window.x, Window.y);
            PrintEnter();
        }

        public void PrintText(string _path, string _text)
        {
            FillTheList(_path);
            //Console.Clear();
            PrintColorBackground(Window.x, Window.y);
            PrintASCII(lines, Window.x, Window.y);
            PrintEnter(_text);
        }


        //static Timer dance;
        //static Timer dance2;
        //static Timer dance3;

        public void PrintText(string _path, string _text, Monster _monsters)
        {
            FillTheList(_path);
            //Console.Clear();
            PrintColorBackground(Window.x, Window.y);
            PrintASCII(lines, Window.x, Window.y);

            int[] coords = Monster.RandomStartPos(true);

            AutoResetEvent danceReset = new AutoResetEvent(true);
            var dance = new Timer(_monsters.DanceTheMonster, danceReset, 1000, 500);
            danceReset.Set();
            PrintEnter(_text, new Timer[] { dance});
        }

        public void PrintText(string _path, string _text, Monster m1, Monster m2, Monster m3)
        {
            FillTheList(_path);
            //Console.Clear();
            PrintColorBackground(Window.x, Window.y);
            PrintASCII(lines, Window.x, Window.y);

            AutoResetEvent danceReset = new AutoResetEvent(true);
            var dance = new Timer(m1.DanceTheMonster, danceReset, 1000, 500);
            danceReset.Set();

            Thread.Sleep(2000);

            AutoResetEvent danceReset2 = new AutoResetEvent(true);
            var dance2 = new Timer(m2.DanceTheMonster, danceReset2, 2000, 800);
            danceReset2.Set();

            Thread.Sleep(2000);

            AutoResetEvent danceReset3 = new AutoResetEvent(true);
            var dance3 = new Timer(m3.DanceTheMonster, danceReset3, 3000, 1100);
            danceReset3.Set();

            Thread.Sleep(2000);

            PrintEnter(_text, new Timer[]{ dance, dance2, dance3 });
        }

        public void PrintGameOver()
        {
            TextOnScreen gameOver = new TextOnScreen();
            gameOver.PrintText("GameOver.txt");

        }

        public void PrintGameOver(string _text)
        {
            TextOnScreen gameOver = new TextOnScreen();
            gameOver.PrintText("GameOver.txt", _text);

        }
    }
}
