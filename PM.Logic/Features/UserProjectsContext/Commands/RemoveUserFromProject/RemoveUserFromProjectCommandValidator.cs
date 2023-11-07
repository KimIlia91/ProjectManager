using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Resources;
using PM.Application.Common.Specifications.ProjectSpecifications;
using PM.Application.Features.TaskContext.Commands.CreateTask.UserSpec;
using PM.Domain.Entities;

namespace PM.Application.Features.UserProjectsContext.Commands.RemoveUserFromProject;

/// <summary>
/// Validates the <see cref="RemoveUserFromProjectCommand"/> 
/// to ensure that it meets the specified criteria.
/// </summary>
public sealed class RemoveUserFromProjectCommandValidator
    : AbstractValidator<RemoveUserFromProjectCommand>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    /// <summary>
    /// Initializes a new instance of the <see cref="RemoveUserFromProjectCommandValidator"/> class.
    /// </summary>
    /// <param name="projectRepository">The project repository used for project-related validation.</param>
    /// <param name="userRepository">The user repository used for user-related validation.</param>
    public RemoveUserFromProjectCommandValidator(
        IProjectRepository projectRepository,
        IUserRepository userRepository,
        ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _projectRepository = projectRepository;
        _currentUserService = currentUserService;

        RuleFor(command => command.ProjectId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MustAsync(ManagerProjectMustBeInDatabase)
            .WithMessage(ErrorsResource.NotFound);

        RuleFor(command => command.UserId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MustAsync(UserMustBeInProject)
            .WithMessage(ErrorsResource.NotFound)
            .When(command => command.Project != null);
    }

    private async Task<bool> ManagerProjectMustBeInDatabase(
         RemoveUserFromProjectCommand command,
         int id,
         CancellationToken cancellationToken)
    {
        var managerProject = new GetProjectOfManagerSpec(id, _currentUserService);

        command.Project = await _projectRepository
            .GetOrDeafaultAsync(managerProject.ToExpression(), cancellationToken);

        return command.Project is not null;
    }

    private async Task<bool> UserMustBeInProject(
        RemoveUserFromProjectCommand command,
        int userId,
        CancellationToken cancellationToken)
    {
        var userProject = new GetUserOfRpojectSpec(userId, command.ProjectId);

        command.User = await _userRepository
            .GetOrDeafaultAsync(userProject.ToExpression(), cancellationToken);

        return command.User is not null;
    }
}
