using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using SudokuSimply.Base;
using SudokuSimply.Converters;
using SudokuSimply.ViewModels;

namespace SudokuSimply
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _viewModel = MainViewModel.Instance;

            InitGameGrid();
        }

        private void InitGameGrid()
        {
            for (var i = 0; i < Constants.ORIG_SUDOKU_GRID_SIZE; i++)
            {
                GameGrid.RowDefinitions.Add(new RowDefinition());
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            var whiteStyle = (Style) FindResource("CellStyle");
            var blueStyle = (Style) FindResource("CellBlueStyle");

            var converter = new CellValueConverter();

            var lastStyle = blueStyle;

            for (var i = 0; i < Constants.ORIG_SUDOKU_GRID_SIZE; i++)
            {
                var currentStyle = lastStyle;
                if (i % Constants.ORIG_SUDOKU_REGION_SIZE == 0)
                {
                    lastStyle = currentStyle = currentStyle == whiteStyle
                        ? blueStyle
                        : whiteStyle;
                }

                for (var j = 0; j < Constants.ORIG_SUDOKU_GRID_SIZE; j++)
                {
                    if (j % Constants.ORIG_SUDOKU_REGION_SIZE == 0)
                    {
                        currentStyle = currentStyle == whiteStyle
                            ? blueStyle
                            : whiteStyle;
                    }

                    var textBox = new TextBox
                    {
                        Style = currentStyle
                    };
                    
                    textBox.SetBinding(TextBox.TextProperty, new Binding
                    {
                        Path = new PropertyPath($"{MainViewModel.CellsPropertyName}[{i * Constants.ORIG_SUDOKU_GRID_SIZE + j}].{CellClass.ValuePropertyName}"),
                        Converter = converter,
                        Mode = BindingMode.TwoWay,
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    });

                    textBox.SetValue(Grid.RowProperty, i);
                    textBox.SetValue(Grid.ColumnProperty, j);

                    GameGrid.Children.Add(textBox);
                }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            _viewModel?.NewGame();
        }

        private async void Solve_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                await _viewModel.SolveGameAsync().ConfigureAwait(true);
            }
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                await _viewModel.SaveGameAsync().ConfigureAwait(true);
            }
        }

        private async void Load_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                await _viewModel.LoadGameAsync().ConfigureAwait(true);
            }
        }
    }
}
