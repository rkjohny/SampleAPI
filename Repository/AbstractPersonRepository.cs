using Microsoft.EntityFrameworkCore;
using SampleAPI.Models;

namespace SampleAPI.Repository;

public class AbstractPersonRepository<TC>(DbContextOptions<TC> options)
    : AbstractAuditableEntityRepository<TC, Person>(options) where TC : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Person>().ToTable("Person");
    }
 
    
    public async Task<Person> AddIfNotExistsAsync(Person person)
    {
        var personInDb = await Items.FirstOrDefaultAsync(p => p.Email == person.Email);

        if (personInDb != null) return personInDb;

        Items.Add(person);
        await SaveChangesAsync();
        return person;
    }

    public async Task<Person?> GetByEmail(string email) => await Items.FirstOrDefaultAsync(p => p.Email == email);

    public async Task<List<Person>> GetByLastName(string lastName) =>
        await Items.Where(p => p.LastName == lastName).ToListAsync();

    public async Task<List<Person>> GetByFirstName(string firstName) =>
        await Items.Where(p => p.FirstName == firstName).ToListAsync();

    public async Task<List<Person>> GetByFullName(string firstName, string lastName) =>
        await Items.Where(p => p.FirstName == firstName && p.LastName == lastName)
            .ToListAsync();
}