using ErrorOr;
using MediatR;
using PM.Application.Common.Models.Project;
using PM.Application.Features.ProjectContext.Dtos;

namespace PM.Application.Features.ProjectContext.Queries.GetUserProjectList;

/// <summary>
/// Represents a query to retrieve a list of projects for a user.
/// </summary>
public sealed class GetUserProjectListQuery
    : IRequest<ErrorOr<List<GetProjectListResult>>>
{
    public ProjectFilter Filetr { get; set; } = new();

    public string? SotrBy { get; set; }
}