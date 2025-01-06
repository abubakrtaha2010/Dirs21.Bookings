namespace Dirs21.Bookings.Infrastructure.Models;

/// <summary>
/// Represents the settings required to connect to the database.
/// </summary>
public record DatabaseSettings
{
    /// <summary>
    /// Gets or sets the connection string used to connect to the database.
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;
}
