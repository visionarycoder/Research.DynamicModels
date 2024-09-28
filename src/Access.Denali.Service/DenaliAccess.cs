using Access.Denali.Contract;
using Data.Alaska;
using Microsoft.Extensions.Logging;

namespace Access.Denali.Service
{
    
    public class DenaliAccess(ILogger<DenaliAccess> logger, AlaskaContext ctx) : IDenaliAccess
    {

    }

}
