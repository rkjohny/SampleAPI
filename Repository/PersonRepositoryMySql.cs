using Microsoft.EntityFrameworkCore;
using SampleAPI.Models;
using SampleAPI.Types;


namespace SampleAPI.Repository;

public class PersonRepositoryMySql(DbContextOptions<PersonRepositoryMySql> options)
    : AbstractPersonRepository<PersonRepositoryMySql>(options)
{
    public async Task<PersonDto> AddIfNotExistsAsync(Person person)
    {
        return await base.AddIfNotExistsAsync(DbType.MySql, person);
    }
}