using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.OpenApi.Extensions;
using SampleAPI.Types;

namespace SampleAPI.Core;

public static class Utils
{
    // TODO: set expiration to a valid value. (for now consider 10 hours as infinity)
    private const int ExpirationTimeOfCachedItemInSec = 10 * 60 * 60; // 10 hour

    private const string LogicalCachedDd = "SampleAPI_Cache";

    private const string LogicalDd = "SampleAPI_DB";

    private const string LogicalTable = "Person";

    public static string GetRedisKey(string email)
    {
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(email));
        return LogicalDd + ":" + LogicalTable + ":" + Convert.ToBase64String(hash);
    }

    public static string GetCachedKey(DbType dbType, string email)
    {
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(email));
        return LogicalCachedDd + ":" + dbType.GetDisplayName() + ":" + LogicalTable + ":" + Convert.ToBase64String(hash);
    }

    public static TimeSpan ExpirationTimeSpan()
    {
        return TimeSpan.FromSeconds(ExpirationTimeOfCachedItemInSec);
    }

    public static DateTimeOffset ExpirationDateTimeOffset()
    {
        return DateTimeOffset.Now.AddSeconds(ExpirationTimeOfCachedItemInSec);
    }


    public static async Task<T?> Deserialize<T>(string json)
    {
        if (string.IsNullOrEmpty(json))
            return default;
        return await Task.FromResult(JsonSerializer.Deserialize<T>(json));
    }

    public static async Task<string> Serialize<T>(T obj)
    {
        return await Task.FromResult(JsonSerializer.Serialize(obj));
    }
}