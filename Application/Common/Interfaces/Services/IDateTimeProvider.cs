namespace Application.Common.Interfaces.Services;

public interface IDateTimeProvider
{
    public DateTime VnNow { get; }

    public DateTime UtcNow { get; }
}