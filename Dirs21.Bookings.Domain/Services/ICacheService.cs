namespace Dirs21.Bookings.Domain.Services;

/// <summary>
/// Interface for a cache service that allows storing and retrieving values.
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Retrieves a value from the cache.
    /// </summary>
    /// <typeparam name="T">The type of the value to retrieve.</typeparam>
    /// <param name="key">The key of the value to retrieve.</param>
    /// <returns>The value associated with the specified key, or null if the key does not exist.</returns>
    T? GetValue<T>(string key);

    /// <summary>
    /// Stores a value in the cache.
    /// </summary>
    /// <typeparam name="T">The type of the value to store.</typeparam>
    /// <param name="key">The key of the value to store.</param>
    /// <param name="value">The value to store.</param>
    /// <returns>The value that was stored.</returns>
    T SetValue<T>(string key, T value);
}
