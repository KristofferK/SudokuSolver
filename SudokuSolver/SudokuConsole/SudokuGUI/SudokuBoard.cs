using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGUI
{
    public class SudokuBoard
    {
        private SudokuNumber[][] board;
        public SudokuNumber[][] Board
        {
            get { return board; }
            set { board = value; }
        }

        public int Size { get; }

        public SudokuBoard()
        {
            board = new SudokuNumber[9][];
            for (int i = 0; i < 9; i++)
            {
                board[i] = new SudokuNumber[9];
                for (int j = 0; j < 9; j++)
                {
                    board[i][j] = new SudokuNumber();
                }
            }
            Size = 9;
        }

        public SudokuNumber this[int x, int y]
        {
            get { return board[x][y]; }
            set
            {
                board[x][y] = value;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    sb.Append("[" + (this[i,j].Value != 0 ? this[i, j].ToString() : " ") + "]");
                    if (j % 3 == 2)
                    {
                        sb.Append(" ");
                    }
                }
                sb.AppendLine();
                if (i % 3 == 2)
                {
                    sb.AppendLine();
                }
            }
            return sb.ToString();
        }

        public void Clear()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Board[i][j].Value = 0;
                }
            }
        }
    }
}
