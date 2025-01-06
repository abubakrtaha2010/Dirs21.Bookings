namespace Dirs21.Bookings.Application.Services.Mappings;

/// <summary>
/// Interface for mapping services to handle data transformations between different types.
/// </summary>
public interface IMappingService
{
    /// <summary>
    /// Retrieves a mapping configuration asynchronously.
    /// </summary>
    /// <param name="key">The key identifying the mapping.</param>
    /// <param name="sourceType">The type of the source data.</param>
    /// <param name="targetType">The type of the target data.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the mapping configuration as a string.</returns>
    Task<string> GetMappingAsync(string key, string sourceType, string targetType);

    /// <summary>
    /// Saves a mapping configuration asynchronously.
    /// </summary>
    /// <param name="key">The key identifying the mapping.</param>
    /// <param name="sourceType">The type of the source data.</param>
    /// <param name="targetType">The type of the target data.</param>
    /// <param name="inputMapping">The mapping configuration to save.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the saved mapping configuration as a string.</returns>
    Task<string> SaveMappingAsync(string key, string sourceType, string targetType, string inputMapping);

    /// <summary>
    /// Maps source data to target data asynchronously using a specified mapping configuration.
    /// </summary>
    /// <param name="key">The key identifying the mapping.</param>
    /// <param name="sourceType">The type of the source data.</param>
    /// <param name="targetType">The type of the target data.</param>
    /// <param name="sourceData">The source data to be mapped.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the mapped data as a string.</returns>
    Task<string> MapDataAsync(string key, string sourceType, string targetType, string sourceData);
}
