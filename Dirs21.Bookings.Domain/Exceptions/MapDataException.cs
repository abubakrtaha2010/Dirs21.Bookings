namespace Dirs21.Bookings.Domain.Exceptions;

/// <summary>
/// Represents errors that occur during mapping data operations.
/// </summary>
public class MapDataException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MapDataException"/> class.
    /// </summary>
    public MapDataException() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="MapDataException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public MapDataException(string? message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="MapDataException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public MapDataException(string? message, Exception? innerException) : base(message, innerException) { }
}
