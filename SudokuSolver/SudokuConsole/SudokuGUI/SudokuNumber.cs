using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGUI
{
    public class SudokuNumber : INotifyPropertyChanged
    {
        private int? value;
        public int? Value
        {
            get { return value; }
            set
            {
                if (value != null && value > 0)
                {
                    this.value = Math.Max(1, Math.Min(9, value.Value));
                }
                else
                {
                    this.value = null;
                }
                NotifyPropertyChanged();
            }
        }
        public SudokuNumber()
        {
        }

        public override string ToString()
        {
            return Value != null ? Value.ToString() : "";
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
    }
}
