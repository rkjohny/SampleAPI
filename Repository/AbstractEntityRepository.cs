using Microsoft.EntityFrameworkCore;
using SampleAPI.Models;


namespace SampleAPI.Repository;

public abstract class AbstractEntityRepository<TC, TM>(DbContextOptions<TC> options) : DbContext(options) where TM : class where TC : DbContext
{
    public required DbSet<TM> Items { get; set; }
    
    public async Task<TM?> GetById(long id) => await Items.FindAsync(id);

    public async Task<List<TM>> GetAll() => await Items.ToListAsync();

    public async Task<TM> Create(TM entity)
    {
        Items.Add(entity);
        await SaveChangesAsync();
        return entity;
    }

    public async Task<TM> Update(TM entity)
    {
        Items.Update(entity);
        await SaveChangesAsync();
        return entity;
    }

    public async Task Delete(TM entity)
    {
        Items.Remove(entity);
        await SaveChangesAsync();
    }

    
    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    ((AbstractEntity)entry.Entity).RowVersion = Guid.NewGuid().ToByteArray();
                    break;
                case EntityState.Modified:
                    ((AbstractEntity)entry.Entity).RowVersion = Guid.NewGuid().ToByteArray();
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
                    ((AbstractEntity)entry.Entity).RowVersion = Guid.NewGuid().ToByteArray();
                    break;
                case EntityState.Modified:
                    ((AbstractEntity)entry.Entity).RowVersion = Guid.NewGuid().ToByteArray();
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
    
}
