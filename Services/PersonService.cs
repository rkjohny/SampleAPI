using SampleAPI.Models;
using SampleAPI.Repository;
using SampleAPI.Types;

namespace SampleAPI.Services;

public class PersonService(PersonRepositoryInMemory repositoryInMemory, PersonRepositoryPgSql repositoryPgSql, PersonRepositoryMySql repositoryMySql, PersonRepositoryRedis repositoryRedis)
{
    public async Task<PersonDto> AddPersonInMemoryAsync(Person person)
    {
        var addedPerson = await repositoryInMemory.AddIfNotExistsAsync(person);
        return addedPerson;
    }

    public async Task<PersonDto> AddPersonPgSqlAsync(Person person)
    {
        var addedPerson = await repositoryPgSql.AddIfNotExistsAsync(person);
        return addedPerson;
    }
    
    public async Task<PersonDto> AddPersonMySqlAsync(Person person)
    {
        var addedPerson = await repositoryMySql.AddIfNotExistsAsync(person);
        return addedPerson;
    }


    public async Task<PersonDto> AddPersonRedisAsync(Person person)
    {
        var addedPerson = await repositoryRedis.AddIfNotExistsAsync(person);
        return addedPerson;
    }
}