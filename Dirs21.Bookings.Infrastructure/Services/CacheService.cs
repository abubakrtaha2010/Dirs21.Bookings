namespace Dirs21.Bookings.Infrastructure.Services;

public class CacheService(IMemoryCache cache, IOptions<CacheSettings> options) : ICacheService
{
    public T? GetValue<T>(string key) => cache.Get<T>(key);

    public T SetValue<T>(string key, T value) => cache.Set(key, value, TimeSpan.FromHours(options.Value.ExpirationTimeInHours));
}
