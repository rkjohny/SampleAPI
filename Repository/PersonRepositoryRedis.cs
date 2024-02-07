using SampleAPI.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace SampleAPI.Repository;

public class PersonRepositoryRedis(IConnectionMultiplexer redis)
{
    private static int _id = 0;
    private const string LogicalTable = "Person:";

    public IDatabase RedisDb { get; } = redis.GetDatabase();

    public async Task<Person> AddIfNotExistsAsync(Person person)
    {
        var personInDb = RedisDb.StringGet(LogicalTable + person.Email);

        if (!personInDb.IsNullOrEmpty)
        {
            return await Deserialize(personInDb!);
        }
        
        person.Id = ++_id;
        person.CreatedAt = DateTime.Now;
        person.LastUpdatedAt = DateTime.Now;
        person.SyncVersion = DateTime.UtcNow.ToFileTime();
        person.RowVersion = Guid.NewGuid().ToByteArray();
        
        var personJson = await Serialize(person);
        RedisDb.StringSet(LogicalTable + person.Email, personJson);
        return person;
    }

    private static async Task<Person> Deserialize(string person)
    {
        return  await Task.FromResult(JsonSerializer.Deserialize<Person>(person)!);
    }
    
    private static async Task<string> Serialize(Person person)
    {
        return await Task.FromResult(JsonSerializer.Serialize(person));
    }
}