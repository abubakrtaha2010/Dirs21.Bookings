namespace Dirs21.Bookings.API.Extensions;

/// <summary>
/// Extension methods for setting up API services in an <see cref="IServiceCollection" />.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds API services to the specified <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        _ = services.AddControllers();

        _ = services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader());
        });

        _ = services.AddEndpointsApiExplorer();

        _ = services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1.0", new()
            {
                Title = "DIRS21 - Bookings",
                Description = "An API which provides endpoints for managing hotel bookings at DIRS21 backend.",
                Version = "v1.0"
            });

            options.DocumentFilter<VersionDocumentFilter>();
            options.OperationFilter<VersionOperationFilter>();
            options.OperationFilter<HeaderOperationFilter>();

            options.AddSecurityDefinition("Bearer", new()
            {
                Name = "Authorization",
                Description = "Add a JWT Bearer Token with authorization",
                Type = SecuritySchemeType.Http,
                In = ParameterLocation.Header,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });

            options.AddSecurityRequirement(new()
            {
                {
                    new() { Reference = new() { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
                    Array.Empty<string>()
                }
            });

            options.EnableAnnotations();
            options.SupportNonNullableReferenceTypes();
            options.DescribeAllParametersInCamelCase();
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
        });

        return services;
    }
}
