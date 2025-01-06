namespace Dirs21.Bookings.Infrastructure.Extensions;

/// <summary>
/// Extension methods for adding infrastructure services to the IServiceCollection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds infrastructure services to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <param name="configuration">The configuration to use for configuring services.</param>
    /// <returns>The IServiceCollection with the added services.</returns>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services
            .AddSingleton<ICacheService, CacheService>()
            .AddScoped<IMappingRepository, MappingRepository>()
            .Configure<CacheSettings>(configuration.GetSection("Cache"))
            .Configure<DatabaseSettings>(configuration.GetSection("Database"));

        return services;
    }
}
