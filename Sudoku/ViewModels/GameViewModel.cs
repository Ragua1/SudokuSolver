using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Sudoku.Base;
using Sudoku.Enums;
using Sudoku.Interfaces;
using Sudoku.Solvers;

namespace Sudoku.ViewModels
{
    public class GameViewModel : ObservableObject, IViewModel
    {
        private readonly CellClass[,] _model;

        public GameViewModel()
        {
            _model = new [,]
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

        public async Task<bool> Solve()
        {
            var s = new NaiveSolver();

            var res = await s.Resolve(_model);

            return res;
        }

        public async Task<bool> Save()
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
                    if (_model[i, j].Value == CellValue.None)
                    {
                        builder.Append(',');
                    }
                    else
                    {
                        builder.Append($"{(short)_model[i, j].Value},");
                    }
                }
            }

            builder.Length--;

            await File.WriteAllTextAsync(filepath, builder.ToString());

            return true;
        }

        public async Task<bool> Load()
        {
            var filepath = SelectFile();

            if (string.IsNullOrEmpty(filepath) || !File.Exists(filepath))
            {
                return false;
            }

            var text = await File.ReadAllTextAsync(filepath);

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
                    _model[i, j].Value = Enum.TryParse(data[d++], out CellValue value) 
                        ? value 
                        : CellValue.None;
                }
            }

            return true;
        }

        #region Cell classes
        // ReSharper disable UnusedMember.Global
        public CellClass Cell00 => _model[0, 0];
        public CellClass Cell10 => _model[1, 0];
        public CellClass Cell20 => _model[2, 0];
        public CellClass Cell30 => _model[3, 0];
        public CellClass Cell40 => _model[4, 0];
        public CellClass Cell50 => _model[5, 0];
        public CellClass Cell60 => _model[6, 0];
        public CellClass Cell70 => _model[7, 0];
        public CellClass Cell80 => _model[8, 0];
        public CellClass Cell01 => _model[0, 1];
        public CellClass Cell11 => _model[1, 1];
        public CellClass Cell21 => _model[2, 1];
        public CellClass Cell31 => _model[3, 1];
        public CellClass Cell41 => _model[4, 1];
        public CellClass Cell51 => _model[5, 1];
        public CellClass Cell61 => _model[6, 1];
        public CellClass Cell71 => _model[7, 1];
        public CellClass Cell81 => _model[8, 1];
        public CellClass Cell02 => _model[0, 2];
        public CellClass Cell12 => _model[1, 2];
        public CellClass Cell22 => _model[2, 2];
        public CellClass Cell32 => _model[3, 2];
        public CellClass Cell42 => _model[4, 2];
        public CellClass Cell52 => _model[5, 2];
        public CellClass Cell62 => _model[6, 2];
        public CellClass Cell72 => _model[7, 2];
        public CellClass Cell82 => _model[8, 2];
        public CellClass Cell03 => _model[0, 3];
        public CellClass Cell13 => _model[1, 3];
        public CellClass Cell23 => _model[2, 3];
        public CellClass Cell33 => _model[3, 3];
        public CellClass Cell43 => _model[4, 3];
        public CellClass Cell53 => _model[5, 3];
        public CellClass Cell63 => _model[6, 3];
        public CellClass Cell73 => _model[7, 3];
        public CellClass Cell83 => _model[8, 3];
        public CellClass Cell04 => _model[0, 4];
        public CellClass Cell14 => _model[1, 4];
        public CellClass Cell24 => _model[2, 4];
        public CellClass Cell34 => _model[3, 4];
        public CellClass Cell44 => _model[4, 4];
        public CellClass Cell54 => _model[5, 4];
        public CellClass Cell64 => _model[6, 4];
        public CellClass Cell74 => _model[7, 4];
        public CellClass Cell84 => _model[8, 4];
        public CellClass Cell05 => _model[0, 5];
        public CellClass Cell15 => _model[1, 5];
        public CellClass Cell25 => _model[2, 5];
        public CellClass Cell35 => _model[3, 5];
        public CellClass Cell45 => _model[4, 5];
        public CellClass Cell55 => _model[5, 5];
        public CellClass Cell65 => _model[6, 5];
        public CellClass Cell75 => _model[7, 5];
        public CellClass Cell85 => _model[8, 5];
        public CellClass Cell06 => _model[0, 6];
        public CellClass Cell16 => _model[1, 6];
        public CellClass Cell26 => _model[2, 6];
        public CellClass Cell36 => _model[3, 6];
        public CellClass Cell46 => _model[4, 6];
        public CellClass Cell56 => _model[5, 6];
        public CellClass Cell66 => _model[6, 6];
        public CellClass Cell76 => _model[7, 6];
        public CellClass Cell86 => _model[8, 6];
        public CellClass Cell07 => _model[0, 7];
        public CellClass Cell17 => _model[1, 7];
        public CellClass Cell27 => _model[2, 7];
        public CellClass Cell37 => _model[3, 7];
        public CellClass Cell47 => _model[4, 7];
        public CellClass Cell57 => _model[5, 7];
        public CellClass Cell67 => _model[6, 7];
        public CellClass Cell77 => _model[7, 7];
        public CellClass Cell87 => _model[8, 7];
        public CellClass Cell08 => _model[0, 8];
        public CellClass Cell18 => _model[1, 8];
        public CellClass Cell28 => _model[2, 8];
        public CellClass Cell38 => _model[3, 8];
        public CellClass Cell48 => _model[4, 8];
        public CellClass Cell58 => _model[5, 8];
        public CellClass Cell68 => _model[6, 8];
        public CellClass Cell78 => _model[7, 8];
        public CellClass Cell88 => _model[8, 8];
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