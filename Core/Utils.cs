using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace SampleAPI.Core;

public static class Utils
{
    // TODO: set expiration to a valid value. (for now consider 10 hours as infinity)
    private const int ExpirationTimeOfCachedItemInSec = 10 * 60 * 60; // 10 hour
    
    public static string GetCachedKey(string key)
    {
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(key));
        return Convert.ToBase64String(hash);
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