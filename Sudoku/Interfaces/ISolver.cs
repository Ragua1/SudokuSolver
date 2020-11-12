using System.Threading.Tasks;

namespace Sudoku.Interfaces
{
    public interface ISolver
    {
        IArena Solution { get; set; }

        Task<bool> ResolveAsync(IArena arena);
    }
}