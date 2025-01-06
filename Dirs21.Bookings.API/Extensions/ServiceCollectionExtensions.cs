namespace Dirs21.Bookings.API.Extensions;

public static class ServiceCollectionExtensions
{
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