using Sudoku.Enums;

namespace Sudoku.Base
{
    public class CellClass : ObservableObject
    {
        private CellValue _value = CellValue.None;

        public CellValue Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}