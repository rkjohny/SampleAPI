using Microsoft.EntityFrameworkCore;
using SampleAPI.Core;
using SampleAPI.Models;
using SampleAPI.Types;


namespace SampleAPI.Repository;

public class PersonRepositoryMySql(DbContextOptions<PersonRepositoryMySql> options, ICacheService cacheService)
    : AbstractPersonRepository<PersonRepositoryMySql>(options, cacheService)
{
    public async Task<PersonDto> AddIfNotExistsAsync(Person person)
    {
        return await base.AddIfNotExistsAsync(DbType.MySql, person);
    }
}