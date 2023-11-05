using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Resources;
using PM.Application.Common.Specifications.ProjectSpecifications;
using PM.Application.Features.TaskContext.Commands.CreateTask.UserSpec;
using PM.Domain.Entities;

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
    private readonly ICurrentUserService _currentUserService;

    /// <summary>
    /// Initializes a new instance of the <see cref="RemoveEmployeeFromProjectCommandValidator"/> class.
    /// </summary>
    /// <param name="projectRepository">The project repository used for project-related validation.</param>
    /// <param name="userRepository">The user repository used for user-related validation.</param>
    public RemoveEmployeeFromProjectCommandValidator(
        IProjectRepository projectRepository,
        IUserRepository userRepository,
        ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _projectRepository = projectRepository;
        _currentUserService = currentUserService;

        RuleFor(command => command.EmployeeId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MustAsync(UserMustBeInProject)
            .WithMessage(ErrorsResource.NotFound);

        RuleFor(command => command.ProjectId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MustAsync(ManagerProjectMustBeInDatabase)
            .WithMessage(ErrorsResource.NotFound);
    }

    private async Task<bool> ManagerProjectMustBeInDatabase(
         RemoveEmployeeFromProjectCommand command,
         int id,
         CancellationToken cancellationToken)
    {
        var managerProject = new GetProjectOfManagerSpec(id, _currentUserService.UserId);

        command.Project = await _projectRepository
            .GetOrDeafaultAsync(managerProject.ToExpression(), cancellationToken);

        return command.Project is not null;
    }

    private async Task<bool> UserMustBeInProject(
        RemoveEmployeeFromProjectCommand command,
        int userId,
        CancellationToken cancellationToken)
    {
        var userProject = new GetUserOfRpojectSpec(userId, command.ProjectId);

        command.Employee = await _userRepository
            .GetOrDeafaultAsync(userProject.ToExpression(), cancellationToken);

        return command.Employee is not null;
    }
}
