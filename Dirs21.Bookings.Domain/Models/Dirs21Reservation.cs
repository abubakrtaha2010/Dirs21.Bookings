namespace Dirs21.Bookings.Domain.Models;

public record Dirs21Reservation
{
    public string Dirs21ReservationId { get; set; } = string.Empty;
    public string Dirs21ReservationStatus { get; set; } = string.Empty;
    public string Dirs21ReservationName { get; set; } = string.Empty;
    public string Dirs21ReservationStartTime { get; set; } = string.Empty;
    public string Dirs21ReservationEndTime { get; set; } = string.Empty;
}
