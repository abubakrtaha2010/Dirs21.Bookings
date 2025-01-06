namespace Dirs21.Bookings.API.Utilities.Swagger;

/// <summary>
/// A document filter that replaces the version placeholder in the Swagger document paths with the actual version.
/// </summary>
public class VersionDocumentFilter : IDocumentFilter
{
    /// <summary>
    /// Applies the filter to the Swagger document.
    /// </summary>
    /// <param name="swaggerDoc">The Swagger document to modify.</param>
    /// <param name="context">The context for the document filter.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="swaggerDoc"/> is null.</exception>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        ArgumentNullException.ThrowIfNull(swaggerDoc);

        var paths = new OpenApiPaths();

        foreach (var path in swaggerDoc.Paths)
        {
            paths.Add(path.Key.Replace("v{version}", swaggerDoc.Info.Version, StringComparison.Ordinal), path.Value);
        }

        swaggerDoc.Paths = paths;
    }
}
