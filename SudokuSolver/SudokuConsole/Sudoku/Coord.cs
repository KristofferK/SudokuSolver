using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Coord
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
