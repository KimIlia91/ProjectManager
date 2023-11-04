namespace PM.Domain.Entities;

/// <summary>
/// Represents a base entity with an identifier.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public int Id { get; protected set; }
}
