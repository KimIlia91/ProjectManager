using ErrorOr;
using MediatR;
using PM.Domain.Common.Extensions;

namespace PM.Application.Features.PriorityContext.Queries;

/// <summary>
/// Represents a query to retrieve a list of priority values.
/// </summary>
public sealed record GetPrioritiesQuery : IRequest<ErrorOr<List<EnumResult>>>;

