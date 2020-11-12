using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sudoku.Enums;
using Sudoku.Interfaces;

namespace Sudoku.Solvers
{
    public class NaiveSolver : ISolver
    {
        private static readonly Random _rand = new Random();

        public IArena Solution { get; set; }

        public async Task<bool> ResolveAsync(IArena arena)
        {
            var checkTask = Task.Run(() => !CheckModel(arena));
            var solveTask = Task.Run(() => SolveSudoku(arena, 0, 0));

            if (await checkTask.ConfigureAwait(false))
            {
                return false;
            }

            if (await solveTask.ConfigureAwait(false))
            {
                Solution = arena;
                return true;
            }

            return false;
        }

        private static bool SolveSudoku(IArena arena, int row, int col)
        {
            Thread.Sleep(1);
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
                if (arena.Model[col, row].Value != CellValue.None)
                {
                    col += 1;
                    continue;
                }

                var values = Enum.GetValues(typeof(CellValue)).Cast<CellValue>().Where(e => e > CellValue.None).ToList();
                while (values.Any())
                {
                    var num = values[_rand.Next(values.Count)];
                    var cell = arena.Model[col, row];

                    // check value possibility
                    if (CheckModel(arena, row, col, num))
                    {
                        cell.Value = num;

                        // Check next positions
                        if (SolveSudoku(arena, row, col + 1))
                        {
                            return true;
                        }
                    }

                    // remove the assigned num 
                    values.Remove(num);
                    cell.Value = CellValue.None;
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
        private static bool CheckModel(IArena arena, int row, int col, CellValue num)
        {
            // check row for same value
            for (var x = 0; x < arena.GridSize; x++)
            {
                if (x != col && arena.Model[x, row].Value == num)
                {
                    return false;
                }
            }

            // check column for same value
            for (var x = 0; x < arena.GridSize; x++)
            {
                if (x != row && arena.Model[col, x].Value == num)
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