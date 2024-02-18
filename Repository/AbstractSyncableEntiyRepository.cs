using Microsoft.EntityFrameworkCore;
using SampleAPI.Models;

namespace SampleAPI.Repository;

public class AbstractSyncableEntiyRepository<TC, TM>(DbContextOptions<TC> options) : AbstractEntityRepository<TC, TM>(options) where TM : class where TC : DbContext
{
    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            ((AbstractSyncableEntity)entry.Entity).SyncVersion = entry.State switch
            {
                EntityState.Added => DateTime.UtcNow.ToFileTime(),
                EntityState.Modified => DateTime.UtcNow.ToFileTime(),
                _ => ((AbstractSyncableEntity)entry.Entity).SyncVersion
            };
        }
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            ((AbstractSyncableEntity)entry.Entity).SyncVersion = entry.State switch
            {
                EntityState.Added => DateTime.UtcNow.ToFileTime(),
                EntityState.Modified => DateTime.UtcNow.ToFileTime(),
                _ => ((AbstractSyncableEntity)entry.Entity).SyncVersion
            };
        }
        return base.SaveChangesAsync(cancellationToken);
    }

}