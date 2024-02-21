using SampleAPI.Models;
using StackExchange.Redis;
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
        var cacheKey = Utils.GetCachedKey("Redis" + ":" + LogicalTable + ":" + person.Email);

        var redisKey = Utils.GetCachedKey(LogicalTable + ":" + person.Email);

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
            var personJson = await Utils.Serialize(person);
            RedisDb.StringSet(redisKey, personJson);
        }

        var newPersonInCache = new PersonDto(person);
        cacheService.SetData(cacheKey, newPersonInCache, Utils.ExpirationDateTimeOffset());
        return newPersonInCache;
    }
}