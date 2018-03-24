using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringBasics
{
    class Program
    {
        static string[] lstring = { "xxxxx", "yyyyy" , "zzzzz"}; 

        static void Main(string[] args)
        {
            // make it look like "xx#yy#zz"
            string line = "";
            string sstring = "";

            for (int i = 0; i < 3; i++)
            {
                line = lstring[i];
                for (int j = 0; j < 2; j++)
                {
                    sstring += line[j];
                }

                if (i < 2)
                {
                    sstring += "#";
                }

            }

            // Console.WriteLine(sstring);
            int distance = 99;
            String text = String.Format("Actual Distance:{0,4}", distance);
            Console.WriteLine(text);

            Console.WriteLine("Press any key ...");
            Console.ReadLine();
        }
    }
}
