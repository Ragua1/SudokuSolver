using System.Threading;
using System.Threading.Tasks;

namespace SudokuSimply.Interfaces
{
    public interface ISolver
    {
        Task<bool> ResolveAsync(IArena arena, CancellationToken token);
    }
}