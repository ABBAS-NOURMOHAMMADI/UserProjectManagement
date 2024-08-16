using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        protected ICurrentUserService currentUserService;

        public DbSet<Project> Project { get; set; }
        public DbSet<Domain.Entities.Task> Tasks { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService = null) : base(options)
        {
            this.currentUserService = currentUserService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new TaskConfiguration());
        }

        public ChangeTracker GetChangeTracker() => this.ChangeTracker;

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var utcNow = DateTime.UtcNow;

            string userid = null;
            if (currentUserService != null)
            {
                userid = currentUserService.UserId;
                if (userid == null)
                    userid = ApplicationUser.SystemUser.Id;
            }

            var models = ChangeTracker.Entries<ICreateEntity>();
            foreach (var entity in models)
            {
                if (entity.State == EntityState.Added)
                {
                    if (entity.Entity.CreatedAt == DateTime.MinValue)
                        entity.Entity.CreatedAt = utcNow;

                    entity.Entity.CreatedBy = userid;
                }
            }

            var updateableModels = ChangeTracker.Entries<IModifiedEntity>();
            foreach (var entity in updateableModels)
            {
                if (entity.State == EntityState.Modified)
                {
                    entity.Entity.ModifiedAt = utcNow;
                    entity.Entity.ModifiedBy = userid;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
