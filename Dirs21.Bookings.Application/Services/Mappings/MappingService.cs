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

        return cache.SetValue(cacheKey, repoMapping);
    }

    public async Task<string> SaveMappingAsync(string key, string sourceType, string targetType, string inputMapping)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);
        ArgumentException.ThrowIfNullOrEmpty(sourceType);
        ArgumentException.ThrowIfNullOrEmpty(targetType);
        ArgumentException.ThrowIfNullOrEmpty(inputMapping);

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

        var mapping = await GetMappingAsync(key, sourceType, targetType).ConfigureAwait(false);
        var options = GetScriptOptions();
        var globals = new ScriptGlobals { Parameter = sourceData };

        var targetData = await CSharpScript.EvaluateAsync<string>(mapping, options, globals).ConfigureAwait(false);

        return targetData;
    }

    private static string GetCacheKey(string key, string sourceType, string targetType) => $"{key}:{sourceType}:{targetType}";

    private static ScriptOptions GetScriptOptions() => ScriptOptions.Default
        .AddReferences(typeof(JsonSerializer).Assembly)
        .AddReferences(typeof(GoogleReservation).Assembly)
        .AddReferences(typeof(Dirs21Reservation).Assembly)
        .AddReferences(typeof(ScriptGlobals).Assembly)
        .AddImports("System", "System.Text.Json", "Dirs21.Bookings.Infrastructure.Models", "Dirs21.Bookings.Domain.Models");
}