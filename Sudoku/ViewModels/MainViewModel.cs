using System.Threading;
using System.Threading.Tasks;
using Sudoku.Base;

namespace Sudoku.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private GameViewModel _currentGameViewModel;
        private string _statusMsg;
        private CancellationTokenSource _tokenSource;

        public static readonly MainViewModel Instance = new MainViewModel();

        public GameViewModel CurrentGameViewModel
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

        internal void NewGame()
        {
            ResetToken(false);
            CurrentGameViewModel = new GameViewModel();
            StatusMsg = "New game.";
        }

        internal async Task SolveGameAsync()
        {
            if (CurrentGameViewModel != null)
            {
                ResetToken();
                StatusMsg = "Trying find a solution.";
                var gameModel = CurrentGameViewModel.Clone();

                if (await gameModel.SolveAsync(_tokenSource.Token).ConfigureAwait(true))
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
            if (CurrentGameViewModel != null)
            {
                ResetToken();
                StatusMsg = "Select file.";

                StatusMsg = await CurrentGameViewModel.SaveAsync(_tokenSource.Token).ConfigureAwait(true)
                    ? "Game saved."
                    : "Game cannot be saved.";
            }
        }

        internal async Task LoadGameAsync()
        {
            ResetToken();
            NewGame();

            if (CurrentGameViewModel != null)
            {
                StatusMsg = "Select file.";

                StatusMsg = await CurrentGameViewModel.LoadAsync(_tokenSource.Token).ConfigureAwait(true) 
                        ? "Game loaded." 
                        : "Game cannot be loaded.";
            }
        }

        private void ResetToken(bool newToken = true)
        {
            _tokenSource?.Cancel();

            _tokenSource = newToken ? new CancellationTokenSource() : null;
        }
    }
}