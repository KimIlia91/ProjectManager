using ErrorOr;
using MediatR;
using PM.Application.Common.Models.Project;
using PM.Application.Features.ProjectContext.Dtos;

namespace PM.Application.Features.ProjectContext.Queries.GetManagerProjects;

public sealed class GetManagerProjectListQuery
    : IRequest<ErrorOr<List<GetManagerProjectListResult>>>
{
    public ProjectFilter Filetr { get; set; } = new();

    public string? SotrBy { get; set; }
}