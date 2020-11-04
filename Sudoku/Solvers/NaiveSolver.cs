using System.Threading.Tasks;
using Sudoku.Base;
using Sudoku.Enums;

namespace Sudoku.Solvers
{
    public class NaiveSolver
    {
        private const int MATRIX_SIZE = 9;

        public CellClass[,] Solution { get; set; }

        public async Task<bool> Resolve(CellClass[,] model)
        {
            var arena = (CellClass[,]) model.Clone();

            if (!CheckModel(model))
            {
                return false;
            }

            if (SolveSudoku(arena, 0, 0))
            {
                Solution = arena;
                return await Task.FromResult(true);
            }

            return false;
        }

        private static bool SolveSudoku(CellClass[,] model, int row, int col)
        {
            while (true)
            {
                // avoid backtracking
                if (row == MATRIX_SIZE - 1 && col == MATRIX_SIZE)
                {
                    return true;
                }

                // set new row if end of line
                if (col == MATRIX_SIZE)
                {
                    row++;
                    col = 0;
                }

                // move next if value founded
                if (model[col, row].Value != 0)
                {
                    col += 1;
                    continue;
                }

                for (var num = CellValue.V1; num <= CellValue.V9; num++)
                {
                    // check value possibility
                    if (CheckModel(model, row, col, num))
                    {
                        model[col, row].Value = num;

                        // Check next positions
                        if (SolveSudoku(model, row, col + 1))
                        {
                            return true;
                        }
                    }

                    // remove the assigned num 
                    model[col, row].Value = 0;
                }

                return false;
            }
        }

        private static bool CheckModel(CellClass[,] model)
        {
            for (var row = 0; row < MATRIX_SIZE; row++)
            {
                for (var col = 0; col < MATRIX_SIZE; col++)
                {
                    var num = model[col, row].Value;

                    if (num > 0 && !CheckModel(model, row, col, num))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool CheckModel(CellClass[,] model, int row, int col, CellValue num)
        {
            // check row for same value
            for (var x = 0; x < 8; x++)
            {
                if (x != col && model[x, row].Value == num)
                {
                    return false;
                }
            }

            // check column for same value
            for (var x = 0; x <= 8; x++)
            {
                if (x != row && model[col, x].Value == num)
                {
                    return false;
                }
            }

            // check matrix for same value
            int startRow = row - row % 3, startCol = col - col % 3;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    int cRow = j + startRow, cCol = i + startCol;
                    if (model[cCol, cRow].Value == num)
                    {
                        if (cCol == col && cRow == row)
                        {
                            continue;
                        }

                        return false;
                    }
                }
            }

            return true;
        }
    }
}