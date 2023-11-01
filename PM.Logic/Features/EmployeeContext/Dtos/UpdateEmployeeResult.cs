using Mapster;
using PM.Domain.Entities;

namespace PM.Application.Features.EmployeeContext.Dtos;

public sealed class UpdateEmployeeResult : IRegister
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MiddelName { get; set; }

    public string Email { get; set; } = null!;

    public string RoleName { get; set; } = null!;

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(Employee, string), UpdateEmployeeResult>()
            .Map(dest => dest.RoleName, src => src.Item2)
            .Map(dest => dest, src => src.Item1);
    }
}