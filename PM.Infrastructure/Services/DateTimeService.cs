using PM.Application.Common.Interfaces.ISercices;

namespace PM.Infrastructure.Services;

/// <summary>
/// Implementation of the <see cref="IDateTimeService"/> interface that provides the current
/// Coordinated Universal Time (UTC) date and time.
/// </summary>
public class DateTimeService : IDateTimeService
{
    /// <inheritdoc />
    public DateTime UtcNow => DateTime.UtcNow;
}
