namespace Dirs21.Bookings.Tests;

public class MainTests
{
    private const string ReservationKey = "Reservation";
    private const string ReservationSourceType = "GoogleReservation";
    private const string ReservationTargetType = "Dirs21Reservation";

    private const string ReservationSourceData = """
{
  "GoogleReservationId": "1",
  "GoogleReservationStatus": "confirmed",
  "GoogleReservationName": "booking",
  "GoogleReservationStartTime": "00:00:00",
  "GoogleReservationEndTime": "23:59:59"
}
""";

    private const string ReservationTargetData = """
{
  "Id": "1",
  "Status": "confirmed",
  "Name": "booking",
  "StartTime": "00:00:00",
  "EndTime": "23:59:59"
}
""";

    private const string ReservationMapping = """
var googleReservation = System.Text.Json.JsonSerializer.Deserialize<GoogleReservation>(Parameter); 
if (googleReservation is null) throw new InvalidOperationException($"Data is not a valid JSON of {typeof(GoogleReservation)}."); 

var dirs21Reservation = new Dirs21.Bookings.Domain.Models.Dirs21Reservation 
{ 
    Id = googleReservation.GoogleReservationId, 
    Name = googleReservation.GoogleReservationName, 
    StartTime = googleReservation.GoogleReservationStartTime, 
    EndTime = googleReservation.GoogleReservationEndTime, 
    Status = googleReservation.GoogleReservationStatus 
}; 

return JsonSerializer.Serialize(dirs21Reservation, new JsonSerializerOptions { WriteIndented = true }); 

public record GoogleReservation 
{ 
    public string GoogleReservationId { get; set; } = string.Empty; 
    public string GoogleReservationStatus { get; set; } = string.Empty; 
    public string GoogleReservationName { get; set; } = string.Empty; 
    public string GoogleReservationStartTime { get; set; } = string.Empty; 
    public string GoogleReservationEndTime { get; set; } = string.Empty; 
}
""";

    private readonly Mock<ICacheService> _mockCacheService = new();
    private readonly Mock<IMappingRepository> _mockMappingRepository = new();
    private readonly MappingService _mappingService;
    private readonly MappingsController _controller;

    public MainTests()
    {
        _mappingService = new MappingService(_mockCacheService.Object, _mockMappingRepository.Object);
        _controller = new MappingsController(_mappingService);
    }

    [Fact]
    public async Task GetReservationMappingAsync_ReturnsOkResult_WithMapping()
    {
        // Arrange
        const string key = ReservationKey;
        const string sourceType = ReservationSourceType;
        const string targetType = ReservationTargetType;
        const string expectedMapping = ReservationMapping;

        _ = _mockCacheService.Setup(cache => cache.GetValue<string>(It.IsAny<string>())).Returns((string?)null);
        _ = _mockMappingRepository.Setup(repo => repo.GetMappingAsync(key, sourceType, targetType)).ReturnsAsync(expectedMapping);
        _ = _mockCacheService.Setup(cache => cache.SetValue(It.IsAny<string>(), It.IsAny<string>())).Returns(expectedMapping);

        // Act
        var result = await _controller.GetReservationMappingAsync(key, sourceType, targetType);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(expectedMapping, okResult.Value);
    }

    [Fact]
    public async Task SaveReservationMappingAsync_ReturnsOkResult_WithSavedMapping()
    {
        // Arrange
        const string key = ReservationKey;
        const string sourceType = ReservationSourceType;
        const string targetType = ReservationTargetType;
        const string inputMapping = ReservationMapping;

        _ = _mockMappingRepository.Setup(repo => repo.SaveMappingAsync(key, sourceType, targetType, inputMapping)).ReturnsAsync(ReservationMapping);
        _ = _mockCacheService.Setup(cache => cache.SetValue(It.IsAny<string>(), It.IsAny<string>())).Returns(ReservationMapping);

        // Act
        var result = await _controller.SaveReservationMappingAsync(key, sourceType, targetType, inputMapping);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(ReservationMapping, okResult.Value);
    }

    [Fact]
    public async Task MapReservationDataAsync_ReturnsOkResult_WithMappedData()
    {
        // Arrange
        const string key = ReservationKey;
        const string sourceType = ReservationSourceType;
        const string targetType = ReservationTargetType;
        const string sourceData = ReservationSourceData;
        const string expectedMapping = ReservationMapping;
        const string expectedMappedData = ReservationTargetData;

        _ = _mockCacheService.Setup(cache => cache.GetValue<string>(It.IsAny<string>())).Returns((string?)null);
        _ = _mockMappingRepository.Setup(repo => repo.GetMappingAsync(key, sourceType, targetType)).ReturnsAsync(expectedMapping);

        // Act
        var result = await _controller.MapReservationDataAsync(key, sourceType, targetType, sourceData);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(expectedMappedData, okResult.Value);
    }
}
