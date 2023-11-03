namespace PM.Application.Features.RoleContext.Dtos;

/// <summary>
/// Represents a result containing role information.
/// </summary>
public class GetRoleListResult
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}