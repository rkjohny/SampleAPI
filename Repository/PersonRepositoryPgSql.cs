using Microsoft.EntityFrameworkCore;
using SampleAPI.Core;
using SampleAPI.Models;
using SampleAPI.Types;
using DbType = SampleAPI.Types.DbType;


namespace SampleAPI.Repository;

public class PersonRepositoryPgSql(DbContextOptions<PersonRepositoryPgSql> options, ICacheService cacheService)
    : AbstractPersonRepository<PersonRepositoryPgSql>(options, cacheService)
{
    public async Task<PersonDto> AddIfNotExistsAsync(Person person)
    {
        return await base.AddIfNotExistsAsync(DbType.PgSql, person);
    }
}