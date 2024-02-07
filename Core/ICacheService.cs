namespace SampleAPI.Core;

public interface ICacheService
{
    T? GetData<T>(string key);

    void SetData<T>(string key, T? value, DateTimeOffset expirationTime);
}