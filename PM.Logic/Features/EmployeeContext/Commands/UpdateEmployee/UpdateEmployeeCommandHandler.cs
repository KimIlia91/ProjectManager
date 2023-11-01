using ErrorOr;
using MapsterMapper;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Features.EmployeeContext.Dtos;

namespace PM.Application.Features.EmployeeContext.Commands.UpdateEmployee;

public sealed class UpdateEmployeeCommandHandler
    : IRequestHandler<UpdateEmployeeCommand, ErrorOr<UpdateEmployeeResult>>
{
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public UpdateEmployeeCommandHandler(
        IMapper mapper,
        IIdentityService identityService)
    {
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task<ErrorOr<UpdateEmployeeResult>> Handle(
        UpdateEmployeeCommand command,
        CancellationToken cancellationToken)
    {
        command.Employee!.Update(
            command.FirstName,
            command.LastName,
            command.MiddelName,
            command.Email);

        var result = await _identityService
            .UpdateAsync(command.Employee, cancellationToken);

        if (result.IsError)
            return result.Errors;

        return _mapper.Map<UpdateEmployeeResult>(result.Value);
    }
}
