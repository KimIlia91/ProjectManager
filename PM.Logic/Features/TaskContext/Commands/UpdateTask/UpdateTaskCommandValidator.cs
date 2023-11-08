using FluentValidation;
using PM.Domain.Common.Constants;
using PM.Application.Common.Resources;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Specifications.TaskSpecifications.Manager;
using PM.Application.Features.TaskContext.Commands.CreateTask.UserSpec;

namespace PM.Application.Features.TaskContext.Commands.UpdateTask;

/// <summary>
/// Validator for the update task command.
/// </summary>
public sealed class UpdateTaskCommandValidator
    : AbstractValidator<UpdateTaskCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUser;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateTaskCommandValidator"/> class.
    /// </summary>
    /// <param name="taskRepository">The task repository.</param>
    /// <param name="userRepository">The employee repository.</param>
    public UpdateTaskCommandValidator(
        ITaskRepository taskRepository,
        IUserRepository userRepository,
        ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _taskRepository = taskRepository;
        _currentUser = currentUserService;

        RuleFor(command => command.Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MustAsync(TaskMustBeInManagerProject)
            .WithMessage(ErrorsResource.NotFound);

        RuleFor(command => command.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MaximumLength(EntityConstants.TaskName)
            .WithMessage(string.Format(ErrorsResource.MaxLength, EntityConstants.TaskName));

        RuleFor(command => command.AuthorId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MustAsync(AuthorMustBeInProject)
            .When(command => command.Task is not null)
            .WithMessage(ErrorsResource.NotFound);

        RuleFor(command => command.ExecutorId)
            .Cascade(CascadeMode.Stop)
            .MustAsync(ExecutorMustBeInProject)
            .When(command => command.ExecutorId != 0 && command.Task is not null)
            .WithMessage(ErrorsResource.NotFound);

        RuleFor(command => command.Comment)
            .Cascade(CascadeMode.Stop)
            .MaximumLength(EntityConstants.Comment)
            .When(command => !string.IsNullOrEmpty(command.Comment))
            .WithMessage(string.Format(ErrorsResource.MaxLength, EntityConstants.Comment));

        RuleFor(command => command.Status)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .IsInEnum()
            .WithMessage(ErrorsResource.InvalidTaskStatus);

        RuleFor(command => command.Priority)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .IsInEnum()
            .WithMessage(ErrorsResource.InvalidPriority);
    }

    private async Task<bool> ExecutorMustBeInProject(
        UpdateTaskCommand command,
        int userId,
        CancellationToken cancellationToken)
    {
        var getUserInProject = new UserProjectMembershipSpec(
            userId,
            command.Task!.ProjectId);

        command.Executor = await _userRepository
           .GetOrDeafaultAsync(getUserInProject.ToExpression(), cancellationToken);

        return command.Executor is not null;
    }

    private async Task<bool> AuthorMustBeInProject(
        UpdateTaskCommand command,
        int userId,
        CancellationToken cancellationToken)
    {
        var getUserInProject = new UserProjectMembershipSpec(
            userId,
            command.Task!.ProjectId);

        command.Author = await _userRepository
           .GetOrDeafaultAsync(getUserInProject.ToExpression(), cancellationToken);

        return command.Author is not null;
    }

    private async Task<bool> TaskMustBeInManagerProject(
        UpdateTaskCommand command,
        int taskId,
        CancellationToken cancellationToken)
    {
        var getTaskOfManager = new TaskOfManagerSpec(
            taskId,
            _currentUser);

        command.Task = await _taskRepository
             .GetOrDeafaultAsync(getTaskOfManager.ToExpression(), cancellationToken);

        return command.Task is not null;
    }
}
