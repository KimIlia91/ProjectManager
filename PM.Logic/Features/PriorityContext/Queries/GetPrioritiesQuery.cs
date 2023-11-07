using ErrorOr;
using MediatR;
using PM.Domain.Common.Extensions;

namespace PM.Application.Features.PriorityContext.Queries;

public sealed record GetPrioritiesQuery : IRequest<ErrorOr<List<EnumResult>>>;
