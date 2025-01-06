namespace Dirs21.Bookings.Domain.Exceptions;

public class SaveMappingException : Exception
{
    public SaveMappingException() { }

    public SaveMappingException(string? message) : base(message) { }

    public SaveMappingException(string? message, Exception? innerException) : base(message, innerException) { }
}
