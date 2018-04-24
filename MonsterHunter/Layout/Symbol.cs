using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    struct Symbol
    {
        public char sign;
        public int color;

        public char Sign { get => sign; set => sign = value; }
        public int Color { get => color; set => color = value; }
    }
}
