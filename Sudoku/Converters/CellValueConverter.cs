using System;
using System.Globalization;
using System.Windows.Data;
using Sudoku.Base;
using Sudoku.Enums;

namespace Sudoku.Converters
{
    public class CellValueConverter : IValueConverter
    {
        // model to view
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is CellValue cellValue
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