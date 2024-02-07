using Microsoft.EntityFrameworkCore;
using SampleAPI.Models;
using SampleAPI.Types;


namespace SampleAPI.Repository;

public class PersonRepositoryPgSql(DbContextOptions<PersonRepositoryPgSql> options)
    : AbstractPersonRepository<PersonRepositoryPgSql>(options)
{
    public override async Task<Person> AddIfNotExistsAsync(Person person)
    {
        return await base.AddIfNotExistsAsync(person);
    }
}