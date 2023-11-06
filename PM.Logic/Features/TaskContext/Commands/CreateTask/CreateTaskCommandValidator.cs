using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Resources;
using PM.Application.Common.Specifications.ProjectSpecifications;
using PM.Application.Features.TaskContext.Commands.CreateTask.UserSpec;
using PM.Domain.Common.Constants;

namespace PM.Application.Features.TaskContext.Commands.CreateTask;

/// <summary>
/// Validates the CreateTaskCommand to ensure it meets specified criteria.
/// </summary>
public sealed class CreateTaskCommandValidator
    : AbstractValidator<CreateTaskCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly ICurrentUserService _currentUserService;

    /// <summary>
    /// Initializes a new instance of the CreateTaskCommandValidator class.
    /// </summary>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="projectRepository">The project repository.</param>
    /// <param name="currentUserService">The current user service.</param>
    public CreateTaskCommandValidator(
        IUserRepository userRepository,
        IProjectRepository projectRepository,
        ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _projectRepository = projectRepository;
        _currentUserService = currentUserService;

        RuleFor(command => command.ProjectId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MustAsync(ManagerProjectMustBeInDatabase)
            .WithMessage(ErrorsResource.NotFound);

        RuleFor(command => command.Name)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MaximumLength(EntityConstants.TaskName);

        RuleFor(command => command.ExecutorId)
            .MustAsync(UserMustBeInProject)
            .When(command => command.ExecutorId > 0 && command.ProjectId > 0)
            .WithMessage(ErrorsResource.NotFound);

        RuleFor(command => command.Comment)
            .MaximumLength(EntityConstants.Comment)
            .When(command => !string.IsNullOrEmpty(command.Comment))
            .WithMessage(string.Format(ErrorsResource.MaxLength, EntityConstants.Comment));

        RuleFor(command => command.Status)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .IsInEnum()
            .WithMessage(ErrorsResource.InvalidTaskStatus);

        RuleFor(command => command.Priority)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .IsInEnum()
            .WithMessage(ErrorsResource.InvalidPriority);
    }

    private async Task<bool> ManagerProjectMustBeInDatabase(
        CreateTaskCommand command,
        int projectId,
        CancellationToken cancellationToken)
    {
        var getManagerProject = new GetProjectOfManagerSpec(
            projectId,
            _currentUserService);

        command.Project = await _projectRepository
            .GetOrDeafaultAsync(getManagerProject.ToExpression(), cancellationToken);

        return command.Project is not null;
    }

    private async Task<bool> UserMustBeInProject(
        CreateTaskCommand command,
        int userId,
        CancellationToken cancellationToken)
    {
        var getUserInProject = new GetUserOfRpojectSpec(userId, command.ProjectId);

        command.Executor = await _userRepository
           .GetOrDeafaultAsync(getUserInProject.ToExpression(), cancellationToken);

        return command.Executor is not null;
    }
}
