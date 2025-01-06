namespace Dirs21.Bookings.API.Utilities.Swagger;

/// <summary>
/// A filter that removes the version parameter from the OpenAPI operation.
/// </summary>
public class VersionOperationFilter : IOperationFilter
{
    /// <summary>
    /// Applies the filter to the specified OpenAPI operation.
    /// </summary>
    /// <param name="operation">The OpenAPI operation to modify.</param>
    /// <param name="context">The context for the operation filter.</param>
    /// <exception cref="ArgumentNullException">Thrown when the operation is null.</exception>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        ArgumentNullException.ThrowIfNull(operation);

        var versionParameter = operation.Parameters.Single(p => p.Name == "version");
        _ = operation.Parameters.Remove(versionParameter);
    }
}
