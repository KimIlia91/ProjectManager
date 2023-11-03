using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.EmployeeProjectsContext.Dtos;

namespace PM.Application.Features.EmployeeProjectsContext.Commands.RemoveEmployeeFromProject;

internal sealed class RemoveEmployeeFromProjectCommandHandler
    : IRequestHandler<RemoveEmployeeFromProjectCommand, ErrorOr<RemoveEmployeeFromProjectResult>>
{
    private readonly IProjectRepository _projectRepository;

    public RemoveEmployeeFromProjectCommandHandler(
        IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ErrorOr<RemoveEmployeeFromProjectResult>> Handle(
        RemoveEmployeeFromProjectCommand command, 
        CancellationToken cancellationToken)
    {
        command.Project!.RemoveEmployee(command.Employee!);

        await _projectRepository.SaveChangesAsync(cancellationToken);

        return new RemoveEmployeeFromProjectResult(command.EmployeeId, command.ProjectId);
    }
}
