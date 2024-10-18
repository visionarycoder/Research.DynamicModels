using Access.Denali.Contract.Models;

namespace Access.Denali.Contract;

public interface IDenaliAccess
{
    Task<ICollection<Bear>> GetBearsAsync(CancellationToken cancellationToken);
    Task<Bear?> GetBearByIdAsync(int id, CancellationToken cancellationToken);
    Task<Bear?> CreateBearAsync(Bear bear, CancellationToken cancellationToken);
    Task<Bear?> UpdateBearAsync(Bear bear, CancellationToken cancellationToken);
    Task<bool> DeleteBearAsync(int id, CancellationToken cancellationToken);

}