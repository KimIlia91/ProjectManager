using ErrorOr;
using MapsterMapper;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.EmployeeContext.Dtos;

namespace PM.Application.Features.EmployeeContext.Queries.GetEmployee;

public sealed class GetUserQueryHandler
    : IRequestHandler<GetUserQuery, ErrorOr<GetUserResult>>
{
    private readonly IUserRepository _employeeRepository;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(
        IUserRepository employeeRepository,
        IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<GetUserResult>> Handle(
        GetUserQuery query, 
        CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository
            .GetEmployeeByIdAsync(query.EmployeeId, cancellationToken);

        if (employee is null)
            return Error.NotFound("Not found", nameof(query.EmployeeId));

        return employee;
    }
}
