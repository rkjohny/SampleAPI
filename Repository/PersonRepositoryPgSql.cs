using Microsoft.EntityFrameworkCore;
using SampleAPI.Models;
using SampleAPI.Types;
using DbType = SampleAPI.Types.DbType;


namespace SampleAPI.Repository;

public class PersonRepositoryPgSql(DbContextOptions<PersonRepositoryPgSql> options)
    : AbstractPersonRepository<PersonRepositoryPgSql>(options)
{
    public async Task<PersonDto> AddIfNotExistsAsync(Person person)
    {
        return await base.AddIfNotExistsAsync(DbType.PgSql, person);
    }
}