using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Dirs21.Bookings.Infrastructure.Repositories;

public class MappingRepository(IOptions<DatabaseSettings> options) : IMappingRepository, IDisposable
{
    private const string SourceTypeKey = "SourceType";
    private const string TargetTypeKey = "TargetType";
    private const string MappingKey = "Mapping";

    private readonly LiteDatabase _database = new(options.Value.ConnectionString);

    public Task<string> GetMappingAsync(string key, string sourceType, string targetType)
    {
        var collection = _database.GetCollection<BsonDocument>(key);

        var document = collection.FindOne(x => x[SourceTypeKey] == sourceType && x[TargetTypeKey] == targetType);
        if (document is null || !document.ContainsKey(MappingKey))
        {
            var message = JsonSerializer.Serialize(new ErrorResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                ErrorType = ErrorType.ResourceNotFound,
                UserMessage = "Mapping is not found.",
                InternalMessage =
                    $"Error: {nameof(ErrorType.ResourceNotFound)}.{Environment.NewLine}" +
                    "Mapping is not found at database. Please check the input parameter values, or add the missing mapping if not exists.",
                MoreInfo =
                    $"Parameters: {new { key, sourceType, targetType }}."
            });

            throw new GetMappingException(message);
        }

        return Task.FromResult(document[MappingKey].AsString);
    }

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

    protected virtual void Dispose(bool disposing)
    {
        if (Disposed) return;

        if (disposing) _database.Dispose();

        Disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
