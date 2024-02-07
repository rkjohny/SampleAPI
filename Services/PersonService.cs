using SampleAPI.Models;
using SampleAPI.Repository;
using SampleAPI.Types;

namespace SampleAPI.Services;

public class PersonService(PersonRepositoryInMemory repositoryInMemory, PersonRepositoryPgSql repositoryPgSql, PersonRepositoryMySql repositoryMySql, PersonRepositoryRedis repositoryRedis)
{
    public async Task<AddPersonOutput> AddPersonInMemoryAsync(AddPersonInput input)
    {
        var person = new Person
        {
            FirstName = input.FirstName,
            LastName = input.LastName,
            Email = input.Email,
        };
        var addedPerson = await repositoryInMemory.AddIfNotExistsAsync(person);
        return new AddPersonOutput(addedPerson);
    }

    public async Task<AddPersonOutput> AddPersonPgSqlAsync(AddPersonInput input)
    {
        var person = new Person
        {
            FirstName = input.FirstName,
            LastName = input.LastName,
            Email = input.Email
        };
        
        var addedPerson = await repositoryPgSql.AddIfNotExistsAsync(person);
        return new AddPersonOutput(addedPerson);
    }
    
    public async Task<AddPersonOutput> AddPersonMySqlAsync(AddPersonInput input)
    {
        var person = new Person
        {
            FirstName = input.FirstName,
            LastName = input.LastName,
            Email = input.Email,
        };
        var addedPerson = await repositoryMySql.AddIfNotExistsAsync(person);
        return new AddPersonOutput(addedPerson);
    }


    public async Task<AddPersonOutput> AddPersonRedisAsync(AddPersonInput input)
    {
        var person = new Person
        {
            FirstName = input.FirstName,
            LastName = input.LastName,
            Email = input.Email
        };
        var addedPerson = await repositoryRedis.AddIfNotExistsAsync(person);
        return new AddPersonOutput(addedPerson);
    }
}