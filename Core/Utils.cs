using System.Security.Cryptography;
using System.Text;

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
}