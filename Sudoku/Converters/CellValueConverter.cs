using System;
using System.Globalization;
using System.Windows.Data;
using Sudoku.Enums;

namespace Sudoku.Converters
{
    public class CellValueConverter : IValueConverter
    {
        // model to view
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CellValue cellValue)
            {
                switch (cellValue)
                {
                    case CellValue.None:
                        break;
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
                }
            }

            return "";
        }

        // view to model
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Enum.TryParse(typeof(CellValue), value?.ToString(), out var cellValue) 
                ? cellValue 
                : CellValue.None;
        }
    }
}