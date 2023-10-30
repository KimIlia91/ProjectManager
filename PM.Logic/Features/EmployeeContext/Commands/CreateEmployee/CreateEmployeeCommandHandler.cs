using ErrorOr;
using MapsterMapper;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.EmployeeContext.Dtos;
using PM.Domain.Entities;

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
            return Error.Conflict("Employee has already exist", nameof(employee));

        var result = Employee.Create(
            command.FirstName, command.LastName, command.Email, command.MiddelName);

        if (result.IsError)
            return result.FirstError;

        await _employeeRepository.AddAsync(result.Value, cancellationToken);
        return new CreateEmployeeResult(result.Value.Id);
    }
}
