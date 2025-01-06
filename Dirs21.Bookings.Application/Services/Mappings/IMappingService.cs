namespace Dirs21.Bookings.Application.Services.Mappings;

public interface IMappingService
{
    Task<string> GetMappingAsync(string key, string sourceType, string targetType);
    Task<string> SaveMappingAsync(string key, string sourceType, string targetType, string inputMapping);
    Task<string> MapDataAsync(string key, string sourceType, string targetType, string sourceData);
}
