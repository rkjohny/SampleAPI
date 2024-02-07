using System.Runtime.Caching;

namespace SampleAPI.Core;

// TODO: Inject in Dependency as SingleTon 
public class PersonCacheService : ICacheService
{
    // TODO: for distributed system use a distributed caching (as second layer caching of EF) like Redis, MemCache, HazelCast etc.
    // MemoryCache is thread-safe, but doesn't prevent race condition for Set method
    // MemoryCache will be used as SingleTon
    private static readonly ObjectCache MemoryCache = System.Runtime.Caching.MemoryCache.Default;

    public T? GetData<T>(string key)
    {
        if (MemoryCache.Contains(key))
        {
            return (T)MemoryCache.Get(key);
        }
        return default;
    }

    public void SetData<T>(string key, T? value, DateTimeOffset expirationTime)
    {
        if (value != null)
        {
            MemoryCache.Set(key, value, expirationTime);
        }
    }
}