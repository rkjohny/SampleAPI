using SampleAPI.Models;
using StackExchange.Redis;
using System.Text.Json;
using SampleAPI.Types;
using SampleAPI.Core;


namespace SampleAPI.Repository;

public class PersonRepositoryRedis(IConnectionMultiplexer redis, ICacheService cacheService)
{
    private static int _id = 0;
    private const string LogicalTable = "Person";

    public IDatabase RedisDb { get; } = redis.GetDatabase();

    public async Task<PersonDto> AddIfNotExistsAsync(Person person)
    {
        string cacheKey = "Redis" + ":" + LogicalTable + ":" + person.Email;

        var redisKey = LogicalTable + ":" + person.Email;

        // First look into cache if found return
        var personInCache = cacheService.GetData<PersonDto>(cacheKey);
        if (personInCache != null)
        {
            return personInCache;
        }

        person.Id = ++_id;
        person.CreatedAt = DateTime.Now;
        person.LastUpdatedAt = DateTime.Now;
        person.SyncVersion = DateTime.UtcNow.ToFileTime();
        person.RowVersion = DateTime.UtcNow.ToFileTime();
        
        // Then look into redis database, if not found, add into database
        var personInDb = RedisDb.StringGet(redisKey);
        if (!personInDb.IsNullOrEmpty)
        {
            var personJson = await Serialize(person);
            RedisDb.StringSet(redisKey, personJson);
            //return new PersonDto(await Deserialize(personInDb!));
        }

        PersonDto newPersonInCache = new PersonDto(person);
        // TODO: set expiration to a valid value. (for now consider 10 hours as infinity)
        var expirationTime = DateTimeOffset.Now.AddMinutes(600);
        cacheService.SetData(cacheKey, newPersonInCache, expirationTime);
        return newPersonInCache;
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