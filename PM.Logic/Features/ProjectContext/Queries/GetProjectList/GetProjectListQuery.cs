using ErrorOr;
using MediatR;
using PM.Application.Common.Models.Project;
using PM.Application.Features.ProjectContext.Dtos;

namespace PM.Application.Features.ProjectContext.Queries.GetProjectList;

public sealed class GetProjectListQuery
    : IRequest<ErrorOr<List<GetProjectListResult>>>
{
    public ProjectFilter Filter { get; set; } = new();

    public string? SortBy { get; set; }
}
