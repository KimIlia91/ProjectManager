using PM.Application.Common.Interfaces.ISercices;

namespace PM.Infrastructure.Services;

/// <summary>
/// A generic service for checking access to entities of type T.
/// </summary>
/// <typeparam name="T">The type of entity for which access is checked.</typeparam>
public class AccessService<T> : IAccessService<T> where T : class
{
    /// <summary>
    /// Gets or sets the access status for entities of type T.
    /// </summary>
    public bool Access { get; protected set; }
}
