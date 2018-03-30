using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DashBoard
{
    struct Monster
    {
        // store top left corner of monster
        /// <summary>
        /// The x of top left coordinates of the monster print.
        /// </summary>
        /// <remarks>Is relative to monster position</remarks>
        int start_x;
        /// <summary>
        /// The y of top left coordinates of the monster print.
        /// </summary>
        /// <remarks>Is relative to monster position</remarks>
        int start_y;

        // store actual position of monster
        /// <summary>
        /// Store actual x position
        /// </summary>
        public int pos_x;
        /// <summary>
        /// Store actual x position
        /// </summary>
        public int pos_y;

        // store name to personalize monster
        /// <summary>
        /// String to store the name of the monster
        /// </summary>
        public string name;

        // store the symbols to draw the monster 
        /// <summary>
        /// Holds the parts of the monster.
        /// </summary>
        /// <remarks>Element[0] for the head, [4] for the fighting</remarks>
        public string[] parts;

        /// <summary>
        /// Contains the monster design
        /// </summary>
        public Design outfit;

        // keine Veränderung;
        // public Stats stats;
        
        //  print a monster at pos x,y
        //  lock the printing of a Monster 
        /// <summary>
        /// Print the monster at position
        /// </summary>
        /// <param name="x">x-coordinate of monster center</param>
        /// <param name="y">y-coordinate of monster center</param>
        public void PrintMonster(int x, int y)
        {
            // store aktual position of monster
            pos_x = x;
            pos_y = y;
            // set cursor to top left corner of monster
            start_x = x - 2;
            start_y = y - 1;

            parts = outfit.designElements;

            // [0] set background color;
            //      store default color;
            ConsoleColor color_backup = Console.ForegroundColor;

            lock (Program.printlock)
            {
                // if outfit.designColor is set, (it's not Black)
                // set ForegroundColor to outfit.designColor;

                if (outfit.designColor != ConsoleColor.Black)
                {
                    Console.ForegroundColor = outfit.designColor;
                }
                
                // [1] printing the head
                //      set cursor to start_x, start_y
                Console.SetCursorPosition(start_x, start_y);
                //      write the head
                Console.Write(parts[0]);

                // [2] printing the arms
                //      set cursor
                Console.SetCursorPosition(Console.CursorLeft - parts[0].Length, Console.CursorTop + 1);
                //      print arms
                Console.Write(parts[1]);

                // [3] printing the legs
                //      set cursor
                Console.SetCursorPosition(Console.CursorLeft - parts[0].Length, Console.CursorTop + 1);
                //      print legs
                Console.Write(parts[2]);

                // [4] set cursor position back to params
                Console.SetCursorPosition(pos_x, pos_y);

                // [5] set cursorColor back
                Console.ForegroundColor = color_backup;
            }  // end of lock

        }

        // print a monster and return true when printed;
        /// <summary>
        /// Print the monster at stored position
        /// </summary>
        /// <returns></returns>
        public bool PrintMonster()
        {
            bool feedback = false;
            if (pos_x != 0 && pos_y != 0)
            {
                PrintMonster(pos_x, pos_y);
                return !feedback;
            }
            else return feedback;
        }

        //  Hide Monster
            //  recover the background
            //  lock the printing
        /// <summary>
        /// Hide the monster
        /// </summary>
        /// <remarks>Hide the monster by re-printing background at given position.
        /// Locks the Thread while printing.</remarks>
        /// <param name="x">x-coordinate of monster position</param>
        /// <param name="y">y-coordinate of monster position</param>
        public void HideMonster(int x, int y)
        {
            // set cursor to top left corner of monster
            start_x = x - 2;
            start_y = y - 1;

            string monster_string = "";

            // get the chars out of the background array
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    // build the string
                    // get the corresponding char
                    monster_string += Program.background[start_y + j][start_x + i];
                }
                // add the delimeter;
                // we use it like a newline later
                monster_string += "#";
            }

            // put it in the old monster printing function
            string parts = monster_string;

            //string parts = "     " + "#" +   // we use # to split string
            //                 "     " + "#" +   // 
            //                 "     " + "#";

            var blanc_bg = parts.Split('#');
            // set position of cursor after every printed part of monster
            // params are the middle of the monster
            // Set cursor to background color

            // [0] set background color;
            //      store default color;
            ConsoleColor color_backup = Console.ForegroundColor;

            lock (Program.printlock)
            {
                // set a new ForegroundColor
                Console.ForegroundColor = Program.gameBackground;
                
                // [1] printing the head
                Console.SetCursorPosition(pos_x - 2, pos_y - 1);
                Console.Write(blanc_bg[0]);

                // [2] printing the arms
                //      set cursor
                Console.SetCursorPosition(Console.CursorLeft - blanc_bg[0].Length, Console.CursorTop + 1);
                //      print arms
                Console.Write(blanc_bg[1]);

                // [3] printing the legs
                //      set cursor
                Console.SetCursorPosition(Console.CursorLeft - blanc_bg[0].Length, Console.CursorTop + 1);
                //      print legs
                Console.Write(blanc_bg[2]);

                // [4] set cursor position back to params
                Console.SetCursorPosition(pos_x, pos_y);

                // [5] set CursorColor back to default
                Console.ForegroundColor = color_backup;
            }
        }

        //  let it fight
        //  switch parts[3] and parts[1]
        /// <summary>
        /// Prints the "fighting" part of monster
        /// </summary>
        /// <remarks>Calls the fight sound function</remarks>

        #region old Fight()
            //  public void Fight()
            //  {
            //      lock (Program.printlock)
            //      {
            //          Program.MakeSomeNoise(1);
            //          string store = parts[1];
            //          parts[1] = parts[3];
            //          PrintMonster();
            //          parts[1] = store;
            //          // display fight for 50ms
            //          Thread.Sleep(50);
            //          HideMonster(pos_x,pos_y);
            //          PrintMonster();
            //      }
            //  }
        #endregion

        public void Fight(Monster _monster)
        {
            lock (Program.printlock)
            {
                // Sorry, that Console Sound s*cks
                // Program.MakeSomeNoise(1, _monster.outfit.FightSound);
                string store = parts[1];
                parts[1] = parts[3];
                PrintMonster();
                parts[1] = store;
                // display fight for 50ms
                Thread.Sleep(50);
                HideMonster(pos_x, pos_y);
                PrintMonster();


            }

        }

        // ToDo: Delete this method ?
        public void HitMonster(Stats _me, Stats _oponent)
        {
            // get defense of _monster
            // calc damage ( myAttack - hisDefense = theDamage )
            // write new health at _monster ( hisHealth -= theDamage  )
            // do _monster.PrintStats()
            lock (Program.printlock)
            {
                int damage = 0;
                damage = _me.aPoints - _oponent.dPoints;
                // _oponent.hPoints -= damage;
                _oponent.SetHPoints(damage);
            
            }
            Program.PrintStats();
        }

        public void HitMonster(Stats _me, Stats _oponent, bool _enemyWasHit)
        {
            int damage = 0;
            if (_enemyWasHit)
            {
                damage = _me.aPoints - _oponent.dPoints;
                _oponent.SetHPoints(damage);
            }
            else
            {
                damage = _oponent.aPoints - _me.dPoints;
                _me.SetHPoints(damage);
            }
            Program.PrintStats(_me, _oponent);

        }
    }
}

