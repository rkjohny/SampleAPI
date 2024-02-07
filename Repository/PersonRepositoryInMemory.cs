using Microsoft.EntityFrameworkCore;
using SampleAPI.Models;
using SampleAPI.Types;


namespace SampleAPI.Repository;

public class PersonRepositoryInMemory(DbContextOptions<PersonRepositoryInMemory> options)
    : AbstractPersonRepository<PersonRepositoryInMemory>(options)
{
    public async Task<PersonDto> AddIfNotExistsAsync(Person person)
    {
        return await base.AddIfNotExistsAsync(DbType.InMemory, person);
    }
}