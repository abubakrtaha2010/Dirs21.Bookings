namespace Dirs21.Bookings.Tests;

public class MappingServiceTests
{
    private readonly Mock<ICacheService> _mockCacheService = new();
    private readonly Mock<IMappingRepository> _mockMappingRepository = new();
    private readonly MappingService _service;

    public MappingServiceTests() => _service = new MappingService(_mockCacheService.Object, _mockMappingRepository.Object);

    [Fact]
    public async Task GetMappingAsync_ReturnsMapping_WhenMappingExists()
    {
        // Arrange
        const string key = "testKey";
        const string sourceType = "sourceType";
        const string targetType = "targetType";
        const string expectedMapping = "expectedMapping";

        _ = _mockMappingRepository.Setup(repo => repo.GetMappingAsync(key, sourceType, targetType)).ReturnsAsync(expectedMapping);
        _ = _mockCacheService.Setup(cache => cache.GetValue<string>(It.IsAny<string>())).Returns((string?)null);
        _ = _mockCacheService.Setup(cache => cache.SetValue(It.IsAny<string>(), It.IsAny<string>())).Returns(expectedMapping);

        // Act
        var result = await _service.GetMappingAsync(key, sourceType, targetType);

        // Assert
        Assert.Equal(expectedMapping, result);
    }

    [Fact]
    public async Task SaveMappingAsync_ReturnsSavedMapping_WhenMappingSaved()
    {
        // Arrange
        const string key = "testKey";
        const string sourceType = "sourceType";
        const string targetType = "targetType";
        const string inputMapping = "var x = 3;";
        const string expectedMapping = "expectedMapping";

        _ = _mockMappingRepository.Setup(repo => repo.SaveMappingAsync(key, sourceType, targetType, inputMapping)).ReturnsAsync(expectedMapping);
        _ = _mockCacheService.Setup(cache => cache.SetValue(It.IsAny<string>(), It.IsAny<string>())).Returns(expectedMapping);

        // Act
        var result = await _service.SaveMappingAsync(key, sourceType, targetType, inputMapping);

        // Assert
        Assert.Equal(expectedMapping, result);
    }

    [Fact]
    public async Task MapDataAsync_ReturnsMappedData_WhenDataMapped()
    {
        // Arrange
        const string key = "testKey";
        const string sourceType = "sourceType";
        const string targetType = "targetType";
        const string sourceData = "sourceData";
        const string mapping = "return string.Empty;";
        var expectedMapping = string.Empty;

        _ = _mockMappingRepository.Setup(repo => repo.GetMappingAsync(key, sourceType, targetType)).ReturnsAsync(mapping);
        _ = _mockCacheService.Setup(cache => cache.GetValue<string>(It.IsAny<string>())).Returns((string?)null);

        // Act
        var result = await _service.MapDataAsync(key, sourceType, targetType, sourceData);

        // Assert
        Assert.Equal(expectedMapping, result);
    }
}
