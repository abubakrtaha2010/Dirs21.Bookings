namespace Dirs21.Bookings.DTOs;

public record ReservationMappingQueryParameterDto
{
    [JsonPropertyName("sourceType")]
    public string? SourceType { get; set; }

    [JsonPropertyName("targetType")]
    public string? TargetType { get; set; }
}
