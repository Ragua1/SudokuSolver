using System.Linq;
using System.Threading;
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
        private CancellationTokenSource _tokenSource;
        private object _lock = new object();

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
                    OnPropertyChanged(nameof(IsGameEnabled));
                    if (value != null)
                    {
                        OnPropertyChanged(nameof(Cells));
                    }
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
            ResetToken(false);
            SetArena(GetArena(true));
            StatusMsg = "New game.";
        }

        internal async Task SolveGameAsync()
        {
            var arena = GetArena();
            if (arena != null)
            {
                ResetToken();
                SetArena(null);

                StatusMsg = "Trying find a solution.";
                var newArena = arena.Clone();

                if (await newArena.SolveAsync(_tokenSource.Token).ConfigureAwait(true))
                {
                    StatusMsg = "Game solved.";
                    arena = newArena;
                }
                else
                {
                    StatusMsg = "Game cannot be solved.";
                }

                if (GetArena() == null)
                {
                    SetArena(arena);
                }
            }
        }

        internal async Task SaveGameAsync()
        {
            var arena = GetArena();
            if (arena != null)
            {
                ResetToken();
                StatusMsg = "Select file.";

                StatusMsg = await Arena.SaveAsync(_tokenSource.Token).ConfigureAwait(true)
                    ? "Game saved."
                    : "Game cannot be saved.";
            }
        }

        internal async Task LoadGameAsync()
        {
            ResetToken();
            var arena = GetArena(true);
            StatusMsg = "Select file.";

            var res = await arena.LoadAsync(_tokenSource.Token).ConfigureAwait(true);
            
            if (res)
            {
                SetArena(arena);
                StatusMsg = "Game loaded.";
            }
            else
            {
                StatusMsg = "Game cannot be loaded.";
            }
        }

        private void ResetToken(bool newToken = true)
        {
            _tokenSource?.Cancel();

            _tokenSource = newToken ? new CancellationTokenSource() : null;
        }

        private IArena GetArena(bool @new = false)
        {
            lock (_lock)
            {
                return @new 
                    ? new SudokuArena() 
                    : Arena;
            }
        }

        private void SetArena(IArena arena)
        {
            lock (_lock)
            {
                Arena = arena;
            }
        }
    }
}