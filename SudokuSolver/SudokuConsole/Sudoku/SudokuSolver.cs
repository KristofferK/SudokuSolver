using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class SudokuSolver
    {
        private SudokuBoard board;
        private Coord lastIndex;
        public SudokuSolver(SudokuBoard board)
        {
            this.board = board;
        }
        
        public void Solve()
        {
            Coord c = GetNextEmptyCoord();
            lastIndex = GetLastIndex();
            Solve(c.X, c.Y);
        }

        private Coord GetLastIndex()
        {
            for (int i = board.Size - 1; i >= 0; i--)
            {
                for (int j = board.Size - 1; j >= 0; j--)
                {
                    if (board[i, j] == 0)
                    {
                        return new Coord(i, j);
                    }
                }
            }
            return null;
        }

        private Coord GetNextEmptyCoord()
        {
            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    if (board[i, j] == 0)
                    {
                        return new Coord(i, j);
                    }
                }
            }
            return null;
        }

        private bool Solve(int x, int y)
        {
            for (int i = 1; i < 10; i++)
            {
                if (OK(x, y, i))
                {
                    board[x, y] = i;
                    Coord nextCoord = GetNextEmptyCoord();
                    
                    if (x == lastIndex.X && y == lastIndex.Y) // denne condition skal være her så recursionen stopper når det sidste coord er løst
                    {
                        return true;
                    }
                    else if (Solve(nextCoord.X, nextCoord.Y))
                    {
                        //Console.Clear();
                        //Console.Write(board);
                        return true;                        
                    }
                    else
                    {
                        board[x, y] = 0;
                    }
                }
                //Console.Clear();
                //Console.Write(board);
            }
            return false;
        }        

        private bool OK(int x, int y, int number)
        {
            bool rv = CheckRowCol(x, y, number) && CheckBlock(x, y, number);
            
            return rv;
        }

        private bool CheckBlock(int x, int y, int number)
        {
            int startX = (x / 3) * 3;
            int startY = (y / 3) * 3;
            int endX = startX + 3;
            int endY = startY + 3;

            for (int curX = startX; curX < endX; curX++)
            {                
                for (int curY = startY; curY < endY; curY++)
                {
                    if (curX != x || curY != y)
                    {
                        if (board[curX, curY] == number)
                        {
                            return false;
                        }
                    }                    
                }
            }
            return true;
        }

        private bool CheckRowCol(int x, int y, int number)
        {
            for (int row = 0; row < board.Size; row++)
            {
                if (row != x && board[row, y] == number)
                {
                     return false;
                }
            }
            for (int col = 0; col < board.Size; col++)
            {
                if (col != y && board[x, col] == number)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
