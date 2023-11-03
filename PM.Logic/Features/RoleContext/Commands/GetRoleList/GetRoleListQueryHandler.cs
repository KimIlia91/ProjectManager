using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.RoleContext.Dtos;

namespace PM.Application.Features.RoleContext.Commands.GetRoleList;

/// <summary>
/// Handles the retrieval of a list of roles.
/// </summary>
internal sealed class GetRoleListQueryHandler
    : IRequestHandler<GetRoleListQuery, ErrorOr<List<GetRoleListResult>>>
{
    private readonly IRoleRepository _roleRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetRoleListQueryHandler"/> class.
    /// </summary>
    /// <param name="roleRepository">The role repository used for data access.</param>
    public GetRoleListQueryHandler(
        IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    /// <summary>
    /// Handles the query to retrieve a list of roles.
    /// </summary>
    /// <param name="query">The query to retrieve roles.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of role results or an error.</returns>
    public async Task<ErrorOr<List<GetRoleListResult>>> Handle(
        GetRoleListQuery query, 
        CancellationToken cancellationToken)
    {
        return await _roleRepository.GetRoleListResultAsync(cancellationToken);
    }
}
