namespace Dirs21.Bookings.API.Utilities.Swagger;

public class VersionOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        ArgumentNullException.ThrowIfNull(operation);

        var versionParameter = operation.Parameters.Single(p => p.Name == "version");
        _ = operation.Parameters.Remove(versionParameter);
    }
}
