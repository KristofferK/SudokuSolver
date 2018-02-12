using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SudokuGUI
{
    public class SudokuSolver : INotifyPropertyChanged
    {
        private SudokuBoard board;
        private Coord lastIndex;
        private int[][][] dynSolutions;
        public event EventHandler<EventArgs> solved;

        private int backtrackCount;
        public int BacktrackCount
        {
            get { return backtrackCount; }
            set
            {
                backtrackCount = value;
                NotifyPropertyChanged();
            }
        }

        private int delay;
        public int Delay
        {
            get { return delay; }
            set
            {
                delay = value;
                NotifyPropertyChanged();
            }
        }

        public SudokuSolver(SudokuBoard board)
        {
            this.board = board;
            Delay = 10;
        }
        
        private void onSolved()
        {
            solved?.Invoke(this, new EventArgs());
        }

        private Coord GetNextEmptyCoord()
        {
            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    if (board[i, j].Value == null)
                    {
                        return new Coord(i, j);
                    }
                }
            }
            return null;
        }

        #region Backtracking algorithm
        public void Solve()
        {
            lastIndex = GetLastIndex();
            Coord c = GetNextEmptyCoord();
            Solve(c.X, c.Y);
        }

        private bool Solve(int x, int y)
        {
            Thread.Sleep(Delay);
            for (int i = 1; i < 10; i++)
            {
                if (OK(x, y, i))
                {
                    board[x, y].Value = i;
                    Coord nextCoord = GetNextEmptyCoord();
                    
                    if (x == lastIndex.X && y == lastIndex.Y) // denne condition skal være her så recursionen stopper når det sidste coord er løst
                    {
                        onSolved();
                        return true;
                    }
                    else if (Solve(nextCoord.X, nextCoord.Y))
                    {
                        return true;
                    }
                    else
                    {
                        board[x, y].Value = null;
                        BacktrackCount++;
                    }
                }
            }
            return false;
        }
        #endregion

        public void SolveDyn() // Virker semi, kun på lette sudoku, kan ikke gætte
        {
            InstSolutionsArray();
            RemoveSolutions();
            while (NotDone())
            {                
                Thread.Sleep(60);
                for (int x = 0; x < 9; x++)
                {
                    for (int y = 0; y < 9; y++)
                    {
                        int solutionCount = 0;
                        for (int z = 0; z < 9; z++)
                        {                            
                            if (dynSolutions[x][y][z] != 0)
                            {
                                solutionCount++;
                            }
                        }
                        if (solutionCount == 1)
                        {
                            board[x, y].Value = GetSolution(x, y);
                            RemoveSolutions(x,y, board[x,y].Value.Value);
                        }
                    }
                }                
            }
            onSolved();
        }
            
        private int? GetSolution(int x, int y)
        {            
            for (int i = 0; i < 9; i++)
            {
                if (dynSolutions[x][y][i] != 0)
                {
                    return dynSolutions[x][y][i];
                }
            }
            return null;
        }

        private void InstSolutionsArray()
        {
            dynSolutions = new int[9][][];
            for (int x = 0; x < 9; x++)
            {
                dynSolutions[x] = new int[9][];
                for (int y = 0; y < 9; y++)
                {
                    dynSolutions[x][y] = new int[9];
                    if (board[x,y].Value == null)
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            dynSolutions[x][y][i] = i + 1;
                        }
                    }
                }
            }
        }

        private void RemoveSolutions()
        {
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    if (board[x,y].Value != null)
                    {
                        RemoveSolutions(x, y, board[x, y].Value.Value);
                    }
                }
            }
        }

        private bool NotDone()
        {
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    if (board[x,y].Value == null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void RemoveSolutions(int x, int y, int number)
        {
            // X aksen
            for (int i = 0; i < 9; i++)
            {
                dynSolutions[x][i][number-1] = 0;

                //tag along
                dynSolutions[x][y][i] = 0;
            }

            // Y aksen
            for (int i = 0; i < 9; i++)
            {
                dynSolutions[i][y][number-1] = 0;
            }

            // Block
            int startX = (x / 3) * 3;
            int startY = (y / 3) * 3;
            int endX = startX + 3;
            int endY = startY + 3;

            for (int curX = startX; curX < endX; curX++)
            {
                for (int curY = startY; curY < endY; curY++)
                {
                    dynSolutions[curX][curY][number-1] = 0;
                }
            }
        }

        private bool OK(int x, int y, int number)
        {
            bool rv = CheckRowCol(x, y, number) && CheckBlock(x, y, number);
            
            return rv;
        }

        private bool CheckBlock(int x, int y, int? number)
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
                        if (board[curX, curY].Value == number)
                        {
                            return false;
                        }
                    }                    
                }
            }
            return true;
        }

        private bool CheckRowCol(int x, int y, int? number)
        {
            for (int row = 0; row < board.Size; row++)
            {
                if (row != x && board[row, y].Value == number)
                {
                     return false;
                }
            }
            for (int col = 0; col < board.Size; col++)
            {
                if (col != y && board[x, col].Value == number)
                {
                    return false;
                }
            }
            return true;
        }
        private Coord GetLastIndex()
        {
            for (int i = board.Size - 1; i >= 0; i--)
            {
                for (int j = board.Size - 1; j >= 0; j--)
                {
                    if (board[i, j].Value == null)
                    {
                        return new Coord(i, j);
                    }
                }
            }
            return null;
        }
        public bool ValidateBoard()
        {
            bool foundZero = false;
            for (int i = board.Size - 1; i >= 0; i--)
            {
                for (int j = board.Size - 1; j >= 0; j--)
                {
                    if (board[i, j].Value != null)
                    {
                        if (!CheckBlock(i, j, board[i,j].Value) || !CheckRowCol(i, j, board[i, j].Value))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        foundZero = true;
                    }
                }
            }
            // Alt er gyldigt. Return true hvis der blev fundet et 0, ellers false.
            return foundZero;
        }


        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
