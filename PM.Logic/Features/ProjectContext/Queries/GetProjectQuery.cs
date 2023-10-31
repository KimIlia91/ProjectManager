using ErrorOr;
using MediatR;
using PM.Application.Features.ProjectContext.Dtos;

namespace PM.Application.Features.ProjectContext.Queries;

public sealed record GetProjectQuery(
    int Id) : IRequest<ErrorOr<GetProjectResult>>;
