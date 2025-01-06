namespace Dirs21.Bookings.DTOs;

public record CreateReservationMappingDto
{
    [Required]
    [MaxLength(100)]
    [JsonPropertyName("sourceType")]
    public string? SourceType { get; set; }

    [Required]
    [MaxLength(100)]
    [JsonPropertyName("targetType")]
    public string? TargetType { get; set; }

    [Required]
    [JsonPropertyName("mapping")]
    public string? Mapping { get; set; }
}
