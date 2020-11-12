using Microsoft.Win32;

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
    }
}