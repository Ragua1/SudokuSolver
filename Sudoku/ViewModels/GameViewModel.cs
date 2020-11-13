using System.Linq;
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

        internal Task<bool> SolveAsync()
        {
            return _arena.SolveAsync();
        }

        internal Task<bool> SaveAsync()
        {
            return _arena.SaveAsync();
        }

        internal Task<bool> LoadAsync()
        {
            return _arena.LoadAsync();
        }

        internal GameViewModel Clone()
        {
            return new GameViewModel(_arena.Clone());
        }
    }
}