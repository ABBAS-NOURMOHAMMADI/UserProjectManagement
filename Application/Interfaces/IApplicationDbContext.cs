using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Domain.Interfaces
{
    public interface IApplicationDbContext : IDisposable
    {
        DbSet<Project> Project { get; set; }
        DbSet<Entities.Task> Tasks { get; set; }

        ChangeTracker GetChangeTracker();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
