namespace Dirs21.Bookings.Tests;

public class MappingsControllerTests
{
    private readonly Mock<IMappingService> _mockMappingService = new();
    private readonly MappingsController _controller;

    public MappingsControllerTests() => _controller = new MappingsController(_mockMappingService.Object);

    [Fact]
    public async Task GetReservationMappingAsync_ReturnsOkResult_WithMapping()
    {
        // Arrange
        const string key = "testKey";
        const string sourceType = "sourceType";
        const string targetType = "targetType";
        const string expectedMapping = "expectedMapping";

        _ = _mockMappingService.Setup(service => service.GetMappingAsync(key, sourceType, targetType)).ReturnsAsync(expectedMapping);

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
        const string key = "testKey";
        const string sourceType = "sourceType";
        const string targetType = "targetType";
        const string inputMapping = "inputMapping";
        const string expectedMapping = "expectedMapping";

        _ = _mockMappingService.Setup(service => service.SaveMappingAsync(key, sourceType, targetType, inputMapping)).ReturnsAsync(expectedMapping);

        // Act
        var result = await _controller.SaveReservationMappingAsync(key, sourceType, targetType, inputMapping);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(expectedMapping, okResult.Value);
    }

    [Fact]
    public async Task MapReservationDataAsync_ReturnsOkResult_WithMappedData()
    {
        // Arrange
        const string key = "testKey";
        const string sourceType = "sourceType";
        const string targetType = "targetType";
        const string sourceData = "sourceData";
        const string expectedMappedData = "expectedMappedData";

        _ = _mockMappingService.Setup(service => service.MapDataAsync(key, sourceType, targetType, sourceData)).ReturnsAsync(expectedMappedData);

        // Act
        var result = await _controller.MapReservationDataAsync(key, sourceType, targetType, sourceData);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(expectedMappedData, okResult.Value);
    }

    [Fact]
    public async Task GetReservationMappingAsync_ThrowsArgumentException_WhenSourceTypeIsNull()
    {
        // Arrange
        const string key = "testKey";
        const string? sourceType = null;
        const string targetType = "targetType";

        // Act
        async Task Act() => _ = await _controller.GetReservationMappingAsync(key, sourceType, targetType);

        // Assert
        _ = await Assert.ThrowsAsync<ArgumentNullException>(Act);
    }

    [Fact]
    public async Task GetReservationMappingAsync_ThrowsArgumentException_WhenTargetTypeIsNull()
    {
        // Arrange
        const string key = "testKey";
        const string sourceType = "sourceType";
        const string? targetType = null;

        // Act
        async Task Act() => _ = await _controller.GetReservationMappingAsync(key, sourceType, targetType);

        // Assert
        _ = await Assert.ThrowsAsync<ArgumentNullException>(Act);
    }
}
