using System.Windows;
using Sudoku.ViewModels;

namespace Sudoku
{
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = MainViewModel.GetInstance();
        }

        internal MainViewModel ViewModel
        {
            get => _viewModel;
            set
            {
                _viewModel = value;
                this.DataContext = value;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.NewGame();
        }

        private void Solve_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.SolveGame();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.SaveGame();
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.LoadGame();
        }
    }
}
