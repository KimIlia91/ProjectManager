using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Resources;
using PM.Application.Common.Specifications.ProjectSpecifications;
using PM.Application.Features.TaskContext.Commands.CreateTask.UserSpec;

namespace PM.Application.Features.UserProjectsContext.Commands.AddUserToProject;

/// <summary>
/// Validator for the command to add an employee to a project.
/// </summary>
public sealed class AddUserToProjectCommandValidator
    : AbstractValidator<AddUserToProjectCommand>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddUserToProjectCommandValidator"/> class.
    /// </summary>
    /// <param name="projectRepository">The project repository used for database operations.</param>
    /// <param name="userRepository">The user repository used for database operations.</param>
    public AddUserToProjectCommandValidator(
        IProjectRepository projectRepository,
        IUserRepository userRepository,
        ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
        _userRepository = userRepository;
        _projectRepository = projectRepository;

        RuleFor(command => command.UserId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MustAsync(UserMustBeInDatebase)
            .WithMessage(ErrorsResource.NotFound)
            .MustAsync(UserMustNotBeInProject)
            .WithMessage(ErrorsResource.UserInProject)
            .When(command => command.ProjectId > 0);

        RuleFor(command => command.ProjectId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MustAsync(ManagerProjectMustBeInDatabase)
            .WithMessage(ErrorsResource.NotFound);
    }

    private async Task<bool> UserMustBeInDatebase(
        AddUserToProjectCommand command,
        int userId,
        CancellationToken cancellationToken)
    {
        command.Employee = await _userRepository
            .GetOrDeafaultAsync(u => u.Id == userId, cancellationToken);

        return command.Employee is not null;
    }

    private async Task<bool> ManagerProjectMustBeInDatabase(
        AddUserToProjectCommand command,
        int projectId,
        CancellationToken cancellationToken)
    {
        var managerProject = new ProjectOfManagerSpec(projectId, _currentUserService);

        command.Project = await _projectRepository
            .GetOrDeafaultAsync(managerProject.ToExpression(), cancellationToken);

        return command.Project is not null;
    }

    private async Task<bool> UserMustNotBeInProject(
        AddUserToProjectCommand command,
        int userId,
        CancellationToken cancellationToken)
    {
        var userProject = new UserProjectMembershipSpec(userId, command.ProjectId);

        var user = await _userRepository
            .GetOrDeafaultAsync(userProject.ToExpression(), cancellationToken);

        return user is null;
    }
}
