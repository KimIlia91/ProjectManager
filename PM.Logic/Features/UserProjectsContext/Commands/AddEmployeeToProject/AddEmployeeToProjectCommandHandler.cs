using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.EmployeeProjectsContext.Dtos;

namespace PM.Application.Features.EmployeeProjectsContext.Commands.AddEmployeeToProject;

internal sealed class AddEmployeeToProjectCommandHandler
    : IRequestHandler<AddEmployeeToProjectCommand, ErrorOr<AddEmployeeToProjectResult>>
{
    private readonly IProjectRepository _projectRepository;

    public AddEmployeeToProjectCommandHandler(
        IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ErrorOr<AddEmployeeToProjectResult>> Handle(
        AddEmployeeToProjectCommand command,
        CancellationToken cancellationToken)
    {
        command.Project!.AddEmployee(command.Employee!);
        await _projectRepository.SaveChangesAsync(cancellationToken);

        return new AddEmployeeToProjectResult(command.EmployeeId);
    }
}
