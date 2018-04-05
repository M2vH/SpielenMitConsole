using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonsterHunter
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

            lock (Game.printlock)
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
        public void PrintMonster()
        {
            if (pos_x != 0 && pos_y != 0)
            {
                PrintMonster(pos_x, pos_y);
            }
        }

        public void PrintMonster(int[] _coords)
        {
                PrintMonster(_coords[0], _coords[1]);
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
                    monster_string += Background.background[start_y + j][start_x + i];
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

            lock (Game.printlock)
            {
                // set a new ForegroundColor
                Console.ForegroundColor = Background.fieldColor;

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

        static Symbol testSign;

        public void HideDancingMonster(int x, int y)
        {
            start_x = x;
            start_y = y;
            // set cursor to top left corner of monster
            start_x -= 2;
            start_y -= 1;
            // print the old position with spaces;

            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    start_x += i;
                    start_y += j;
                    try
                    {
                       testSign.Sign = ' ';
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Symbol not set!\n" + ex.Message);
                    }
                    // Debug.WriteLine(testSign.Sign);
                    lock (Game.printlock)
                    {
                        try
                        {
                            // set cursor to position
                            Console.SetCursorPosition(start_x, start_y);
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine("Cursor Out of Range!\n" + ex.Source);
                        }

                        Console.Write(testSign.Sign);

                    }

                }
            }

            start_x = x;
            start_y = y;
            // set cursor to top left corner of monster
            start_x -= 2;
            start_y -= 1;
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    start_x += i;
                    start_y += j;
                    try
                    {
                        testSign = Background.signs[start_x, start_y];

                    }
                    catch (Exception ex)
                    {

                        Debug.WriteLine("Symbol not set!\n" + ex.Message);
                    }
                    // Debug.WriteLine(testSign.Sign);
                    lock (Game.printlock)
                    {
                        try
                        {
                            // set cursor to position
                            Console.SetCursorPosition(start_x, start_y);

                        }
                        catch (Exception ex)
                        {

                            System.Diagnostics.Debug.WriteLine("Cursor Out of Range!\n" + ex.Source);
                        }

                        try
                        {
                            // set the right color
                            Console.ForegroundColor = (ConsoleColor)testSign.Color;

                        }
                        catch (Exception ex)
                        {

                            Debug.WriteLine("Color out of Range!\n" + ex.Source);
                        }
                        // get the corresponding char
                        // write the right symbol
                        // store = Background.signs[start_x, start_y].Sign;
                        Console.Write(testSign.Sign);

                    }

                }
            }
        }

        //  let it fight
        //  switch parts[3] and parts[1]
        /// <summary>
        /// Prints the "fighting" part of monster
        /// <remarks>Calls the fight sound function</remarks>
        /// </summary>
        /// <param name="_monster">the monster that will fight</param>
        public void Fight(Monster _monster)
        {
            lock (Game.printlock)
            {
                // Sorry, that Console Sound s*cks
                // Program.MakeSomeNoise(1, _monster.outfit.FightSound);
                string store = parts[1];
                try
                {
                    parts[1] = parts[3];

                }
                catch (IndexOutOfRangeException ex)
                {

                    System.Diagnostics.Debug.WriteLine("We have no 'fighting' layout." + ex);
                }
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
            lock (Game.printlock)
            {
                int damage = 0;
                damage = _me.aPoints - _oponent.dPoints;
                // _oponent.hPoints -= damage;
                _oponent.SetHPoints(damage);

            }
            Game.PrintStats();
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
            Game.PrintStats(_me, _oponent);

        }

        //  the Timer CallBack Funktion
        //  This function is called,
        //  when Timer ticks happen.
        /// <summary>
        /// The Monster Timer Event Callback
        /// </summary>
        /// <remarks>Moves the monster. Is Callback function for Timer Event</remarks>
        /// <param name="_stateInfo">The Event Handle</param>
        public static void MoveMonster(Object _stateInfo)
        {
            AutoResetEvent resetMoveMonster = (AutoResetEvent)_stateInfo;

            /*  
             *  We will be called every <int>_millis
             */

            // we store some data
            int[] move = { 0, 0 };
            int new_x = 0;
            int new_y = 0;
            int pos_x = Game.enemy.monster.pos_x;
            int pos_y = Game.enemy.monster.pos_y;
            bool moveIsPossible = true;

            try
            {

                while (moveIsPossible)
                {
                    // Is enemy still alive?
                    if (Game.playerStats.GetHPoints() <= 0 || Game.enemyStats.GetHPoints() <= 0)
                    {
                        moveIsPossible = false;
                        Game.KillCountdown();
                        if (Game.playerStats.GetHPoints() <= 0)
                        {
                            // player is dead
                            Player.StopPlayer();
                            break;
                        }

                    }

                    move = RandomWeightedMove();
                    // erst hauen, dann laufen;
                    // und nur nebeneinander kämpfen;
                    if (Game.dist.GetDistance() < 5 && Game.dist.diff_y < 4)
                    {
                        int r = Game.random.Next(2, 14);
                        if ((r % 2) == 0)
                        {
                            Game.enemy.monster.Fight(Game.enemy.monster);
                            // enemy.monster.HitMonster(enemyStats,playerStats);
                            Game.enemy.monster.HitMonster(Game.playerStats, Game.enemyStats, false);
                            Sound.PlaySound(1, Game.enemy.monster.outfit.FightSound);
                        }
                    }
                    new_x = pos_x + move[0];
                    new_y = pos_y + move[1];

                    // min_x < new_x < max_x
                    // is next move inside the field?
                    if ((2 < new_x && new_x < Window.x - 3) && (Window.top + 1 < new_y && new_y < Window.y - 2))
                    {
                        // we are inside field
                        Game.enemy.monster.HideMonster(Game.enemy.monster.pos_x, Game.enemy.monster.pos_y);
                        Game.enemy.monster.pos_x += move[0];
                        Game.enemy.monster.pos_y += move[1];
                        Game.enemy.monster.PrintMonster();


                        // check for health
                        // if both alive, print distance,
                        if (Game.playerStats.GetHPoints() > 0 && Game.enemyStats.GetHPoints() > 0)
                        {
                            Game.dist.PrintTheDist();
                            break;
                        }
                        else
                        {
                            // someone is dead
                            Game.KillCountdown();
                            Game.play = false;
                            moveIsPossible = false;
                            // if player is NOT moving, he will not
                            // recognize he is dead.
                            // The hard way;
                            // we must catch exception
                            // StopPlayer();
                            break;
                        }


                    }
                    else
                    {
                        moveIsPossible = true;
                    }


                } // end of while
            }
            catch (ThreadAbortException ex)
            {

                System.Diagnostics.Debug.WriteLine("Catch: MovePlayer() " + ex);
            }


            // give waiting threads a chance to work
            resetMoveMonster.Set();

        }

        /// <summary>
        /// Calculates a weighted random enemy movement
        /// </summary>
        /// <remarks>Weighted movement into left part of field</remarks>
        /// <returns>An int array with next x,y coords</returns>
        static int[] RandomWeightedMove()
        {
            int[] goHere = new int[] { 0, 0 };
            int selected = Game.random.Next(1, 20);

            if (selected >= 0 && selected < 3)  // 
            {
                goHere = Choice.goTo[0].coord;
            }
            else if (selected >= 3 && selected < 9)
            {
                goHere = Choice.goTo[1].coord;
            }
            else if (9 <= selected && selected < 17)
            {
                goHere = Choice.goTo[2].coord;
            }
            else if (17 <= selected && selected < 20)
            {
                goHere = Choice.goTo[3].coord;
            }
            else if (selected == 20)
            {
                goHere = Choice.goTo[4].coord;
            }

            return goHere;
        }

        public static int[] RandomDanceMove()
        {
            int[] goHere = new int[] { 0, 0 };
            int selected = Game.random.Next(0, 5);

            switch (selected)
            {
                case 0:
                    goHere = Choice.goTo[0].coord;
                    break;
                case 1:
                    goHere = Choice.goTo[1].coord;
                    break;
                case 2:
                    goHere = Choice.goTo[2].coord;
                    break;
                case 3:
                    goHere = Choice.goTo[3].coord;
                    break;
                case 4:
                    goHere = Choice.goTo[4].coord;
                    break;
                default:
                    break;
            }

            return goHere;
        }

        /*  Function < int[] > RandomStartPos()
 *  Calculates a random position in the right side of the field
 *  Returns an int[]
 */
        public static int[] RandomStartPos()
        {
            int[] start = new int[2];

            // pos x min/max
            int x_min = Window.x / 2 + 4;  // 4 steps right from middle
            int x_max = Window.x - 4;      // 4 steps left from outer right

            // pos y min/max
            int y_min = 8;          // 4 steps below top // ToDo: set dynamic ??
            int y_max = Window.y - 4;      // 4 steps above bottom

            start[0] = Game.random.Next(x_min, x_max);
            start[1] = Game.random.Next(y_min, y_max);

            return start;
        }

        public static int[] RandomStartPos(bool _fullScreen)
        {
            int[] start = new int[2];

            // pos x min/max
            int x_min = 4;  // 4 steps right from zero
            int x_max = Window.x - 4;      // 4 steps left from outer right

            // pos y min/max
            int y_min = 8;          // 4 steps below top // ToDo: set dynamic ??
            int y_max = Window.y - 4;      // 4 steps above bottom

            start[0] = Game.random.Next(x_min, x_max);
            start[1] = Game.random.Next(y_min, y_max);

            return start;
        }

        static int[] old = new int[2];
        static int[] move = new int[2];

        public void DanceMonster(object _initState)
        {
            AutoResetEvent danceReset = (AutoResetEvent)_initState;
            old[0] = pos_x;
            old[1] = pos_y;
            HideDancingMonster(pos_x, pos_y);
            move = RandomDanceMove();
            old[0] += move[0];
            old[1] += move[1];
            PrintMonster(old[0], old[1]);
            danceReset.Set();


        }


    }
}

