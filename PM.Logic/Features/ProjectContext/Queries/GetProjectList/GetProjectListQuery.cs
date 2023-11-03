using ErrorOr;
using MediatR;
using PM.Application.Common.Models.Project;
using PM.Application.Features.ProjectContext.Dtos;

namespace PM.Application.Features.ProjectContext.Queries.GetProjectList;

/// <summary>
/// Represents a query to retrieve a list of project information.
/// </summary>
public sealed class GetProjectListQuery
    : IRequest<ErrorOr<List<GetProjectListResult>>>
{
    public ProjectFilter Filter { get; set; } = new();

    public string? SortBy { get; set; }
}
