namespace Dirs21.Bookings.Infrastructure.Models;

public record GoogleReservation
{
    public string GoogleReservationId { get; set; } = string.Empty;
    public string GoogleReservationStatus { get; set; } = string.Empty;
    public string GoogleReservationName { get; set; } = string.Empty;
    public string GoogleReservationStartTime { get; set; } = string.Empty;
    public string GoogleReservationEndTime { get; set; } = string.Empty;
}
