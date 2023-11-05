namespace PM.Application.Common.Interfaces.ISercices;

/// <summary>
/// 
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// Gets the unique identifier (ID) of the current user.
    /// </summary>
    /// <remarks>
    /// This property attempts to retrieve the user's ID from the HTTP context's claims.
    /// If the user is authenticated and the ID is available, it will be returned.
    /// If the ID is not available or invalid, -1 is returned to indicate an unauthenticated user.
    /// </remarks>
    int UserId { get; }

    bool IsSupervisor { get; }
}
