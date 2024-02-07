using Microsoft.EntityFrameworkCore;
using SampleAPI.Models;
using SampleAPI.Types;


namespace SampleAPI.Repository;

public class PersonRepositoryMySql(DbContextOptions<PersonRepositoryMySql> options)
    : AbstractPersonRepository<PersonRepositoryMySql>(options)
{
    public override async Task<Person> AddIfNotExistsAsync(Person person)
    {
        return await base.AddIfNotExistsAsync(person);
    }
}