namespace Dirs21.Bookings.Domain.Models;

/// <summary>
/// Represents a reservation with details such as ID, status, name, start time, and end time.
/// </summary>
public record Dirs21Reservation
{
    /// <summary>
    /// Gets or sets the unique identifier for the reservation.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the status of the reservation.
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name associated with the reservation.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the start time of the reservation.
    /// </summary>
    public string StartTime { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the end time of the reservation.
    /// </summary>
    public string EndTime { get; set; } = string.Empty;
}
