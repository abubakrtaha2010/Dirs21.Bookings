namespace Dirs21.Bookings.Domain.Models.Errors;

/// <summary>
/// Represents the types of errors that can occur in the booking domain.
/// </summary>
public enum ErrorType
{
    /// <summary>
    /// The error type is not defined.
    /// </summary>
    Undefined,

    /// <summary>
    /// The requested resource was not found.
    /// </summary>
    ResourceNotFound,

    /// <summary>
    /// There was a failure in retrieving data.
    /// </summary>
    GetFailure,

    /// <summary>
    /// There was a failure in saving data.
    /// </summary>
    SaveFailure,

    /// <summary>
    /// There was a failure in mapping data.
    /// </summary>
    MapFailure,

    /// <summary>
    /// An error occurred that does not fit into the other categories.
    /// </summary>
    Other
}
