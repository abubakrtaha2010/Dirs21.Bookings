namespace Dirs21.Bookings.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services) => services.AddScoped<IMappingService, MappingService>();
}