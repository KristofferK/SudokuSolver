using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGUI
{
    public static class SudokuFormatter
    {
        public static Dictionary<Coord, int> Format(string sudokoString)
        {
            Dictionary<Coord, int> coordValues = new Dictionary<Coord, int>();

            string[] lines = sudokoString.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    Coord c = new Coord(i, j);
                    coordValues[c] = lines[i][j] == ' ' ? 0 : int.Parse(lines[i][j].ToString());
                }
            }

            return coordValues;
        }
    }
}
