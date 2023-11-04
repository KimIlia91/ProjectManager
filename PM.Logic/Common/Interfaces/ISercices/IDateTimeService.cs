namespace PM.Application.Common.Interfaces.ISercices;

/// <summary>
/// Service interface for retrieving the current Coordinated Universal Time (UTC) date and time.
/// </summary>
public interface IDateTimeService
{
    /// <summary>
    /// Gets the current Coordinated Universal Time (UTC) date and time.
    /// </summary>
    DateTime UtcNow { get; }
}
