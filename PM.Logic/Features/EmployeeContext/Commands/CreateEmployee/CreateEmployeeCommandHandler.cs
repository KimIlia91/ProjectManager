using ErrorOr;
using MapsterMapper;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.EmployeeContext.Dtos;

namespace PM.Application.Features.EmployeeContext.Commands.CreateEmployee;

public sealed class CreateEmployeeCommandHandler
    : IRequestHandler<CreateEmployeeCommand, ErrorOr<CreateEmployeeResult>>
{
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _employeeRepository;

    public CreateEmployeeCommandHandler(
        IMapper mapper,
        IEmployeeRepository employeeRepository)
    {
        _mapper = mapper;
        _employeeRepository = employeeRepository;
    }

    public async Task<ErrorOr<CreateEmployeeResult>> Handle(
        CreateEmployeeCommand command,
        CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository
            .GetOrDeafaultAsync(e => e.Email == command.Email, cancellationToken);

        if (employee is not null)
            return Error.Conflict(nameof(employee), "Employee has already exist");

        _mapper.Map(command, employee);

        await _employeeRepository.AddAsync(employee, cancellationToken);
        await _employeeRepository.SaveChangesAsync(cancellationToken);

        return new CreateEmployeeResult(employee.Id);
    }
}
