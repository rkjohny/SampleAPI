using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

using SampleAPI.Models;
using SampleAPI.Types;

namespace SampleAPI.Repository;

public class AbstractPersonRepository<TC>(DbContextOptions<TC> options)
    : AbstractAuditableEntityRepository<TC, Person>(options) where TC : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Person>().ToTable("Person");
    }
    
    public virtual async Task<Person> AddIfNotExistsAsync(Person person)
    {
        var personInDb = await Items.FirstOrDefaultAsync(p => p.Email == person.Email);

        if (personInDb != null)
        {
            return personInDb;
        }

        Items.Add(person);
        await SaveChangesAsync();
        return person;
    }
}