using SampleAPI.Models;
using StackExchange.Redis;
using SampleAPI.Types;
using SampleAPI.Core;


namespace SampleAPI.Repository;

public class PersonRepositoryRedis(IConnectionMultiplexer redis, ICacheService cacheService)
{
    private static int _id = 0;

    public IDatabase RedisDb { get; } = redis.GetDatabase();

    public async Task<PersonDto> AddIfNotExistsAsync(Person person)
    {
        var cacheKey = Utils.GetCachedKey(DbType.Redis, person.Email);

        var redisKey = Utils.GetRedisKey(person.Email);

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
        var personJson = await Utils.Serialize(person);
        RedisDb.StringSet(redisKey, personJson);
        
        // Finally add into cache
        var newPersonInCache = new PersonDto(person);
        cacheService.SetData(cacheKey, newPersonInCache, Utils.ExpirationDateTimeOffset());
        return newPersonInCache;
    }
}