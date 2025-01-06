namespace Dirs21.Bookings.Domain.Services;

public interface ICacheService
{
    T? GetValue<T>(string key);
    T SetValue<T>(string key, T value);
}
