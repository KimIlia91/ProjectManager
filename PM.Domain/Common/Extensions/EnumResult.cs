namespace PM.Domain.Common.Extensions;

/// <summary>
/// Represents the result of an enumeration value.
/// </summary>
public class EnumResult
{
    /// <summary>
    /// Gets or sets the unique identifier for the enumeration value.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the enumeration value.
    /// </summary>
    public string Name { get; set; } = null!;
}
