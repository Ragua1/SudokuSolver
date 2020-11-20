using System.Threading;
using System.Threading.Tasks;
using SudokuSimply.Base;

namespace SudokuSimply.Interfaces
{
    public interface IArena
    {
        int GridSize { get; }
        int RegionSize { get; }

        CellClass[,] Model { get; }

        int GetValue(int row, int col);
        void SetValue(int row, int col, int value = Constants.ORIG_SUDOKU_EMPTY_VALUE);

        IArena Clone();
        Task<bool> SolveAsync(CancellationToken token = default);
        Task<bool> SaveAsync(CancellationToken token = default);
        Task<bool> LoadAsync(CancellationToken token = default);
    }
}