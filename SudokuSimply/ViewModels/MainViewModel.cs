using System.Linq;
using System.Threading.Tasks;
using SudokuSimply.Base;
using SudokuSimply.DataClasses;
using SudokuSimply.Interfaces;

namespace SudokuSimply.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private string _statusMsg;
        private IArena _arena;

        public static readonly MainViewModel Instance = new MainViewModel();
        public static string CellsPropertyName => nameof(Cells);

        public CellClass[] Cells => Arena?.Model.Cast<CellClass>().ToArray();

        private IArena Arena
        {
            get => _arena;
            set
            {
                if (_arena != value)
                {
                    _arena = value;
                    OnPropertyChanged(nameof(Cells));
                    OnPropertyChanged(nameof(IsGameEnabled));
                }
            }
        }

        public bool IsGameEnabled => Arena != null;
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
            Arena = new SudokuArena();
            StatusMsg = "New game.";
        }

        internal async Task SolveGameAsync()
        {
            if (Arena != null)
            {
                StatusMsg = "Trying find a solution.";
                var gameModel = Arena.Clone();

                if (await gameModel.SolveAsync().ConfigureAwait(true))
                {
                    StatusMsg = "Game solved.";
                    Arena = gameModel;
                }
                else
                {
                    StatusMsg = "Game cannot be solved.";
                }
            }
        }

        internal async Task SaveGameAsync()
        {
            if (Arena != null)
            {
                StatusMsg = "Select file.";

                StatusMsg = await Arena.SaveAsync().ConfigureAwait(true)
                    ? "Game saved."
                    : "Game cannot be saved.";
            }
        }

        internal async Task LoadGameAsync()
        {
            NewGame();

            if (Arena != null)
            {
                StatusMsg = "Select file.";

                StatusMsg = await Arena.LoadAsync().ConfigureAwait(true) 
                        ? "Game loaded." 
                        : "Game cannot be loaded.";
            }
        }
    }
}