using System.Threading.Tasks;
using Sudoku.Base;
using Sudoku.DataClasses;
using Sudoku.Interfaces;

namespace Sudoku.ViewModels
{
    public class GameViewModel : ObservableObject
    {
        private readonly IArena _arena;

        public CellClass[] Cells => _arena.Cells;

        public GameViewModel(IArena arena = null)
        {
            this._arena = arena ?? new SudokuArena();
        }

        public Task<bool> SolveAsync()
        {
            return _arena.SolveAsync();
        }

        public Task<bool> SaveAsync()
        {
            return _arena.SaveAsync();
        }

        public Task<bool> LoadAsync()
        {
            return _arena.LoadAsync();
        }

        #region Cell classes
        // ReSharper disable UnusedMember.Global
        public CellClass Cell00 => _arena.Model[0, 0];
        public CellClass Cell10 => _arena.Model[1, 0];
        public CellClass Cell20 => _arena.Model[2, 0];
        public CellClass Cell30 => _arena.Model[3, 0];
        public CellClass Cell40 => _arena.Model[4, 0];
        public CellClass Cell50 => _arena.Model[5, 0];
        public CellClass Cell60 => _arena.Model[6, 0];
        public CellClass Cell70 => _arena.Model[7, 0];
        public CellClass Cell80 => _arena.Model[8, 0];
        public CellClass Cell01 => _arena.Model[0, 1];
        public CellClass Cell11 => _arena.Model[1, 1];
        public CellClass Cell21 => _arena.Model[2, 1];
        public CellClass Cell31 => _arena.Model[3, 1];
        public CellClass Cell41 => _arena.Model[4, 1];
        public CellClass Cell51 => _arena.Model[5, 1];
        public CellClass Cell61 => _arena.Model[6, 1];
        public CellClass Cell71 => _arena.Model[7, 1];
        public CellClass Cell81 => _arena.Model[8, 1];
        public CellClass Cell02 => _arena.Model[0, 2];
        public CellClass Cell12 => _arena.Model[1, 2];
        public CellClass Cell22 => _arena.Model[2, 2];
        public CellClass Cell32 => _arena.Model[3, 2];
        public CellClass Cell42 => _arena.Model[4, 2];
        public CellClass Cell52 => _arena.Model[5, 2];
        public CellClass Cell62 => _arena.Model[6, 2];
        public CellClass Cell72 => _arena.Model[7, 2];
        public CellClass Cell82 => _arena.Model[8, 2];
        public CellClass Cell03 => _arena.Model[0, 3];
        public CellClass Cell13 => _arena.Model[1, 3];
        public CellClass Cell23 => _arena.Model[2, 3];
        public CellClass Cell33 => _arena.Model[3, 3];
        public CellClass Cell43 => _arena.Model[4, 3];
        public CellClass Cell53 => _arena.Model[5, 3];
        public CellClass Cell63 => _arena.Model[6, 3];
        public CellClass Cell73 => _arena.Model[7, 3];
        public CellClass Cell83 => _arena.Model[8, 3];
        public CellClass Cell04 => _arena.Model[0, 4];
        public CellClass Cell14 => _arena.Model[1, 4];
        public CellClass Cell24 => _arena.Model[2, 4];
        public CellClass Cell34 => _arena.Model[3, 4];
        public CellClass Cell44 => _arena.Model[4, 4];
        public CellClass Cell54 => _arena.Model[5, 4];
        public CellClass Cell64 => _arena.Model[6, 4];
        public CellClass Cell74 => _arena.Model[7, 4];
        public CellClass Cell84 => _arena.Model[8, 4];
        public CellClass Cell05 => _arena.Model[0, 5];
        public CellClass Cell15 => _arena.Model[1, 5];
        public CellClass Cell25 => _arena.Model[2, 5];
        public CellClass Cell35 => _arena.Model[3, 5];
        public CellClass Cell45 => _arena.Model[4, 5];
        public CellClass Cell55 => _arena.Model[5, 5];
        public CellClass Cell65 => _arena.Model[6, 5];
        public CellClass Cell75 => _arena.Model[7, 5];
        public CellClass Cell85 => _arena.Model[8, 5];
        public CellClass Cell06 => _arena.Model[0, 6];
        public CellClass Cell16 => _arena.Model[1, 6];
        public CellClass Cell26 => _arena.Model[2, 6];
        public CellClass Cell36 => _arena.Model[3, 6];
        public CellClass Cell46 => _arena.Model[4, 6];
        public CellClass Cell56 => _arena.Model[5, 6];
        public CellClass Cell66 => _arena.Model[6, 6];
        public CellClass Cell76 => _arena.Model[7, 6];
        public CellClass Cell86 => _arena.Model[8, 6];
        public CellClass Cell07 => _arena.Model[0, 7];
        public CellClass Cell17 => _arena.Model[1, 7];
        public CellClass Cell27 => _arena.Model[2, 7];
        public CellClass Cell37 => _arena.Model[3, 7];
        public CellClass Cell47 => _arena.Model[4, 7];
        public CellClass Cell57 => _arena.Model[5, 7];
        public CellClass Cell67 => _arena.Model[6, 7];
        public CellClass Cell77 => _arena.Model[7, 7];
        public CellClass Cell87 => _arena.Model[8, 7];
        public CellClass Cell08 => _arena.Model[0, 8];
        public CellClass Cell18 => _arena.Model[1, 8];
        public CellClass Cell28 => _arena.Model[2, 8];
        public CellClass Cell38 => _arena.Model[3, 8];
        public CellClass Cell48 => _arena.Model[4, 8];
        public CellClass Cell58 => _arena.Model[5, 8];
        public CellClass Cell68 => _arena.Model[6, 8];
        public CellClass Cell78 => _arena.Model[7, 8];
        public CellClass Cell88 => _arena.Model[8, 8];
        // ReSharper restore UnusedMember.Global
        #endregion

        public GameViewModel Clone()
        {
            return new GameViewModel(_arena.Clone());
        }
    }
}