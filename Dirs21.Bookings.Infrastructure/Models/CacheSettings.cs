namespace Dirs21.Bookings.Infrastructure.Models;

public record CacheSettings
{
    public int ExpirationTimeInHours { get; set; }
}