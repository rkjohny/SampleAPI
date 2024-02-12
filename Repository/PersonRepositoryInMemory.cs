using Microsoft.EntityFrameworkCore;
using SampleAPI.Core;
using SampleAPI.Models;
using SampleAPI.Types;


namespace SampleAPI.Repository;

public class PersonRepositoryInMemory(DbContextOptions<PersonRepositoryInMemory> options, ICacheService cacheService)
    : AbstractPersonRepository<PersonRepositoryInMemory>(options, cacheService)
{
    public async Task<PersonDto> AddIfNotExistsAsync(Person person)
    {
        return await base.AddIfNotExistsAsync(DbType.InMemory, person);
    }
}