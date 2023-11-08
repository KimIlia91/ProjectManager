using ErrorOr;
using MediatR;
using PM.Domain.Common.Enums;
using PM.Domain.Common.Extensions;

namespace PM.Application.Features.StatusContext.Queries.GetStatusList;

/// <summary>
/// Handler for retrieving a list of statuses.
/// </summary>
internal sealed class GetStatusListQueryHandler
    : IRequestHandler<GetStatusListQuery, ErrorOr<List<EnumResult>>>
{
    /// <summary>
    /// Handles the retrieval of a list of status values.
    /// </summary>
    /// <param name="query">The GetStatusListQuery to be handled.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of GetStatusListResult or an error result.</returns>
    public async Task<ErrorOr<List<EnumResult>>> Handle(
        GetStatusListQuery query,
        CancellationToken cancellationToken)
    {
        return EnumExtensions.ToEnumResults<Status>().ToList();
    }
}
