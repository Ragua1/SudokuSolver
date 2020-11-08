using Sudoku.Base;

namespace Sudoku.DataClasses
{
    public class SudokuArena
    {
        public CellClass[,] Model { get; set; }

        public SudokuArena()
        {
            Model = new[,]
            {
                { new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass() },
                { new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass() },
                { new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass() },

                { new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass() },
                { new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass() },
                { new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass() },

                { new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass() },
                { new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass() },
                { new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass(), new CellClass() },
            };
        }

        public SudokuArena Clone()
        {
            var clone = new SudokuArena();

            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    clone.Model[i, j] = new CellClass { Value = Model[i, j].Value };
                }
            }

            return clone;
        }
    }
}