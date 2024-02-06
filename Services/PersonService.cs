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
        var addedPerson = await repositoryInMemory.AddIfNotExists(person);
        return new AddPersonOutput(addedPerson);
    }

    public async Task<AddPersonOutput> AddPersonPgSqlAsync(AddPersonInput input)
    {
        var person = new Person
        {
            FirstName = input.FirstName,
            LastName = input.LastName,
            Email = input.Email,
        };
        var addedPerson = await repositoryPgSql.AddIfNotExists(person);
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
        var addedPerson = await repositoryMySql.AddIfNotExists(person);
        return new AddPersonOutput(addedPerson);
    }


    public Task<AddPersonOutput> AddPersonRedis(AddPersonInput input)
    {
        var person = new Person
        {
            FirstName = input.FirstName,
            LastName = input.LastName,
            Email = input.Email,
        };
        var addedPerson = repositoryRedis.AddIfNotExists(person);
        return Task.FromResult(new AddPersonOutput(addedPerson!));
    }
}