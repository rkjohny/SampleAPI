using SampleAPI.Models;
using SampleAPI.Repository;
using SampleAPI.Types;

namespace SampleAPI.Services;

public class PersonService(PersonRepositoryInMemory repositoryInMemory, PersonRepositoryPgSql repositoryPgSql, PersonRepositoryMySql repositoryMySql, PersonRepositoryRedis repositoryRedis)
{
    public async Task<PersonDto> AddPersonInMemoryAsync(AddPersonInput input)
    {
        var person = new Person
        {
            FirstName = input.FirstName,
            LastName = input.LastName,
            Email = input.Email,
        };
        var addedPerson = await repositoryInMemory.AddIfNotExistsAsync(person);
        return addedPerson;
    }

    public async Task<PersonDto> AddPersonPgSqlAsync(AddPersonInput input)
    {
        var person = new Person
        {
            FirstName = input.FirstName,
            LastName = input.LastName,
            Email = input.Email
        };
        
        var addedPerson = await repositoryPgSql.AddIfNotExistsAsync(person);
        return addedPerson;
    }
    
    public async Task<PersonDto> AddPersonMySqlAsync(AddPersonInput input)
    {
        var person = new Person
        {
            FirstName = input.FirstName,
            LastName = input.LastName,
            Email = input.Email,
        };
        var addedPerson = await repositoryMySql.AddIfNotExistsAsync(person);
        return addedPerson;
    }


    public async Task<PersonDto> AddPersonRedisAsync(AddPersonInput input)
    {
        var person = new Person
        {
            FirstName = input.FirstName,
            LastName = input.LastName,
            Email = input.Email
        };
        var addedPerson = await repositoryRedis.AddIfNotExistsAsync(person);
        return addedPerson;
    }
}