using Microsoft.EntityFrameworkCore;
using SchedulingBusinessSoftware.Models;

namespace SchedulingBusinessSoftware.Interfaces
{
    public interface IAppDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);


    }
}
