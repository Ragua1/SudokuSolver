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
            ViewModel = MainViewModel.Instance;
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

        private async void Solve_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                await ViewModel.SolveGameAsync().ConfigureAwait(true);
            }
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                await ViewModel.SaveGameAsync().ConfigureAwait(true);
            }
        }

        private async void Load_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                await ViewModel.LoadGameAsync().ConfigureAwait(true);
            }
        }
    }
}
