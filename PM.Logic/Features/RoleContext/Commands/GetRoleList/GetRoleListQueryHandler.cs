using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.RoleContext.Dtos;

namespace PM.Application.Features.RoleContext.Commands.GetRoleList;

internal sealed class GetRoleListQueryHandler
    : IRequestHandler<GetRoleListQuery, ErrorOr<List<GetRoleListResult>>>
{
    private readonly IRoleRepository _roleRepository;

    public GetRoleListQueryHandler(
        IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<ErrorOr<List<GetRoleListResult>>> Handle(
        GetRoleListQuery query, 
        CancellationToken cancellationToken)
    {
        return await _roleRepository.GetRoleListResultAsync(cancellationToken);
    }
}
