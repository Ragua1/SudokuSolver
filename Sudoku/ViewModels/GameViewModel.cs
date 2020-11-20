using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sudoku.Base;
using Sudoku.DataClasses;
using Sudoku.Interfaces;

namespace Sudoku.ViewModels
{
    public class GameViewModel : ObservableObject
    {
        private readonly IArena _arena;

        public CellClass[] Cells => _arena.Model.Cast<CellClass>().ToArray();

        public GameViewModel(IArena arena = null)
        {
            this._arena = arena ?? new SudokuArena();
        }

        internal Task<bool> SolveAsync(CancellationToken token = default)
        {
            return _arena.SolveAsync(token);
        }

        internal Task<bool> SaveAsync(CancellationToken token = default)
        {
            return _arena.SaveAsync(token);
        }

        internal Task<bool> LoadAsync(CancellationToken token = default)
        {
            return _arena.LoadAsync(token);
        }

        internal GameViewModel Clone()
        {
            return new GameViewModel(_arena.Clone());
        }
    }
}