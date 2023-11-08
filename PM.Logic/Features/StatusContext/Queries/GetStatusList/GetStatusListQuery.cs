using ErrorOr;
using MediatR;
using PM.Domain.Common.Extensions;

namespace PM.Application.Features.StatusContext.Queries.GetStatusList;

/// <summary>
/// Represents a query to retrieve a list of statuses.
/// </summary>
public sealed record GetStatusListQuery : IRequest<ErrorOr<List<EnumResult>>>;
