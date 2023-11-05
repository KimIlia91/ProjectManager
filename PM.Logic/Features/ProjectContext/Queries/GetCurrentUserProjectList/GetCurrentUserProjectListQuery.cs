using ErrorOr;
using MediatR;
using PM.Application.Common.Models.Project;
using PM.Application.Features.ProjectContext.Dtos;

namespace PM.Application.Features.ProjectContext.Queries.GetCurrentUserProjectList;

/// <summary>
/// Represents a query to retrieve a list of projects for a user.
/// </summary>
public sealed class GetCurrentUserProjectListQuery
    : IRequest<ErrorOr<List<GetProjectListResult>>>
{
    /// <summary>
    /// Project filter
    /// </summary>
    public ProjectFilter Filetr { get; set; } = new();

    /// <summary>
    /// Sort by
    /// </summary>
    public string? SotrBy { get; set; }
}