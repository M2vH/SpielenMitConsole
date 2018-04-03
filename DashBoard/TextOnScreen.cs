using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MonsterHunter
{
    struct TextOnScreen
    {
        private string path;

        private char[] fill;

        private List<string> lines;

        private int[] coords;

        private int size;

        public string Path { get => path; set => path = value; }
        public char[] Fill { get => fill; set => fill = value; }
        public List<string> Lines { get => lines;  }
        public int Size { get => size; set => size = value; }

        public void FillWellcome()
        {
            path = "Welcome.txt";
            string[] welcome = File.ReadAllLines(path);

            lines = new List<string>();

            Size = 0;
            foreach (string line in welcome)
            {
                if (Size < line.Length)
                {
                    Size = line.Length;
                }

                if (line.StartsWith("-"))
                {
                    lines.Add(String.Format("{0," + Size + "}"," "));
                }
                else
                {
                    lines.Add(line);
                }
            }
        }

        public void FillWellcome(string[] _welcome)
        {
            string[]welcome = _welcome;

            lines = new List<string>();

            int size = 0;
            foreach (string line in welcome)
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

        public void PrintWelcome(int _x, int _y)
        {
            Fill = new char[]{ '.',':',',',' ',' '};
            int new_x = (_x - Size) / 2;
            // all line in field - ( welcome lines / 2 )
            int new_y = _y - Lines.Count-Lines.Count/2;

            int x = new_x;
            int y = new_y;

            Random random = new Random();
            coords = new int[] { 0, 0 };

            coords[0] = new_x;
            coords[1] = new_y;

            // make some color on the screen
            for (int i = 0; i < _x; i++)
            {
                for (int j = 0; j < _y; j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.ForegroundColor = (ConsoleColor)random.Next(1, 7);
                    Console.Write(Fill[random.Next(0,4)]);

                }
            }

            foreach(string line in lines)
            {
                char[] symbols = new char[line.Length];
                symbols = line.ToCharArray();

                foreach (char symbol in symbols)
                {
                    Console.SetCursorPosition(x++, y);
                    Console.ForegroundColor = (ConsoleColor)random.Next(1,7);

                    if (symbol == ' ')
                    {
                        Console.Write(Fill[random.Next(0,5)]);
                    }
                    else
                    {
                        ConsoleColor store = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(symbol);
                        Console.ForegroundColor = store;
                    }
                }
                x = new_x;
                y++;
            }
        }
    }
}
