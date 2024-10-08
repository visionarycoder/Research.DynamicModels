using Access.Denali.Contract.Models;

namespace Access.Denali.Contract
{
    public interface IDenaliAccess
    {
        Task<ICollection<Widget>> GetWidgetsAsync();
    }
}
