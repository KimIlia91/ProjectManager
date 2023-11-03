using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Resources;

namespace PM.Application.Features.ProjectContext.Commands.DeleteProject;

/// <summary>
/// Validator for the command to delete a project.
/// </summary>
public sealed class DeleteProjectCommandValidator
    : AbstractValidator<DeleteProjectCommand>
{
    private readonly IProjectRepository _projectRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProjectCommandValidator"/> class.
    /// </summary>
    /// <param name="projectRepository">The project repository used for data access.</param>
    public DeleteProjectCommandValidator(
        IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;

        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MustAsync(MustBeIndatabase)
            .WithMessage(ErrorsResource.NotFound);
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
