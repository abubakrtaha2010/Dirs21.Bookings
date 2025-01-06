namespace Dirs21.Bookings.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
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
