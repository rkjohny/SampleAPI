using Microsoft.EntityFrameworkCore;
using SampleAPI.Models;

namespace SampleAPI.Repository;

public class AbstractSyncableEntiyRepository<TC, TM>(DbContextOptions<TC> options) : AbstractEntityRepository<TC, TM>(options) where TM : class where TC : DbContext
{
    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    ((AbstractSyncableEntity)entry.Entity).SyncVersion = DateTime.UtcNow.ToFileTime();
                    break;
                case EntityState.Modified:
                    ((AbstractSyncableEntity)entry.Entity).SyncVersion = DateTime.UtcNow.ToFileTime();
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
                    ((AbstractSyncableEntity)entry.Entity).SyncVersion = DateTime.UtcNow.ToFileTime();
                    break;
                case EntityState.Modified:
                    ((AbstractSyncableEntity)entry.Entity).SyncVersion = DateTime.UtcNow.ToFileTime();
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }

}