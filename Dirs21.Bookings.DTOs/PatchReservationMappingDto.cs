namespace Dirs21.Bookings.DTOs;

public record PatchReservationMappingDto
{
    [MaxLength(100)]
    [JsonPropertyName("sourceType")]
    public string? SourceType { get; set; }

    [MaxLength(100)]
    [JsonPropertyName("targetType")]
    public string? TargetType { get; set; }

    [JsonPropertyName("mapping")]
    public string? Mapping { get; set; }
}
