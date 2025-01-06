namespace Dirs21.Bookings.Domain.Models.Errors;

public enum ErrorType
{
    Undefined,
    ResourceNotFound,
    GetFailure,
    SaveFailure,
    Other
}