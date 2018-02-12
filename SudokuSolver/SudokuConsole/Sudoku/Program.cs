using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Program
    {
        private static SudokuBoard board;
        static void Main(string[] args)
        {
            FillBoard();

            SudokuSolver solver = new SudokuSolver(board);

            Console.WriteLine(board);
            solver.Solve();
            Console.WriteLine(board);

            Console.ReadKey();
        }

        static void FillBoard()
        {
            board = new SudokuBoard();
            board[0, 1] = 1;
            board[0, 3] = 3;
            board[1, 2] = 8;
            board[1, 4] = 1;
            board[1, 6] = 3;
            board[1, 8] = 6;
            board[2, 2] = 3;
            board[2, 4] = 9;
            board[2, 6] = 8;
            board[3, 0] = 3;
            board[3, 1] = 5;
            board[3, 5] = 7;
            board[3, 7] = 6;
            board[4, 3] = 6;
            board[4, 6] = 9;
            board[4, 8] = 5;
            board[5, 0] = 9;
            board[5, 5] = 4;
            board[5, 8] = 7;
            board[6, 2] = 4;
            board[6, 5] = 2;
            board[6, 7] = 5;
            board[7, 0] = 8;
            board[7, 2] = 5;
            board[7, 7] = 2;
            board[8, 0] = 2;
            board[8, 4] = 5;
            board[8, 5] = 3;
            board[8, 8] = 4;
        }
    }
}
