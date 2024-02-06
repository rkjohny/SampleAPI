using SampleAPI.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace SampleAPI.Repository;

public class PersonRepositoryRedis(IConnectionMultiplexer redis)
{
    private static int _id = 0;
    private const string LogicalTable = "Person:";

    public IDatabase RedisDb { get; } = redis.GetDatabase();

    public Person? AddIfNotExists(Person person)
    {
        var personInDb = RedisDb.StringGet(LogicalTable + person.Email);

        if (!personInDb.IsNullOrEmpty)
        {
            return JsonSerializer.Deserialize<Person>(personInDb!);
        }
        
        person.Id = ++_id;
        person.CreatedAt = DateTime.Now;
        person.LastUpdatedAt = DateTime.Now;
        RedisDb.StringSet(LogicalTable + person.Email, JsonSerializer.Serialize(person));
        return person;
    }
}