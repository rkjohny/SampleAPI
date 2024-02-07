using SampleAPI.Models;
using StackExchange.Redis;
using System.Text.Json;
using SampleAPI.Types;

namespace SampleAPI.Repository;

public class PersonRepositoryRedis(IConnectionMultiplexer redis)
{
    private static int _id = 0;
    private const string LogicalTable = "Person";

    public IDatabase RedisDb { get; } = redis.GetDatabase();

    public async Task<PersonDto> AddIfNotExistsAsync(Person person)
    {
        var key = LogicalTable + ":" + person.Email;
        var personInDb = RedisDb.StringGet(key);

        if (!personInDb.IsNullOrEmpty)
        {
            return new PersonDto(await Deserialize(personInDb!));
        }
        
        person.Id = ++_id;
        person.CreatedAt = DateTime.Now;
        person.LastUpdatedAt = DateTime.Now;
        person.SyncVersion = DateTime.UtcNow.ToFileTime();
        person.RowVersion = Guid.NewGuid().ToByteArray();
        
        var personJson = await Serialize(person);
        RedisDb.StringSet(key, personJson);
        return new PersonDto(person);
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