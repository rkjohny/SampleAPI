using System.ComponentModel;
using SampleAPI.Models;
using SampleAPI.Services;
using SampleAPI.Types;

namespace SampleAPI.Core;

public class AddPersonHelper(PersonService personService, ILogger<AddPersonHelper> logger) : AbstractHelper(logger)
{
    protected override void ValidateInput(AbstractInput input)
    {
    }

    protected override void CheckPermission(AbstractInput input)
    {
    }

    protected override async Task<AbstractOutput> ExecuteAsync(AbstractInput input, object? args)
    {
        if (input is not AddPersonInput addPersonInput)
        {
            throw new InvalidEnumArgumentException("Invalid argument in AddPersonHelper, expected AddPersonInput");
        }

        if (args is not DbType dbType)
        {
            throw new InvalidEnumArgumentException("Invalid argument in AddPersonHelper, expected DbType");
        }
            
        var person = new Person
        {
            FirstName = addPersonInput.FirstName,
            LastName = addPersonInput.LastName,
            Email = addPersonInput.Email,
        };

        var addedPerson = dbType switch
        {
            DbType.InMemory => await personService.AddPersonInMemoryAsync(person),
            DbType.PgSql => await personService.AddPersonPgSqlAsync(person),
            DbType.MySql => await personService.AddPersonMySqlAsync(person),
            DbType.Redis => await personService.AddPersonRedisAsync(person),
            _ => throw new InvalidEnumArgumentException("Invalid database type.")
        };
        return new AddPersonOutput(addedPerson);
    }
}