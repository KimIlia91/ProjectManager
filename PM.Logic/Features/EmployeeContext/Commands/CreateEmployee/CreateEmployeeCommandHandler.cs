using ErrorOr;
using MediatR;

namespace PM.Application.Features.EmployeeContext.Commands.CreateEmployee;

public sealed class CreateEmployeeCommandHandler
    : IRequestHandler<CreateEmployeeCommand, ErrorOr<CreateEmployeeResult>>
{
    public Task<ErrorOr<CreateEmployeeResult>> Handle(
        CreateEmployeeCommand command,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
