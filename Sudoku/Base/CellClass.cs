using Sudoku.Enums;

namespace Sudoku.Base
{
    public class CellClass : ObservableObject
    {
        private CellValue _value;

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

        public CellClass(CellValue value = CellValue.None)
        {
            this._value = value;
        }

        public CellClass Clone()
        {
            return new CellClass(_value);
        }
    }
}