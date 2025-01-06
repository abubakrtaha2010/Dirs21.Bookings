namespace Dirs21.Bookings.Domain.Repositories;

/// <summary>
/// Interface for mapping repository to handle mappings between different types.
/// </summary>
public interface IMappingRepository
{
    /// <summary>
    /// Retrieves a mapping asynchronously based on the provided key, source type, and target type.
    /// </summary>
    /// <param name="key">The key identifying the mapping.</param>
    /// <param name="sourceType">The source type of the mapping.</param>
    /// <param name="targetType">The target type of the mapping.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the mapping as a string, or null if not found.</returns>
    Task<string?> GetMappingAsync(string key, string sourceType, string targetType);

    /// <summary>
    /// Saves a mapping asynchronously based on the provided key, source type, target type, and input mapping.
    /// </summary>
    /// <param name="key">The key identifying the mapping.</param>
    /// <param name="sourceType">The source type of the mapping.</param>
    /// <param name="targetType">The target type of the mapping.</param>
    /// <param name="inputMapping">The input mapping to be saved.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the saved mapping as a string.</returns>
    Task<string> SaveMappingAsync(string key, string sourceType, string targetType, string inputMapping);
}
