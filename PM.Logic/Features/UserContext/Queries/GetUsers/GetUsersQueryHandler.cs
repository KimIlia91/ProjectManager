using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.EmployeeContext.Dtos;

namespace PM.Application.Features.EmployeeContext.Queries.GetEmployees;

public sealed class GetUsersQueryHandler
    : IRequestHandler<GetUsersQuery, ErrorOr<List<GetUserResult>>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<List<GetUserResult>>> Handle(
        GetUsersQuery query, 
        CancellationToken cancellationToken)
    {
        return await _userRepository.GetUsersAsync(cancellationToken);
    }
}
