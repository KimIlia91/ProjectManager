using Mapster;
using PM.Domain.Common.Enums;
using PM.Domain.Entities;

namespace PM.Application.Features.ProjectContext.Dtos;

/// <summary>
/// Represents the result of updating project information.
/// </summary>
public sealed class UpdateProjectResult : IRegister
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string CustomerCompany { get; set; } = null!;

    public string ExecutorCompany { get; set; } = null!;

    public int ManagerId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public Priority Priority { get; set; }

    /// <summary>
    /// Registers type mapping for AutoMapper.
    /// </summary>
    /// <param name="config">The type adapter configuration.</param>
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Project, UpdateProjectResult>()
            .Map(dest => dest.ManagerId, src => src.Manager.Id);
    }
}