using ErrorOr;
using MediatR;
using PM.Application.Features.ProjectContext.Dtos;

namespace PM.Application.Features.ProjectContext.Queries.GetCurrentUserProject;

public sealed record GetCurrentUserProjectQuery(
    int ProjectId) : IRequest<ErrorOr<GetProjectResult>>;
