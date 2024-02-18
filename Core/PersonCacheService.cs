using Microsoft.Extensions.Caching.Memory;


namespace SampleAPI.Core;

// Using an InMemoryCaching over the Entity Framework 2nd Layer Cache, for not to iterate the whole entity list while adding an entity
public class PersonCacheService(IMemoryCache memoryCache) : ICacheService
{
    public T? GetData<T>(string key)
    {
        return memoryCache.TryGetValue(key, out T? value) ? value : default;
    }

    public void SetData<T>(string key, T? value, DateTimeOffset expirationTime)
    {
        if (value != null)
        {
            memoryCache.Set(key, value, expirationTime);
        }
    }
}