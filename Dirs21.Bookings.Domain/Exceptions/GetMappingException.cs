namespace Dirs21.Bookings.Domain.Exceptions;

/// <summary>
/// Represents errors that occur during the mapping process.
/// </summary>
public class GetMappingException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetMappingException"/> class.
    /// </summary>
    public GetMappingException() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetMappingException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public GetMappingException(string? message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetMappingException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public GetMappingException(string? message, Exception? innerException) : base(message, innerException) { }
}
