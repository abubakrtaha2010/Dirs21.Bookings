using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Dirs21.Bookings.Infrastructure.Repositories;

/// <summary>
/// Repository for handling mappings between different types.
/// </summary>
public class MappingRepository(IOptions<DatabaseSettings> options) : IMappingRepository, IDisposable
{
    internal const string SourceTypeKey = "SourceType";
    internal const string TargetTypeKey = "TargetType";
    internal const string MappingKey = "Mapping";

    private readonly LiteDatabase _database = new(options.Value.ConnectionString);

    /// <summary>
    /// Retrieves a mapping asynchronously based on the provided key, source type, and target type.
    /// </summary>
    /// <param name="key">The key identifying the mapping.</param>
    /// <param name="sourceType">The source type of the mapping.</param>
    /// <param name="targetType">The target type of the mapping.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the mapping as a string, or null if not found.</returns>
    public Task<string?> GetMappingAsync(string key, string sourceType, string targetType)
    {
        var collection = _database.GetCollection<BsonDocument>(key);

        var document = collection.FindOne(x => x[SourceTypeKey] == sourceType && x[TargetTypeKey] == targetType);
        if (document?.ContainsKey(MappingKey) != true) return Task.FromResult((string?)null);

        return Task.FromResult<string?>(document[MappingKey].AsString);
    }

    /// <summary>
    /// Saves a mapping asynchronously based on the provided key, source type, target type, and input mapping.
    /// </summary>
    /// <param name="key">The key identifying the mapping.</param>
    /// <param name="sourceType">The source type of the mapping.</param>
    /// <param name="targetType">The target type of the mapping.</param>
    /// <param name="inputMapping">The input mapping to be saved.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the saved mapping as a string.</returns>
    /// <exception cref="SaveMappingException">Thrown when the mapping could not be saved or retrieved.</exception>
    public async Task<string> SaveMappingAsync(string key, string sourceType, string targetType, string inputMapping)
    {
        var collection = _database.GetCollection<BsonDocument>(key);

        bool isSaved;

        var document = collection.FindOne(x => x[SourceTypeKey] == sourceType && x[TargetTypeKey] == targetType);
        if (document is not null)
        {
            document[MappingKey] = inputMapping;

            isSaved = collection.Update(document);
        }
        else
        {
            document = new BsonDocument
            {
                [SourceTypeKey] = sourceType,
                [TargetTypeKey] = targetType,
                [MappingKey] = inputMapping
            };

            isSaved = collection.Insert(document).AsObjectId.Pid != 0;
        }

        if (!isSaved)
        {
            var message = JsonSerializer.Serialize(new ErrorResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
                ErrorType = ErrorType.SaveFailure,
                UserMessage = "Mapping was not saved successfully.",
                InternalMessage =
                    $"Error: {nameof(ErrorType.SaveFailure)}.{Environment.NewLine}" +
                    "Mapping was not saved successfully to database. Please check the input parameter values or connection to the database.",
                MoreInfo =
                    $"Parameters: {new { key, sourceType, targetType, inputMapping }}."
            });

            throw new SaveMappingException(message);
        }

        var savedMapping = await GetMappingAsync(key, sourceType, targetType).ConfigureAwait(false);
        if (savedMapping is null)
        {
            var message = JsonSerializer.Serialize(new ErrorResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
                ErrorType = ErrorType.GetFailure,
                UserMessage = "Mapping is not found.",
                InternalMessage =
                    $"Error: {nameof(ErrorType.GetFailure)}.{Environment.NewLine}" +
                    "Mapping is not found after saving at database. Please check the saved data or connection to the database.",
                MoreInfo =
                    $"Parameters: {new { key, sourceType, targetType, inputMapping }}."
            });

            throw new SaveMappingException(message);
        }

        return savedMapping;
    }

    private bool Disposed { get; set; }

    /// <summary>
    /// Releases the unmanaged resources used by the MappingRepository and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (Disposed) return;

        if (disposing) _database.Dispose();

        Disposed = true;
    }

    /// <summary>
    /// Releases all resources used by the MappingRepository.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
