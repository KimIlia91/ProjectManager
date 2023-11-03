using ErrorOr;
using MediatR;
using PM.Application.Features.ProjectContext.Dtos;

namespace PM.Application.Features.ProjectContext.Queries.GetProject;

/// <summary>
/// Represents a query to retrieve project information by its ID.
/// </summary>
public sealed record GetProjectQuery(
    int Id) : IRequest<ErrorOr<GetProjectResult>>;
