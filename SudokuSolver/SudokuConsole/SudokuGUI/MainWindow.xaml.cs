using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SudokuGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public SudokuBoard Board { get; set; }
        public Stopwatch SW { get; set; }
        
        private SudokuSolver solver;
        private Timer time;
        private Task t;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            Board = new SudokuBoard();
            solver = new SudokuSolver(Board);
            SW = new Stopwatch();
            solver.solved += puzzleSolved;
            gridBoard.DataContext = Board;
            sliderDelay.DataContext = solver;
            textBlockBacktrack.DataContext = solver;
            labelDelay.DataContext = solver;
            textBlockStopwatch.DataContext = this;
        }

        private void TimePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.PropertyChanged?.Invoke(sender, e);
        }

        private void buttonSolve_Click(object sender, RoutedEventArgs e)
        {
            if (solver.ValidateBoard())
            {
                SW.Reset();
                solver.BacktrackCount = 0;
                SW.Start();
                t = new Task(solver.Solve);
                t.Start();
                time = new Timer(new TimerCallback((s) => TimePropertyChanged(this, new PropertyChangedEventArgs("SW"))), null,0, 100);
            }
            else
            {
                MessageBox.Show("Not a valid sudoku board");
            }           
        }

        private void puzzleSolved(object sender, EventArgs e)
        {
            SW.Stop();
            time.Dispose();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Board.Clear();
        }

        private void btnExOne_Click(object sender, RoutedEventArgs e)
        {
            RunExample(" 2 5 1 9 \n8  2 3  6\n 3  6  7 \n  1   6  \n54     19\n  2   7  \n 9  3  8 \n2  8 4  7\n 1 9 7 6 ");
        }

        private void btnExTwo_Click(object sender, RoutedEventArgs e)
        {
            RunExample("85   24  \n72      9\n  4      \n   1 7  2\n3 5   9  \n 4       \n    8  7 \n 17      \n    36 4 ");
        }

        private void btnExThree_Click(object sender, RoutedEventArgs e)
        {
            RunExample("    376  \n   6   9 \n  8     4\n 9      1\n6       9\n3      4 \n7     8  \n 1   9   \n  254    ");
        }

        private void RunExample(string sudokuString)
        {
            Board.Clear();
            Dictionary<Coord, int> coordValues = SudokuFormatter.Format(sudokuString);
            foreach (KeyValuePair<Coord, int> coordValue in coordValues)
            {
                Board[coordValue.Key.X, coordValue.Key.Y].Value = coordValue.Value;
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = null;
        }

        private void buttonSolveDynamic_Click(object sender, RoutedEventArgs e)
        {
            if (solver.ValidateBoard())
            {
                SW.Reset();
                SW.Start();
                solver.BacktrackCount = 0;
                t = new Task(solver.SolveDyn);
                t.Start();
                time = new Timer(new TimerCallback((s) => TimePropertyChanged(this, new PropertyChangedEventArgs("SW"))), null, 0, 100);
            }
            else
            {
                MessageBox.Show("Not a valid sudoku board");
            }
        }
    }
}
