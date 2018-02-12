using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class SudokuBoard : INotifyPropertyChanged
    {
        private int[][] board;
        
        public int Size { get; }

        public SudokuBoard()
        {
            board = new int[9][];

            for (int i = 0; i < 9; i++)
            {
                board[i] = new int[9];
            }
            Size = 9;
        }

        public int this[int x, int y]
        {
            get { return board[x][y]; }
            set
            {
                board[x][y] = value;
                NotifyPropertyChanged();
            }
        }

        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    sb.Append("[" + (this[i, j] != 0 ? this[i, j].ToString() : " ") + "]");
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
    }
}
