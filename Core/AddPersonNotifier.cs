using SampleAPI.Types;
using StackExchange.Redis;

namespace SampleAPI.Core;

public class AddPersonNotifier(IConnectionMultiplexer redis)
{
    private const string AddPersonChannelSuccess = "add_person_success";

    private IDatabase RedisDb { get; } = redis.GetDatabase();

    public async Task NotifyOnSuccessAsync(AddPersonOutputV2 output)
    {
        var outputJson = await Utils.Serialize(output);
        await RedisDb.PublishAsync(AddPersonChannelSuccess, outputJson);
    }
}