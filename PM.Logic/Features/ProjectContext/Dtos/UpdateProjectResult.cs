using Mapster;
using PM.Domain.Common.Enums;
using PM.Domain.Entities;

namespace PM.Application.Features.ProjectContext.Dtos;

public sealed class UpdateProjectResult : IRegister
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CustomerCompanyId { get; set; }

    public int ExecutorCompanyId { get; set; }

    public int ManagerId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public ProjectPriority Priority { get; set; }

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Project, UpdateProjectResult>()
            .Map(dest => dest.CustomerCompanyId, src => src.CustomerCompany.Id)
            .Map(dest => dest.ExecutorCompanyId, src => src.ExecutorCompany.Id)
            .Map(dest => dest.ManagerId, src => src.Manager.Id);
    }
}