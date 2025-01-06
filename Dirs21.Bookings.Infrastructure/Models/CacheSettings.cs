namespace Dirs21.Bookings.Infrastructure.Models;

/// <summary>
/// Represents the settings for cache configuration.
/// </summary>
public record CacheSettings
{
    /// <summary>
    /// Gets or sets the expiration time of the cache in hours.
    /// </summary>
    public int ExpirationTimeInHours { get; set; }
}
