using ErrorOr;
using MediatR;
using PM.Application.Common.Models.Project;
using PM.Application.Features.ProjectContext.Dtos;

namespace PM.Application.Features.ProjectContext.Queries.GetUserProjectList;

/// <summary>
/// Represents a query to retrieve a list of projects for a user.
/// </summary>
public sealed class GetProjectListOfUserQuery
    : IRequest<ErrorOr<List<GetProjectListResult>>>
{
    /// <summary>
    /// Filter
    /// </summary>
    public ProjectFilter Filetr { get; set; } = new();

    /// <summary>
    /// Sort by
    /// </summary>
    public string? SotrBy { get; set; }
}