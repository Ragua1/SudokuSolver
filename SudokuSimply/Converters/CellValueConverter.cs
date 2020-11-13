using System;
using System.Globalization;
using System.Windows.Data;
using SudokuSimply.Base;

namespace SudokuSimply.Converters
{
    public class CellValueConverter : IValueConverter
    {
        // model to view
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is int cellValue
                ? Common.GetTextValue(cellValue)
                : "";
        }

        // view to model
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Common.GetCellValue(value?.ToString());
        }
    }
}