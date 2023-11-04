using ErrorOr;
using MediatR;
using PM.Application.Features.StatusContext.Dtos;

namespace PM.Application.Features.StatusContext.Queries.GetStatusList;

public sealed record GetStatusListQuery : IRequest<ErrorOr<List<GetStatusListResult>>>;
