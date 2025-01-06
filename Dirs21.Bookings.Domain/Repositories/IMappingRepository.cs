namespace Dirs21.Bookings.Domain.Repositories;

public interface IMappingRepository
{
    Task<string> GetMappingAsync(string key, string sourceType, string targetType);
    Task<string> SaveMappingAsync(string key, string sourceType, string targetType, string inputMapping);
}