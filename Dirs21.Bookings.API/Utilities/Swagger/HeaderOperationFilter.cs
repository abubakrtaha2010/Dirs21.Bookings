namespace Dirs21.Bookings.API.Utilities.Swagger;

/// <summary>
/// A filter that adds a custom header parameter to the Swagger documentation.
/// </summary>
public class HeaderOperationFilter : IOperationFilter
{
    /// <summary>
    /// Applies the filter to the specified operation.
    /// </summary>
    /// <param name="operation">The operation to which the filter is applied.</param>
    /// <param name="context">The context in which the filter is applied.</param>
    /// <exception cref="ArgumentNullException">Thrown when the operation is null.</exception>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        ArgumentNullException.ThrowIfNull(operation);

        operation.Parameters ??= [];

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "X-TrackingId",
            In = ParameterLocation.Header,
            Required = false,
            Schema = new OpenApiSchema
            {
                Type = "string",
                Default = new OpenApiString(Guid.NewGuid().ToString("D"))
            }
        });
    }
}
