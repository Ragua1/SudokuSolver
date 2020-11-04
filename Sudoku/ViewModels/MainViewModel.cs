using System.Threading.Tasks;
using Sudoku.Base;
using Sudoku.Interfaces;

namespace Sudoku.ViewModels
{
    public class MainViewModel : ObservableObject, IViewModel
    {
        private static readonly object _lock = new object();
        private static MainViewModel _instance;
        private IViewModel _currentGameViewModel;
        private string _statusMsg;

        public IViewModel CurrentGameViewModel
        {
            get => _currentGameViewModel;
            set
            {
                if (_currentGameViewModel != value)
                {
                    _currentGameViewModel = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsGameEnabled));
                }
            }
        }

        public bool IsGameEnabled => CurrentGameViewModel != null;
        public string StatusMsg
        {
            get => _statusMsg;
            set
            {
                if (_statusMsg != value)
                {
                    _statusMsg = value;
                    OnPropertyChanged();
                }
            }
        }

        public static MainViewModel GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new MainViewModel();
                        _instance.InitClass();
                    }
                }
            }

            return _instance;
        }

        internal void NewGame()
        {
            CurrentGameViewModel = new GameViewModel();
            StatusMsg = "New game.";
        }

        internal async Task SolveGame()
        {
            if (CurrentGameViewModel is GameViewModel viewModel)
            {
                StatusMsg = await viewModel.Solve()
                    ? "Game solved."
                    : "Game cannot be solved.";
            }
        }

        internal async Task SaveGame()
        {
            if (CurrentGameViewModel is GameViewModel viewModel)
            {
                StatusMsg = await viewModel.Save()
                    ? "Game saved."
                    : "Game cannot be saved.";
            }
        }

        internal async Task LoadGame()
        {
            NewGame();

            if (CurrentGameViewModel is GameViewModel viewModel)
            {
                StatusMsg = await viewModel.Load() 
                        ? "Game loaded." 
                        : "Game cannot be loaded.";
            }
        }

        private void InitClass()
        {
        }
    }
}