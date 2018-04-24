using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    /// <summary>
    /// ha
    /// </summary>
    public struct Helper
    {
        /*  Function < int[] > RandomStartPos()
         *  Calculates a random position in the right side of the field
         *  Returns an int[]
         */
        /// <summary>
        /// 
        /// </summary>
        public static Random random = new Random();     // ToDo: Make sure, we do it only once

        /*
      *  Monster Movement choices
      *  
      *  Looks like the most stupid way
      *  
      */
        static Direction go = new Direction
        {
            up = new int[] { 0, -1 },
            right_up = new int[2] { 1, -1 },
            right = new int[] { 1, 0 },
            right_down = new int[] { 1, 1 },
            down = new int[] { 0, 1 },
            left_down = new int[] { -1, 1 },
            left = new int[] { -1, 0 },
            left_up = new int[] { -1, -1 },
            stay = new int[] { 0, 0 }
        };

        // make a choice
        static Choice[] choices = new Choice[5];

        /// <summary>
        /// Init teh possible directions of movement
        /// </summary>
        public static void InitChoices()
        {
            choices[0] = new Choice { coord = go.up };              //  1,2
            choices[1] = new Choice { coord = go.right };           //  3,4,5,6,7,8
            choices[2] = new Choice { coord = go.left };           //  9,10,11,12,13,14,15,16
            choices[3] = new Choice { coord = go.down };            //  17,18
            choices[4] = new Choice { coord = go.stay };            //  19
        }

        /*  
         *  Get a weighted random direction
         *  
         *  ToDo: Create a weight formula
         *  
         */

        /// <summary>
        /// Calculates a weighted random enemy movement
        /// </summary>
        /// <remarks>Weighted movement into left part of field</remarks>
        /// <returns>An int array with next x,y coords</returns>
        public static int[] RandomMove()
        {
            int[] goHere = new int[] { 0, 0 };
            int selected = random.Next(1, 20);

            if (selected >= 0 && selected < 3)  // 
            {
                goHere = choices[0].coord;
            }
            else if (selected >= 3 && selected < 9)
            {
                goHere = choices[1].coord;
            }
            else if (9 <= selected && selected < 17)
            {
                goHere = choices[2].coord;
            }
            else if (17 <= selected && selected < 20)
            {
                goHere = choices[3].coord;
            }
            else if (selected == 20)
            {
                goHere = choices[4].coord;
            }

            return goHere;
        }


    }
}
