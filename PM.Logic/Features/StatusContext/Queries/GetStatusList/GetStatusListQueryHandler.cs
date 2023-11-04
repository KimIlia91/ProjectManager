using ErrorOr;
using MapsterMapper;
using MediatR;
using PM.Application.Features.StatusContext.Dtos;
using PM.Domain.Common.Enums;
using PM.Domain.Common.Extensions;

namespace PM.Application.Features.StatusContext.Queries.GetStatusList;

internal sealed class GetStatusListQueryHandler
    : IRequestHandler<GetStatusListQuery, ErrorOr<List<GetStatusListResult>>>
{
    private readonly IMapper _mapper;

    public GetStatusListQueryHandler(
        IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<ErrorOr<List<GetStatusListResult>>> Handle(
        GetStatusListQuery query, 
        CancellationToken cancellationToken)
    {
        var statusList = EnumExtensions.GetAllAsEnumerable<Status>();

        return await Task.FromResult(_mapper.Map<List<GetStatusListResult>>(statusList));
    }
}
