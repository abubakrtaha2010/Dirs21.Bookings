namespace Dirs21.Bookings.Infrastructure.Models;

public record DatabaseSettings
{
    public string ConnectionString { get; set; } = string.Empty;
}
