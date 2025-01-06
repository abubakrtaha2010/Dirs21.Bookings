namespace Dirs21.Bookings.Domain.Models.Errors;

public record ErrorResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public ErrorType ErrorType { get; set; }
    public string UserMessage { get; set; } = string.Empty;
    public string InternalMessage { get; set; } = string.Empty;
    public string MoreInfo { get; set; } = string.Empty;
}
