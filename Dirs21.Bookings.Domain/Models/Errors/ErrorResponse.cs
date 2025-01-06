namespace Dirs21.Bookings.Domain.Models.Errors;

/// <summary>
/// Represents an error response with details about the error.
/// </summary>
public record ErrorResponse
{
    /// <summary>
    /// Gets or sets the HTTP status code associated with the error.
    /// </summary>
    public HttpStatusCode StatusCode { get; set; }

    /// <summary>
    /// Gets or sets the type of error.
    /// </summary>
    public ErrorType ErrorType { get; set; }

    /// <summary>
    /// Gets or sets the user-friendly message describing the error.
    /// </summary>
    public string UserMessage { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the internal message describing the error.
    /// </summary>
    public string InternalMessage { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets additional information about the error.
    /// </summary>
    public string MoreInfo { get; set; } = string.Empty;
}
