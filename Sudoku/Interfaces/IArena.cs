using System.Threading;
using System.Threading.Tasks;
using Sudoku.Base;
using Sudoku.Enums;

namespace Sudoku.Interfaces
{
    public interface IArena
    {
        int GridSize { get; }
        int RegionSize { get; }

        CellClass[,] Model { get; set; }

        CellValue GetValue(int row, int col);
        void SetValue(int row, int col, CellValue value = CellValue.None);

        IArena Clone();
        Task<bool> SolveAsync(CancellationToken token = default);
        Task<bool> SaveAsync(CancellationToken token = default);
        Task<bool> LoadAsync(CancellationToken token = default);
    }
}