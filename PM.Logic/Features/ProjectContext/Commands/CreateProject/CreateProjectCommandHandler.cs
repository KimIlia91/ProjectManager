using ErrorOr;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.ProjectContext.Dtos;
using PM.Domain.Entities;

namespace PM.Application.Features.ProjectContext.Commands.CreateProject;

public sealed class CreateProjectCommandHandler
    : IRequestHandler<CreateProjectCommand, ErrorOr<CreateProjectResult>>
{
    private readonly IProjectRepository _projectRepository;

    public CreateProjectCommandHandler(
        IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ErrorOr<CreateProjectResult>> Handle(
        CreateProjectCommand command, 
        CancellationToken cancellationToken)
    {
        var result = Project.Create(
            command.Name, 
            command.CustomerCompany, 
            command.ExecutorCompany, 
            command.Manager, 
            command.StartDate, 
            command.EndDate,
            command.Priority);

        if (result.IsError)
            return result.Errors;

        await _projectRepository.SaveChangesAsync(cancellationToken);

        return new CreateProjectResult(result.Value.Id);
    }
}