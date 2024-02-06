using Microsoft.EntityFrameworkCore;
using SampleAPI.Models;

namespace SampleAPI.Repository;


public abstract class AbstractAuditableEntityRepository<TC, TM>(DbContextOptions<TC> options) : AbstractEntityRepository<TC, TM>(options) where TM : class where TC : DbContext
{
    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    ((AbstractAuditableEntity)entry.Entity).CreatedAt = DateTime.UtcNow;
                    ((AbstractAuditableEntity)entry.Entity).LastUpdatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    ((AbstractAuditableEntity)entry.Entity).LastUpdatedAt = DateTime.UtcNow;
                    break;
            }
        }
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    ((AbstractAuditableEntity)entry.Entity).CreatedAt = DateTime.UtcNow;
                    ((AbstractAuditableEntity)entry.Entity).LastUpdatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    ((AbstractAuditableEntity)entry.Entity).LastUpdatedAt = DateTime.UtcNow;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
