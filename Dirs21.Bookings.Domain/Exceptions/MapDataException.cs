namespace Dirs21.Bookings.Domain.Exceptions;

public class MapDataException : Exception
{
    public MapDataException() { }

    public MapDataException(string? message) : base(message) { }

    public MapDataException(string? message, Exception? innerException) : base(message, innerException) { }
}
