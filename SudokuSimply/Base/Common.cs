using Microsoft.Win32;

namespace SudokuSimply.Base
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

        public static int GetCellValue(string text)
        {
            return int.TryParse(text, out var cellValue)
                   && cellValue >= Constants.ORIG_SUDOKU_MIN_VALUE 
                   && cellValue <= Constants.ORIG_SUDOKU_MAX_VALUE
                ? cellValue
                : Constants.ORIG_SUDOKU_EMPTY_VALUE;
        }

        public static string GetTextValue(int cellValue)
        {
            return cellValue >= Constants.ORIG_SUDOKU_MIN_VALUE && cellValue <= Constants.ORIG_SUDOKU_MAX_VALUE
                ? $"{cellValue}"
                : "";
        }
    }
}