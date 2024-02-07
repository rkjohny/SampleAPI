using Microsoft.EntityFrameworkCore;
using SampleAPI.Models;
using SampleAPI.Types;


namespace SampleAPI.Repository;

public class PersonRepositoryInMemory(DbContextOptions<PersonRepositoryInMemory> options)
    : AbstractPersonRepository<PersonRepositoryInMemory>(options)
{
    public override async Task<Person> AddIfNotExistsAsync(Person person)
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