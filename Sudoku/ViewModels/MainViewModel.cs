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
                    _instance ??= new MainViewModel();
                }
            }

            return _instance;
        }

        internal void NewGame()
        {
            CurrentGameViewModel = new GameViewModel();
            StatusMsg = "New game.";
        }

        internal async Task SolveGameAsync()
        {
            if (CurrentGameViewModel is GameViewModel viewModel)
            {
                StatusMsg = "Trying find a solution.";
                var gameModel = new GameViewModel(viewModel.Arena.Clone());

                if (await gameModel.SolveAsync().ConfigureAwait(true))
                {
                    StatusMsg = "Game solved.";
                    CurrentGameViewModel = gameModel;
                }
                else
                {
                    StatusMsg = "Game cannot be solved.";
                }
            }
        }

        internal async Task SaveGameAsync()
        {
            if (CurrentGameViewModel is GameViewModel viewModel)
            {
                StatusMsg = "Select file.";

                StatusMsg = await viewModel.SaveAsync().ConfigureAwait(true)
                    ? "Game saved."
                    : "Game cannot be saved.";
            }
        }

        internal async Task LoadGameAsync()
        {
            NewGame();

            if (CurrentGameViewModel is GameViewModel viewModel)
            {
                StatusMsg = "Select file.";

                StatusMsg = await viewModel.LoadAsync().ConfigureAwait(true) 
                        ? "Game loaded." 
                        : "Game cannot be loaded.";
            }
        }
    }
}