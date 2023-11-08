namespace PM.Application.Common.Interfaces.ISercices;

/// <summary>
/// An interface representing a service for checking access to entities of type T.
/// </summary>
/// <typeparam name="T">The type of entity for which access is checked.</typeparam>
public interface IAccessService<T> where T : class
{
    /// <summary>
    /// Gets a value indicating whether access is granted for entities of type T.
    /// </summary>
    bool Access { get; }
}
