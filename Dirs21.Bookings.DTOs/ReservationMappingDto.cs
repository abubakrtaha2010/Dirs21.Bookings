namespace Dirs21.Bookings.DTOs;

public record ReservationMappingDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("sourceType")]
    public string SourceType { get; set; } = string.Empty;

    [JsonPropertyName("targetType")]
    public string TargetType { get; set; } = string.Empty;

    [JsonPropertyName("mapping")]
    public string Mapping { get; set; } = string.Empty;
}
