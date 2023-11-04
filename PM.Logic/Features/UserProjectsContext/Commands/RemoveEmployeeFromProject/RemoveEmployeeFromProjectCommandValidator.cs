using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Resources;

namespace PM.Application.Features.EmployeeProjectsContext.Commands.RemoveEmployeeFromProject;

/// <summary>
/// Validates the <see cref="RemoveEmployeeFromProjectCommand"/> 
/// to ensure that it meets the specified criteria.
/// </summary>
public sealed class RemoveEmployeeFromProjectCommandValidator
    : AbstractValidator<RemoveEmployeeFromProjectCommand>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="RemoveEmployeeFromProjectCommandValidator"/> class.
    /// </summary>
    /// <param name="projectRepository">The project repository used for project-related validation.</param>
    /// <param name="userRepository">The user repository used for user-related validation.</param>
    public RemoveEmployeeFromProjectCommandValidator(
        IProjectRepository projectRepository,
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _projectRepository = projectRepository;

        RuleFor(command => command.EmployeeId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MustAsync(EmployeeMustBeInDatabase)
            .WithMessage(ErrorsResource.NotFound);

        RuleFor(command => command.ProjectId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MustAsync(ProjectMustBeInDatabase)
            .WithMessage(ErrorsResource.NotFound);
    }

    private async Task<bool> ProjectMustBeInDatabase(
         RemoveEmployeeFromProjectCommand command,
         int id,
         CancellationToken cancellationToken)
    {
        command.Project = await _projectRepository
            .GetOrDeafaultAsync(e => e.Id == id, cancellationToken);

        return command.Project is not null;
    }

    private async Task<bool> EmployeeMustBeInDatabase(
        RemoveEmployeeFromProjectCommand command,
        int id,
        CancellationToken cancellationToken)
    {
        command.Employee = await _userRepository
            .GetOrDeafaultAsync(e => e.Id == id, cancellationToken);

        return command.Employee is not null;
    }
}
