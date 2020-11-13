using System.Threading.Tasks;

namespace Sudoku.Interfaces
{
    public interface ISolver
    {
        Task<bool> ResolveAsync(IArena arena);
    }
}