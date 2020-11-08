using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Sudoku.Base;
using Sudoku.DataClasses;
using Sudoku.Enums;
using Sudoku.Interfaces;
using Sudoku.Solvers;

namespace Sudoku.ViewModels
{
    public class GameViewModel : ObservableObject, IViewModel
    {
        public readonly SudokuArena Arena;

        public GameViewModel(SudokuArena arena = null)
        {
            Arena = arena ?? new SudokuArena();
        }

        public Task<bool> SolveAsync()
        {
            var naiveSolver = new NaiveSolver();

            return naiveSolver.ResolveAsync(Arena);
        }

        public async Task<bool> SaveAsync()
        {
            var filepath = SelectFile(true);

            if (string.IsNullOrEmpty(filepath))
            {
                return false;
            }

            var builder = new StringBuilder();

            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    if (Arena.Model[i, j].Value == CellValue.None)
                    {
                        builder.Append(',');
                    }
                    else
                    {
                        builder.Append($"{(short)Arena.Model[i, j].Value},");
                    }
                }
            }

            builder.Length--;

            await File.WriteAllTextAsync(filepath, builder.ToString()).ConfigureAwait(false);

            return true;
        }

        public async Task<bool> LoadAsync()
        {
            var filepath = SelectFile();

            if (string.IsNullOrEmpty(filepath) || !File.Exists(filepath))
            {
                return false;
            }

            var text = await File.ReadAllTextAsync(filepath).ConfigureAwait(false);

            var data = text?.Split(',');

            if (data == null || data.Length != 81)
            {
                return false;
            }

            var d = 0;
            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    Arena.Model[i, j].Value = Enum.TryParse(data[d++], out CellValue value) 
                        ? value 
                        : CellValue.None;
                }
            }

            return true;
        }

        #region Cell classes
        // ReSharper disable UnusedMember.Global
        public CellClass Cell00 => Arena.Model[0, 0];
        public CellClass Cell10 => Arena.Model[1, 0];
        public CellClass Cell20 => Arena.Model[2, 0];
        public CellClass Cell30 => Arena.Model[3, 0];
        public CellClass Cell40 => Arena.Model[4, 0];
        public CellClass Cell50 => Arena.Model[5, 0];
        public CellClass Cell60 => Arena.Model[6, 0];
        public CellClass Cell70 => Arena.Model[7, 0];
        public CellClass Cell80 => Arena.Model[8, 0];
        public CellClass Cell01 => Arena.Model[0, 1];
        public CellClass Cell11 => Arena.Model[1, 1];
        public CellClass Cell21 => Arena.Model[2, 1];
        public CellClass Cell31 => Arena.Model[3, 1];
        public CellClass Cell41 => Arena.Model[4, 1];
        public CellClass Cell51 => Arena.Model[5, 1];
        public CellClass Cell61 => Arena.Model[6, 1];
        public CellClass Cell71 => Arena.Model[7, 1];
        public CellClass Cell81 => Arena.Model[8, 1];
        public CellClass Cell02 => Arena.Model[0, 2];
        public CellClass Cell12 => Arena.Model[1, 2];
        public CellClass Cell22 => Arena.Model[2, 2];
        public CellClass Cell32 => Arena.Model[3, 2];
        public CellClass Cell42 => Arena.Model[4, 2];
        public CellClass Cell52 => Arena.Model[5, 2];
        public CellClass Cell62 => Arena.Model[6, 2];
        public CellClass Cell72 => Arena.Model[7, 2];
        public CellClass Cell82 => Arena.Model[8, 2];
        public CellClass Cell03 => Arena.Model[0, 3];
        public CellClass Cell13 => Arena.Model[1, 3];
        public CellClass Cell23 => Arena.Model[2, 3];
        public CellClass Cell33 => Arena.Model[3, 3];
        public CellClass Cell43 => Arena.Model[4, 3];
        public CellClass Cell53 => Arena.Model[5, 3];
        public CellClass Cell63 => Arena.Model[6, 3];
        public CellClass Cell73 => Arena.Model[7, 3];
        public CellClass Cell83 => Arena.Model[8, 3];
        public CellClass Cell04 => Arena.Model[0, 4];
        public CellClass Cell14 => Arena.Model[1, 4];
        public CellClass Cell24 => Arena.Model[2, 4];
        public CellClass Cell34 => Arena.Model[3, 4];
        public CellClass Cell44 => Arena.Model[4, 4];
        public CellClass Cell54 => Arena.Model[5, 4];
        public CellClass Cell64 => Arena.Model[6, 4];
        public CellClass Cell74 => Arena.Model[7, 4];
        public CellClass Cell84 => Arena.Model[8, 4];
        public CellClass Cell05 => Arena.Model[0, 5];
        public CellClass Cell15 => Arena.Model[1, 5];
        public CellClass Cell25 => Arena.Model[2, 5];
        public CellClass Cell35 => Arena.Model[3, 5];
        public CellClass Cell45 => Arena.Model[4, 5];
        public CellClass Cell55 => Arena.Model[5, 5];
        public CellClass Cell65 => Arena.Model[6, 5];
        public CellClass Cell75 => Arena.Model[7, 5];
        public CellClass Cell85 => Arena.Model[8, 5];
        public CellClass Cell06 => Arena.Model[0, 6];
        public CellClass Cell16 => Arena.Model[1, 6];
        public CellClass Cell26 => Arena.Model[2, 6];
        public CellClass Cell36 => Arena.Model[3, 6];
        public CellClass Cell46 => Arena.Model[4, 6];
        public CellClass Cell56 => Arena.Model[5, 6];
        public CellClass Cell66 => Arena.Model[6, 6];
        public CellClass Cell76 => Arena.Model[7, 6];
        public CellClass Cell86 => Arena.Model[8, 6];
        public CellClass Cell07 => Arena.Model[0, 7];
        public CellClass Cell17 => Arena.Model[1, 7];
        public CellClass Cell27 => Arena.Model[2, 7];
        public CellClass Cell37 => Arena.Model[3, 7];
        public CellClass Cell47 => Arena.Model[4, 7];
        public CellClass Cell57 => Arena.Model[5, 7];
        public CellClass Cell67 => Arena.Model[6, 7];
        public CellClass Cell77 => Arena.Model[7, 7];
        public CellClass Cell87 => Arena.Model[8, 7];
        public CellClass Cell08 => Arena.Model[0, 8];
        public CellClass Cell18 => Arena.Model[1, 8];
        public CellClass Cell28 => Arena.Model[2, 8];
        public CellClass Cell38 => Arena.Model[3, 8];
        public CellClass Cell48 => Arena.Model[4, 8];
        public CellClass Cell58 => Arena.Model[5, 8];
        public CellClass Cell68 => Arena.Model[6, 8];
        public CellClass Cell78 => Arena.Model[7, 8];
        public CellClass Cell88 => Arena.Model[8, 8];
        // ReSharper restore UnusedMember.Global
        #endregion

        private static string SelectFile(bool saveFile = false)
        {
            FileDialog fileDialog;

            if (saveFile)
            {
                fileDialog = new SaveFileDialog()
                {
                    FileName = "sudoku",
                    DefaultExt = "save",
                    Filter = "Save files (*.save)|*.save",
                    RestoreDirectory = true,
                };
            }
            else
            {
                fileDialog = new OpenFileDialog()
                {
                    Filter = "Save files (*.save)|*.save",
                    RestoreDirectory = true,
                    CheckFileExists = true,
                    CheckPathExists = true,
                };
            }


            return fileDialog.ShowDialog() == true ? fileDialog.FileName : null;
        }
    }
}