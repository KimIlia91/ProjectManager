using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;

namespace PM.Application.Features.ProjectContext.Commands.DeleteProject;

public sealed class DeleteProjectCommandValidator
    : AbstractValidator<DeleteProjectCommand>
{
    private readonly IProjectRepository _projectRepository;

    public DeleteProjectCommandValidator(
        IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;

        RuleFor(command => command.Id)
            .NotEmpty()
            .MustAsync(MustBeIndatabase);
    }

    private async Task<bool> MustBeIndatabase(
        DeleteProjectCommand command,
        int id, 
        CancellationToken cancellationToken)
    {
        command.Project = await _projectRepository
            .GetOrDeafaultAsync(p => p.Id == id, cancellationToken);

        return command.Project is not null;
    }
}
