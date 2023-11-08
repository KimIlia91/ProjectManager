using ErrorOr;
using MediatR;
using PM.Domain.Common.Enums;
using PM.Domain.Common.Extensions;

namespace PM.Application.Features.PriorityContext.Queries;

/// <summary>
/// Handles the execution of the <see cref="GetPrioritiesQuery"/> to retrieve a list of priority values.
/// </summary>
internal sealed class GetPrioritiesQueryHandler
    : IRequestHandler<GetPrioritiesQuery, ErrorOr<List<EnumResult>>>
{
    public async Task<ErrorOr<List<EnumResult>>> Handle(
        GetPrioritiesQuery query,
        CancellationToken cancellationToken)
    {
        return await Task.FromResult(EnumExtensions.ToEnumResults<Priority>().ToList());
    }
}