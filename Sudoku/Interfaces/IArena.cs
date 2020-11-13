using System.Threading.Tasks;
using Sudoku.Base;
using Sudoku.DataClasses;
using Sudoku.Enums;

namespace Sudoku.Interfaces
{
    public interface IArena
    {
        int GridSize { get; }
        int RegionSize { get; }

        CellClass[,] Model { get; set; }
        CellClass[] Cells { get; }

        CellValue GetValue(int row, int col);
        void SetValue(int row, int col, CellValue value = CellValue.None);

        IArena Clone();
        Task<bool> SolveAsync();
        Task<bool> SaveAsync();
        Task<bool> LoadAsync();
    }
}