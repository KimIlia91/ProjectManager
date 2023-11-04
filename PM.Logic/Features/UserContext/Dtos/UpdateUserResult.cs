using Mapster;
using PM.Domain.Entities;

namespace PM.Application.Features.EmployeeContext.Dtos;

/// <summary>
/// Represents the result of a user update operation.
/// </summary>
public sealed class UpdateUserResult : IRegister
{
    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public string LastName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the middle name of the user (if available).
    /// </summary>
    public string? MiddleName { get; set; }

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Gets or sets the role name of the user.
    /// </summary>
    public string RoleName { get; set; } = null!;

    /// <summary>
    /// Registers type mapping using TypeAdapterConfig.
    /// </summary>
    /// <param name="config">The TypeAdapterConfig instance to register the mapping.</param>
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(User, string), UpdateUserResult>()
            .Map(dest => dest.RoleName, src => src.Item2)
            .Map(dest => dest, src => src.Item1);
    }
}