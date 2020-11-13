using System;
using Microsoft.Win32;
using Sudoku.Enums;

namespace Sudoku.Base
{
    public static class Common
    {
        public static string SelectFile(bool saveFile = false)
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

        public static CellValue GetCellValue(string text)
        {
            return Enum.TryParse(text, out CellValue cellValue)
                ? cellValue
                : CellValue.None;
        }

        public static string GetTextValue(CellValue cellValue)
        {
            switch (cellValue)
            {
                case CellValue.V1:
                case CellValue.V2:
                case CellValue.V3:
                case CellValue.V4:
                case CellValue.V5:
                case CellValue.V6:
                case CellValue.V7:
                case CellValue.V8:
                case CellValue.V9:
                    return $"{(int)cellValue}";

                case CellValue.None:
                default:
                    return "";
            }
        }
    }
}