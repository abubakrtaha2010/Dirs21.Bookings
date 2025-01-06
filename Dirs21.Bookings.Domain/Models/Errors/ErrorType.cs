namespace Dirs21.Bookings.Domain.Models.Errors;

public enum ErrorType
{
    Undefined,
    ResourceNotFound,
    GetFailure,
    SaveFailure,
    MapFailure,
    Other
}