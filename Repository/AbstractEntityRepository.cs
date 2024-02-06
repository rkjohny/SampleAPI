using Microsoft.EntityFrameworkCore;


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
}
