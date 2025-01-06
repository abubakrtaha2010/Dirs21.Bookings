namespace Dirs21.Bookings.Domain.Exceptions;

public class GetMappingException : Exception
{
    public GetMappingException() { }

    public GetMappingException(string? message) : base(message) { }

    public GetMappingException(string? message, Exception? innerException) : base(message, innerException) { }
}
