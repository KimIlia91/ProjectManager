using PM.Application.Common.Interfaces.ISercices;

namespace PM.Infrastructure.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime UtcNow => DateTime.UtcNow;
}
