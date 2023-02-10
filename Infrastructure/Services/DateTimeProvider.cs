using Application.Common.Interfaces.Services;

namespace Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime VnNow => DateTime.UtcNow.AddHours(7);

    public DateTime UtcNow => DateTime.UtcNow;
}