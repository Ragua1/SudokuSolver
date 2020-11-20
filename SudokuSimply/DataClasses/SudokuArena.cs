using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SudokuSimply.Base;
using SudokuSimply.Interfaces;
using SudokuSimply.Solvers;

namespace SudokuSimply.DataClasses
{
    public class SudokuArena : IArena
    {
        private readonly ISolver _solver = new NaiveSolver();

        public int GridSize => Constants.ORIG_SUDOKU_GRID_SIZE;
        public int RegionSize => Constants.ORIG_SUDOKU_REGION_SIZE;

        public CellClass[,] Model { get; private set; }

        public SudokuArena()
        {
            Model = new CellClass[GridSize, GridSize];
            
            for (var i = 0; i < GridSize; i++)
            {
                for (var j = 0; j < GridSize; j++)
                {
                    Model[i, j] = new CellClass();
                }
            }
        }

        public int GetValue(int row, int col)
        {
            return Model[row, col].Value;
        }

        public void SetValue(int row, int col, int value = Constants.ORIG_SUDOKU_EMPTY_VALUE)
        {
            Model[row, col].Value = value;
        }

        public IArena Clone()
        {
            var clone = new SudokuArena();

            for (var i = 0; i < GridSize; i++)
            {
                for (var j = 0; j < GridSize; j++)
                {
                    clone.Model[i, j] = Model[i, j].Clone();
                }
            }

            return clone;
        }

        public Task<bool> SolveAsync(CancellationToken token = default)
        {
            return _solver.ResolveAsync(this, token);
        }

        public async Task<bool> SaveAsync(CancellationToken token = default)
        {
            var filepath = Common.SelectFile(true);

            if (string.IsNullOrEmpty(filepath))
            {
                return false;
            }

            var builder = new StringBuilder();

            for (var i = 0; i < GridSize; i++)
            {
                for (var j = 0; j < GridSize; j++)
                {
                    builder.Append($"{Common.GetTextValue(GetValue(i, j))},");
                }
            }

            builder.Length--;

            await File.WriteAllTextAsync(filepath, builder.ToString(), token).ConfigureAwait(false);

            return true;
        }

        public async Task<bool> LoadAsync(CancellationToken token = default)
        {
            var filepath = Common.SelectFile();

            if (string.IsNullOrEmpty(filepath) || !File.Exists(filepath))
            {
                return false;
            }

            var text = await File.ReadAllTextAsync(filepath, token).ConfigureAwait(false);

            var data = text?.Split(',');

            if (data == null || data.Length != GridSize*GridSize)
            {
                return false;
            }

            var d = 0;
            for (var i = 0; i < GridSize; i++)
            {
                for (var j = 0; j < GridSize; j++)
                {
                    SetValue(i, j, Common.GetCellValue(data[d++]));
                }
            }

            return true;
        }
    }
}