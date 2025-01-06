namespace Dirs21.Bookings.Application.Services.Mappings;

public class MappingService(ICacheService cache, IMappingRepository repository) : IMappingService
{
    public async Task<string> GetMappingAsync(string key, string sourceType, string targetType)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);
        ArgumentException.ThrowIfNullOrEmpty(sourceType);
        ArgumentException.ThrowIfNullOrEmpty(targetType);

        var cacheKey = GetCacheKey(key, sourceType, targetType);

        var cachedMapping = cache.GetValue<string>(cacheKey);
        if (cachedMapping is not null) return cachedMapping;

        var repoMapping = await repository.GetMappingAsync(key, sourceType, targetType).ConfigureAwait(false);
        if (repoMapping is null)
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

        return cache.SetValue(cacheKey, repoMapping);
    }

    public async Task<string> SaveMappingAsync(string key, string sourceType, string targetType, string inputMapping)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);
        ArgumentException.ThrowIfNullOrEmpty(sourceType);
        ArgumentException.ThrowIfNullOrEmpty(targetType);
        ArgumentException.ThrowIfNullOrEmpty(inputMapping);

        var mapping = $"var Parameter = string.Empty; {inputMapping}";
        var options = GetScriptOptions();

        var diagnostics = CSharpScript.Create(mapping, options).Compile();
        if (diagnostics.Length > 0)
        {
            var message = JsonSerializer.Serialize(new ErrorResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                ErrorType = ErrorType.SaveFailure,
                UserMessage = "Mapping was not compiled successfully.",
                InternalMessage =
                    $"Error: {nameof(ErrorType.SaveFailure)}.{Environment.NewLine}" +
                    $"Mapping was not compiled successfully. Please check the input code for any errors.{Environment.NewLine}" +
                    string.Join(Environment.NewLine, diagnostics),
                MoreInfo =
                    $"Parameters: {new { key, sourceType, targetType, inputMapping }}."
            });

            throw new SaveMappingException(message);
        }

        var repoMapping = await repository.SaveMappingAsync(key, sourceType, targetType, inputMapping).ConfigureAwait(false);

        var cacheKey = GetCacheKey(key, sourceType, targetType);

        return cache.SetValue(cacheKey, repoMapping);
    }

    public async Task<string> MapDataAsync(string key, string sourceType, string targetType, string sourceData)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);
        ArgumentException.ThrowIfNullOrEmpty(sourceType);
        ArgumentException.ThrowIfNullOrEmpty(targetType);
        ArgumentException.ThrowIfNullOrEmpty(sourceData);

        var cacheKey = GetCacheKey(key, sourceType, targetType);

        var mapping = cache.GetValue<string>(cacheKey);
        if (mapping is null)
        {
            mapping = await repository.GetMappingAsync(key, sourceType, targetType).ConfigureAwait(false);
            if (mapping is null)
            {
                var message = JsonSerializer.Serialize(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorType = ErrorType.MapFailure,
                    UserMessage = "Mapping is not found.",
                    InternalMessage =
                        $"Error: {nameof(ErrorType.MapFailure)}.{Environment.NewLine}" +
                        "Mapping is not found at database. Please check the input parameter values, or add the missing mapping if not exists.",
                    MoreInfo =
                        $"Parameters: {new { key, sourceType, targetType, sourceData }}."
                });

                throw new MapDataException(message);
            }
        }

        var options = GetScriptOptions();
        var globals = new ScriptGlobals { Parameter = sourceData };

        var targetData = await CSharpScript.EvaluateAsync<string>(mapping, options, globals).ConfigureAwait(false);

        return targetData;
    }

    private static string GetCacheKey(string key, string sourceType, string targetType) => $"{key}:{sourceType}:{targetType}";

    private static ScriptOptions GetScriptOptions()
    {
        const string domainAssemblyName = "Dirs21.Bookings.Domain";

        var options = ScriptOptions.Default
            .AddReferences(typeof(JsonSerializer).Assembly)
            .AddReferences(typeof(ScriptGlobals).Assembly)
            .AddReferences(Assembly.Load(domainAssemblyName))
            .AddImports("System", "System.Text.Json", $"{domainAssemblyName}.Models");

        return options;
    }
}