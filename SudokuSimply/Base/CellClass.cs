namespace SudokuSimply.Base
{
    public class CellClass : ObservableObject
    {
        private int _value;

        public static string ValuePropertyName => nameof(Value);

        public int Value
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

        public CellClass(int value = 0)
        {
            this._value = value;
        }

        public CellClass Clone()
        {
            return new CellClass(_value);
        }
    }
}