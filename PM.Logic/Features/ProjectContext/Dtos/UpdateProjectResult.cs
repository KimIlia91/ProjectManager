using Mapster;
using PM.Domain.Common.Enums;
using PM.Domain.Entities;

namespace PM.Application.Features.ProjectContext.Dtos;

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

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Project, UpdateProjectResult>()
            .Map(dest => dest.ManagerId, src => src.Manager.Id);
    }
}