using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonsterHunter
{
    class Monster
    {
        // store top left corner of monster
        /// <summary>
        /// The x of top left coordinates of the monster print.
        /// </summary>
        /// <remarks>Is relative to monster randomPosition</remarks>
        int start_x;
        /// <summary>
        /// The y of top left coordinates of the monster print.
        /// </summary>
        /// <remarks>Is relative to monster randomPosition</remarks>
        int start_y;

        // store actual randomPosition of monster
        /// <summary>
        /// Store actual x position
        /// </summary>
        public int pos_x;
        /// <summary>
        /// Store actual x randomPosition
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
        /// Print the monster at randomPosition
        /// </summary>
        /// <param name="x">x-coordinate of monster center</param>
        /// <param name="y">y-coordinate of monster center</param>
        public void PrintMonster(int x, int y)
        {
            // store aktual randomPosition of monster
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

                // [4] set cursor randomPosition back to params
                Console.SetCursorPosition(pos_x, pos_y);

                // [5] set cursorColor back
                Console.ForegroundColor = color_backup;
            }  // end of lock

        }

        public void PrintDancingMonster(int x, int y)
        {
            // store aktual randomPosition of monster
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

                // [4] printing the name
                //      set cursor
                Console.SetCursorPosition(Console.CursorLeft - parts[0].Length, Console.CursorTop + 1);
                //      print name
                Console.BackgroundColor = outfit.designColor;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(name);
                Console.BackgroundColor = ConsoleColor.Black;
                // [5] set cursor randomPosition back to params
                Console.SetCursorPosition(pos_x, pos_y);

                // [6] set cursorColor back
                Console.ForegroundColor = color_backup;
            }  // end of lock

        }

        // print a monster and return true when printed;
        /// <summary>
        /// Print the monster at stored randomPosition
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
        /// <remarks>Hide the monster by re-printing background at given randomPosition.
        /// Locks the Thread while printing.</remarks>
        /// <param name="x">x-coordinate of monster randomPosition</param>
        /// <param name="y">y-coordinate of monster randomPosition</param>
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
            // set randomPosition of cursor after every printed part of monster
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

                // [4] set cursor randomPosition back to params
                Console.SetCursorPosition(pos_x, pos_y);

                // [5] set CursorColor back to default
                Console.ForegroundColor = color_backup;
            }
        }

        static Symbol testSign = new Symbol() { Sign = ' ', Color = 0 };

        static int cr = 0;
        //static int lf = 0;

        public void HideDancingMonster(int x, int y)
        {
            // [1] Zuerst mit Leerzeichen überschreiben;
            start_x = x - 2;
            start_y = y - 1;
            // set cursor to top left corner of monster
            cr = start_x;
            //lf = start_y;
            // print the old randomPosition with spaces;
            #region print with spaces
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    //try
                    //{
                    //    testSign.Sign = ' ';
                    //}
                    //catch (Exception ex)
                    //{
                    //    Debug.WriteLine("Symbol not set!\n" + ex.Message);
                    //}
                    // Debug.WriteLine(testSign.Sign);
                    try
                    {
                        // set cursor to randomPosition
                        lock (Game.printlock)
                        {
                            Console.SetCursorPosition(start_x, start_y);
                            Console.Write(testSign.Sign);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Cursor Out of Range!\n" + ex.Source);
                    }



                    // Cursor einen nach rechts
                    start_x++;
                }
                // Cursor zurück nach links
                start_x = cr;
                // Cursor in die nächste Zeile
                start_y++;
            }
            //++start_y;
            //start_x = cr;
            //Console.SetCursorPosition(start_x, start_y);
            //Console.Write("     ");
            #endregion

            // [2] Zeichen aus Symbol[,] holen und schreiben
            start_x = x - 2;
            start_y = y - 1;
            // set cursor to top left corner of monster
            cr = start_x;
            lock (Game.printlock)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        start_x++;
                        try
                        {
                            testSign = Background.signs[start_x, start_y];

                        }
                        catch (Exception ex)
                        {

                            Debug.WriteLine("Symbol not set!\n" + ex.Message);
                        }
                        // Debug.WriteLine(testSign.Sign);
                        try
                        {
                            // set cursor to randomPosition
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
                        lock (Game.printlock)
                        {
                            Console.Write(testSign.Sign);

                        }

                    }
                    // Cursor auf nächste Zeile
                    start_y++;
                    // Cursor zurück nach links
                    start_x = cr;
                }
                //++start_y;
                //start_x = cr;
                //ConsoleColor store = Console.ForegroundColor;
                //Console.ForegroundColor = ConsoleColor.White;
                //Console.Write(name);
                //Console.ForegroundColor = store;

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
                string store = _monster.parts[1];
                try
                {
                    _monster.parts[1] = _monster.parts[3];

                }
                catch (IndexOutOfRangeException ex)
                {

                    System.Diagnostics.Debug.WriteLine("We have no 'fighting' layout." + ex);
                }
                _monster.PrintMonster();
                _monster.parts[1] = store;
                // display fight for 50ms
                Thread.Sleep(50);
                _monster.HideMonster(pos_x, pos_y);
                _monster.PrintMonster();


            }

        }

        // Delete this method ?
        public void HitMonster(Stats _me, Stats _oponent)
        {
            // get defense of _monster
            // calc damage ( myAttack - hisDefense = theDamage )
            // write new health at _monster ( hisHealth -= theDamage  )
            // do _monster.PrintStats()
            Game.Rounds += 1;
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
            Game.Rounds += 1;
            int damage = 0;
            if (_enemyWasHit)
            {
                if (_me.aPoints > _oponent.dPoints)
                {
                    damage = _me.aPoints - _oponent.dPoints;
                    _oponent.SetHPoints(damage);
                }

#if DEBUG
                System.Diagnostics.Debug.WriteLine("Player attacks Enemy");
                // System.Diagnostics.Debug.WriteLine("damage = attack - defense");
                System.Diagnostics.Debug.WriteLine(damage + " = " + _me.aPoints + " - " + _oponent.dPoints);
#endif
            }
            else
            {
                if (_oponent.aPoints > _me.dPoints)
                {
                    damage = _oponent.aPoints - _me.dPoints;
                    _me.SetHPoints(damage);
                }

#if DEBUG
                System.Diagnostics.Debug.WriteLine("Enemy attacks Player");
                // System.Diagnostics.Debug.WriteLine("damage = attack - defense");
                System.Diagnostics.Debug.WriteLine(damage + " = " + _oponent.aPoints + " - " + _me.dPoints);
#endif
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
            // System.Diagnostics.Debug.WriteLine("Enemy: " + System.DateTime.Now);
            /*  
             *  We will be called every <int>_millis
             */

            // we store some data
            int[] move = { 0, 0 };
            int new_x = 0;
            int new_y = 0;
            int pos_x = Game.enemy.pos_x;
            int pos_y = Game.enemy.pos_y;
            // bool moveIsPossible = true;

            try
            {

                // I check, if one of us is dead
                if (Game.playerStats.GetHPoints() <= 0 || Game.enemyStats.GetHPoints() <= 0)
                {
                    // I dont want to run anymore
                    // moveIsPossible = false;
                    // I stop the clock
                    Game.KillCountdown();

                    // I check, if player is dead!
                    // in case, player is manually controlled
                    // we must kill his thread
                    if (Game.playerStats.GetHPoints() <= 0)
                    {
                        // player is dead
                        Player.StopPlayer();
                        // break;
                    }

                }
                else
                {
                    // I am alive

                    //// next line(s) work
                    // move = RandomWeightedMove();

                    // the thread problem
                    //  #1
                    //  put 'move...' into lock
                    //  #2
                    //  and expand lock until 'move' is not used anymore

                    lock (Game.printlock)
                    {
                        // #1
                        // attack the player
                        move = GetCloser(Game.enemy as Monster, Game.player as Monster);


                        // erst hauen, dann laufen;
                        // und nur nebeneinander kämpfen;
#if TEST
                    int test1 = Game.dist.CalcDistance_xy();
                    int test2 = Game.dist.CalcDistance_y();
                    Debug.WriteLine("Dist xy: {0}", test1);
                    Debug.WriteLine("Dist  y: {0}", test2);
#endif
                        if (Game.dist.CalcDistance_xy() < 5 && Game.dist.CalcDistance_y() < 4)
                        {
                            Game.enemy.Fight(Game.enemy);
                            Game.enemy.HitMonster(Game.playerStats, Game.enemyStats, false);
                            // NoToDo: Sound
                            // play sound here;
                        }
                        new_x = pos_x + move[0];
                        new_y = pos_y + move[1];

                        // min_x < new_x < max_x
                        // is next move inside the field?
                        if ((3 < new_x && new_x < Window.x - 3) && (Window.top + 2 < new_y && new_y < Window.y - 3))
                        {
                            // we are inside field
                            Game.enemy.HideMonster(Game.enemy.pos_x, Game.enemy.pos_y);
                            Game.enemy.pos_x += move[0];
                            Game.enemy.pos_y += move[1];
                            Game.enemy.PrintMonster();

                        }

                    }
                    //  #2
                    //  end of lock

                    // check for health in case we had a fight;
                    // if both alive, print distance,
                    if (Game.playerStats.GetHPoints() > 0 && Game.enemyStats.GetHPoints() > 0)
                    {
                        Game.dist.PrintTheDist();
                        // break;
                    }
                    else
                    {
                        // one of us is dead
                        Game.KillCountdown();
                        Game.play = false;

                        // leave the loop;
                        // break;
                    }
                }

            }   // end try
            catch (ThreadAbortException)
            {

                // System.Diagnostics.Debug.WriteLine("Catch: MovePlayer() " + ex);
            }


            // give waiting threads a chance to work
            resetMoveMonster.Set();

        }

        /// <summary>
        /// Calculates a weighted random  movement
        /// </summary>
        /// <remarks>Weighted movement into left part of field</remarks>
        /// <returns>An int array with next x,y step in range -1 to 1</returns>
        static int[] RandomWeightedMove()
        {
            int[] goHere = new int[] { 0, 0 };
            int selected = Game.rndm.Next(1, 20);

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
            int selected = Game.rndm.Next(0, 4);

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
                // we dont move not ;-)
                //case 4:
                //    goHere = Choice.goTo[4].coord;
                //    break;
                default:
                    break;
            }

            return goHere;
        }

        public static int[] RandomStartPos()
        {
            int[] start = new int[2];

            // pos x min/max
            int x_min = Window.x / 2 + 4;  // 4 steps right from middle
            int x_max = Window.x - 15;      // 15 steps left from outer right

            // pos y min/max
            int y_min = 8;          // 4 steps below top // ToDo: set dynamic ??
            int y_max = Window.y - 6;      // 6 steps above bottom

            start[0] = Game.rndm.Next(x_min, x_max);
            start[1] = Game.rndm.Next(y_min, y_max);

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

            start[0] = Game.rndm.Next(x_min, x_max);
            start[1] = Game.rndm.Next(y_min, y_max);

            return start;
        }

        static int[] old = new int[2];
        static int[] move = new int[2];

        public void DanceTheMonster(object _initState)
        {
            AutoResetEvent danceReset = (AutoResetEvent)_initState;
            old[0] = pos_x;
            old[1] = pos_y;
            lock (Game.printlock)
            {
                HideDancingMonster(pos_x, pos_y);

            }

            do
            {
                move = RandomDanceMove();
                old[0] += move[0];
                old[1] += move[1];
            }
            // links + 2 && rechts -2             &&    oben + 1  &&  unten - 1
            while (!(
            (old[0] >= 2 && old[0] <= Window.x - 2) &&
            (old[1] >= 1 && old[1] <= Window.y - 1)
            ));
            lock (Game.printlock)
            {
                PrintDancingMonster(old[0], old[1]);

            }
            // check if move is possible
            danceReset.WaitOne();


        }

        static int[] goThere;
        static int distance;
        static int[] target;
        static int[] me;
        static int[] new_me;

        //public static int[] GetCloser()
        //{
        //    // get all possible directions;
        //    Choice[] attack = Choice.goTo;

        //    // get the randomPosition of opponent;
        //    target = new int[2];
        //    target[0] = Game.player.pos_x;
        //    target[1] = Game.player.pos_y;

        //    // get own randomPosition
        //    me = new int[2];
        //    me[0] = Game.enemy.pos_x;
        //    me[1] = Game.enemy.pos_y;

        //    // get actual distance
        //    distance = Game.dist.GetDistance(me, target);

        //    // calculate the distance for every possible move;
        //    new_me = new int[2];
        //    int[] reset_me = new int[] { 0, 0 };
        //    int new_dist = 100;
        //    for (int i = 0; i < attack.Length; i++)
        //    {
        //        // calculate next randomPosition
        //        new_me[0] = me[0] + attack[i].coord[0];
        //        new_me[1] = me[1] + attack[i].coord[1];
        //        new_dist = Game.dist.GetDistance(new_me, target);

        //        if (new_dist < distance)
        //        {
        //            goThere = attack[i].coord;
        //        }
        //        new_me = reset_me;

        //    }
        //    // store the distance, if smaller;
        //    // return the move with minimum distance
        //    return goThere;
        //}

        static object moveLock = new object();

        public static int[] GetCloser(Monster _me, Monster _him)
        {
            // get all possible directions;
            Choice[] attack = Choice.goTo;

            // get the randomPosition of opponent;
            target = new int[2];
            target[0] = _him.pos_x;
            target[1] = _him.pos_y;

            // get own randomPosition
            me = new int[2];
            me[0] = _me.pos_x;
            me[1] = _me.pos_y;

            lock (moveLock)
            {

                // get actual distance
                distance = Game.dist.GetDistance(me, target);

                // calculate the distance for every possible move;
                // store a possible move;
                new_me = new int[2];

                int[] reset_me = new int[] { 0, 0 };
                // store calculated distance
                int new_dist = 100;
                for (int i = 0; i < attack.Length; i++)
                {
                    // calculate next randomPosition
                    new_me[0] = me[0] + attack[i].coord[0];
                    new_me[1] = me[1] + attack[i].coord[1];
                    new_dist = Game.dist.GetDistance(new_me, target);

                    if (new_dist < distance)
                    {
                        goThere = attack[i].coord;
                    }
                    new_me = reset_me;

                }
                // store the distance, if smaller;
                // return the move with minimum distance
                return goThere;

            } // end of lock
        }

        public void DanceAsWinner()
        {

            AutoResetEvent danceReset = new AutoResetEvent(true);
            var dance = new Timer(Game.winner.DanceTheMonster, danceReset, 0, 1111);
            danceReset.Set();
        }

    }
}

