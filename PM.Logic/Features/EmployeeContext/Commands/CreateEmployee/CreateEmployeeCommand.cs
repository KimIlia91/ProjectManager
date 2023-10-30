using ErrorOr;
using MediatR;

namespace PM.Application.Features.EmployeeContext.Commands.CreateEmployee;

public record CreateEmployeeCommand(
    string FirstName,
    string LastName,
    string? MiddelName,
    string Email) : IRequest<ErrorOr<CreateEmployeeResult>>;

public class CreateEmployeeResult
{
}
