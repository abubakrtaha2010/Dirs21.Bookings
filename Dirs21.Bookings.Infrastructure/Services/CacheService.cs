namespace Dirs21.Bookings.Infrastructure.Services;

/// <summary>
/// Service for caching data using an in-memory cache.
/// </summary>
/// <param name="cache">The memory cache instance.</param>
/// <param name="options">The cache settings options.</param>
public class CacheService(IMemoryCache cache, IOptions<CacheSettings> options) : ICacheService
{
    /// <summary>
    /// Retrieves a value from the cache.
    /// </summary>
    /// <typeparam name="T">The type of the value to retrieve.</typeparam>
    /// <param name="key">The key of the value to retrieve.</param>
    /// <returns>The value associated with the specified key, or null if the key does not exist.</returns>
    public T? GetValue<T>(string key) => cache.Get<T>(key);

    /// <summary>
    /// Stores a value in the cache.
    /// </summary>
    /// <typeparam name="T">The type of the value to store.</typeparam>
    /// <param name="key">The key of the value to store.</param>
    /// <param name="value">The value to store.</param>
    /// <returns>The value that was stored.</returns>
    public T SetValue<T>(string key, T value) => cache.Set(key, value, TimeSpan.FromHours(options.Value.ExpirationTimeInHours));
}
