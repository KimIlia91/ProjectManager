using ErrorOr;
using MapsterMapper;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.EmployeeContext.Dtos;

namespace PM.Application.Features.EmployeeContext.Commands.UpdateEmployee;

public sealed class UpdateEmployeeCommandHandler
    : IRequestHandler<UpdateEmployeeCommand, ErrorOr<UpdateEmployeeResult>>
{
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _employeeRepository;

    public UpdateEmployeeCommandHandler(
        IMapper mapper,
        IEmployeeRepository employeeRepository)
    {
        _mapper = mapper;
        _employeeRepository = employeeRepository;
    }

    public async Task<ErrorOr<UpdateEmployeeResult>> Handle(
        UpdateEmployeeCommand command, 
        CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository
            .GetOrDeafaultAsync(e => e.Id == command.Id, cancellationToken);

        if (employee is null)
            return Error.NotFound("Not found", nameof(command.Id));

        employee.Update(command.FirstName, command.LastName, command.Email, command.MiddelName);

        await _employeeRepository.SaveChangesAsync(cancellationToken);
        return _mapper.Map<UpdateEmployeeResult>(employee);
    }
}
