using System.Windows;
using Sudoku.ViewModels;

namespace Sudoku
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _viewModel = MainViewModel.Instance;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            _viewModel?.NewGame();
        }

        private async void Solve_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                await _viewModel.SolveGameAsync().ConfigureAwait(true);
            }
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                await _viewModel.SaveGameAsync().ConfigureAwait(true);
            }
        }

        private async void Load_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                await _viewModel.LoadGameAsync().ConfigureAwait(true);
            }
        }
    }
}
