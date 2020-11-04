namespace Sudoku.Interfaces
{
    public interface IViewModel
    {
        string Name => this.GetType().Name;
    }
}