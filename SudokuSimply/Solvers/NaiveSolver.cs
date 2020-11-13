using System;
using System.Linq;
using System.Threading.Tasks;
using SudokuSimply.Base;
using SudokuSimply.Interfaces;

namespace SudokuSimply.Solvers
{
    public class NaiveSolver : ISolver
    {
        private static readonly Random _rand = new Random();
        
        public Task<bool> ResolveAsync(IArena arena)
        {
            return Task.Run(() => CheckModel(arena) && SolveSudoku(arena, 0, 0));
        }

        private static bool SolveSudoku(IArena arena, int row, int col)
        {
            //Thread.Sleep(1);
            while (true)
            {
                // avoid backtracking
                if (row == arena.GridSize - 1 && col == arena.GridSize)
                {
                    return true;
                }

                // set new row if end of line
                if (col == arena.GridSize)
                {
                    row++;
                    col = 0;
                }

                // move next if value founded
                if (arena.GetValue(row, col) != Constants.ORIG_SUDOKU_EMPTY_VALUE)
                {
                    col += 1;
                    continue;
                }


                var values = Enumerable.Range(Constants.ORIG_SUDOKU_MIN_VALUE, Constants.ORIG_SUDOKU_MAX_VALUE).ToList();
                while (values.Any())
                {
                    var num = (byte) values[_rand.Next(values.Count)];

                    // check value possibility
                    if (CheckModel(arena, row, col, num))
                    {
                        arena.SetValue(row, col, num);

                        // Check next positions
                        if (SolveSudoku(arena, row, col + 1))
                        {
                            return true;
                        }
                    }

                    // remove the assigned num 
                    values.Remove(num);
                    arena.SetValue(row, col);
                }

                return false;
            }
        }

        // check all model
        private static bool CheckModel(IArena arena)
        {
            for (var row = 0; row < arena.GridSize; row++)
            {
                for (var col = 0; col < arena.GridSize; col++)
                {
                    var num = arena.GetValue(row, col);

                    if (num > 0 && !CheckModel(arena, row, col, num))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        // check cell
        private static bool CheckModel(IArena arena, int row, int col, int num)
        {
            // check row for same value
            for (var x = 0; x < arena.GridSize; x++)
            {
                if (x != col && arena.GetValue(row, x) == num)
                {
                    return false;
                }
            }

            // check column for same value
            for (var x = 0; x < arena.GridSize; x++)
            {
                if (x != row && arena.GetValue(x, col) == num)
                {
                    return false;
                }
            }

            // check matrix for same value
            int startRow = row - row % arena.RegionSize, startCol = col - col % arena.RegionSize;
            for (var i = 0; i < arena.RegionSize; i++)
            {
                for (var j = 0; j < arena.RegionSize; j++)
                {
                    int cRow = i + startRow, cCol = j + startCol;
                    if (arena.GetValue(cRow, cCol) == num)
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