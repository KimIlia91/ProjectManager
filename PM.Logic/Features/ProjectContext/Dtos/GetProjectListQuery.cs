using MediatR;
using PM.Application.Common.Models.Project;
using PM.Application.Features.ProjectContext.Queries.GetProjectList;

namespace PM.Application.Features.ProjectContext.Dtos;

public sealed class GetProjectListQuery
    : IRequest<List<GetProjectListResult>>
{
    public ProjectFilter Filter { get; set; } = new();

    public string? SortBy { get; set; }
}
