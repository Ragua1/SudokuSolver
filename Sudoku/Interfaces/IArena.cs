using System.Threading.Tasks;
using Sudoku.Base;
using Sudoku.DataClasses;

namespace Sudoku.Interfaces
{
    public interface IArena
    {
        int GridSize { get; }
        int RegionSize { get; }

        CellClass[,] Model { get; set; }
        CellClass[] Cells { get; }

        IArena Clone();
        Task<bool> SolveAsync();
        Task<bool> SaveAsync();
        Task<bool> LoadAsync();
    }
}