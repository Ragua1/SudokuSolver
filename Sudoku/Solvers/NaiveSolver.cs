using System;
using System.Linq;
using System.Threading.Tasks;
using Sudoku.DataClasses;
using Sudoku.Enums;

namespace Sudoku.Solvers
{
    public class NaiveSolver
    {
        private const int MATRIX_SIZE = 9;

        private static readonly Random _rand = new Random();

        public SudokuArena Solution { get; set; }

        public async Task<bool> ResolveAsync(SudokuArena arena)
        {

            if (!CheckModel(arena))
            {
                return false;
            }

            if (SolveSudoku(arena, 0, 0))
            {
                Solution = arena;
                return await Task.FromResult(true).ConfigureAwait(true);
            }

            return false;
        }

        private static bool SolveSudoku(SudokuArena arena, int row, int col)
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
                if (arena.Model[col, row].Value != CellValue.None)
                {
                    col += 1;
                    continue;
                }

                var values = Enum.GetValues(typeof(CellValue)).Cast<CellValue>().Where(e => e > CellValue.None).ToList();
                while (values.Any())
                {
                    var num = values[_rand.Next(values.Count)];

                    // check value possibility
                    if (CheckModel(arena, row, col, num))
                    {
                        arena.Model[col, row].Value = num;

                        // Check next positions
                        if (SolveSudoku(arena, row, col + 1))
                        {
                            return true;
                        }
                    }

                    // remove the assigned num 
                    values.Remove(num);
                    arena.Model[col, row].Value = CellValue.None;
                }

                //for (var num = CellValue.V1; num <= CellValue.V9; num++)
                //{
                //    // check value possibility
                //    if (CheckModel(arena, row, col, num))
                //    {
                //        arena.Model[col, row].Value = num;

                //        // Check next positions
                //        if (SolveSudoku(arena, row, col + 1))
                //        {
                //            return true;
                //        }
                //    }

                //    // remove the assigned num 
                //    arena.Model[col, row].Value = CellValue.None;
                //}

                return false;
            }
        }

        // check all model
        private static bool CheckModel(SudokuArena arena)
        {
            for (var row = 0; row < MATRIX_SIZE; row++)
            {
                for (var col = 0; col < MATRIX_SIZE; col++)
                {
                    var num = arena.Model[col, row].Value;

                    if (num > 0 && !CheckModel(arena, row, col, num))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        // check cell
        private static bool CheckModel(SudokuArena arena, int row, int col, CellValue num)
        {
            // check row for same value
            for (var x = 0; x <= 8; x++)
            {
                if (x != col && arena.Model[x, row].Value == num)
                {
                    return false;
                }
            }

            // check column for same value
            for (var x = 0; x <= 8; x++)
            {
                if (x != row && arena.Model[col, x].Value == num)
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
                    if (arena.Model[cCol, cRow].Value == num)
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