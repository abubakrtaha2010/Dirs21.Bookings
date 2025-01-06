namespace Dirs21.Bookings.API.Utilities.Swagger;

public class VersionDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        ArgumentNullException.ThrowIfNull(swaggerDoc);

        var paths = new OpenApiPaths();

        foreach (var path in swaggerDoc.Paths) paths.Add(path.Key.Replace("v{version}", swaggerDoc.Info.Version, StringComparison.Ordinal), path.Value);

        swaggerDoc.Paths = paths;
    }
}
