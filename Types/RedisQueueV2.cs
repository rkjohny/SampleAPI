using StackExchange.Redis;

namespace SampleAPI.Types;

public class RedisQueueV2(IConnectionMultiplexer redis, ILogger<RedisQueueV2> logger)
{
    private IDatabase RedisDb { get; } = redis.GetDatabase();

    public async Task PushAsync(RedisKey queueName, RedisValue value)
    {
        await RedisDb.ListLeftPushAsync(queueName, value);
    }

    public async Task<RedisValue> PopAsync(RedisKey queueName)
    {
        try
        {
            return await RedisDb.ListRightPopAsync(queueName);
        }
        catch (RedisTimeoutException ex)
        {
            logger.LogError(ex,"Redis Timed out error occured: {StackTrace}", ex.StackTrace);
            return RedisValue.Null;
        }
    }
}
