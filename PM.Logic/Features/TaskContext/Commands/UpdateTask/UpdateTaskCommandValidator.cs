using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Resources;
using PM.Application.Features.TaskContext.Commands.CreateTask.UserSpec;
using PM.Domain.Common.Constants;

namespace PM.Application.Features.TaskContext.Commands.UpdateTask;

/// <summary>
/// Validator for the update task command.
/// </summary>
public sealed class UpdateTaskCommandValidator
    : AbstractValidator<UpdateTaskCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateTaskCommandValidator"/> class.
    /// </summary>
    /// <param name="taskRepository">The task repository.</param>
    /// <param name="userRepository">The employee repository.</param>
    public UpdateTaskCommandValidator(
        ITaskRepository taskRepository,
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _taskRepository = taskRepository;

        RuleFor(command => command.Id)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MustAsync(TaskMustBeInDatabase)
            .WithMessage(ErrorsResource.NotFound);

        RuleFor(command => command.Name)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MaximumLength(EntityConstants.TaskName)
            .WithMessage(string.Format(ErrorsResource.MaxLength, EntityConstants.TaskName));

        RuleFor(command => command.AuthorId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MustAsync(AuthorMustBeInProject)
            .WithMessage(ErrorsResource.NotFound);

        RuleFor(command => command.ExecutorId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .MustAsync(ExecutorMustBeInProject)
            .When(command => command.ExecutorId != 0)
            .WithMessage(ErrorsResource.NotFound);

        RuleFor(command => command.Comment)
            .Cascade(CascadeMode.StopOnFirstFailure)
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

    private async Task<bool> ExecutorMustBeInProject(
        UpdateTaskCommand command,
        int userId,
        CancellationToken cancellationToken)
    {
        var getUserInProject = new GetUserOfRpojectSpec(userId, command.Task!.Project.Id);

        command.Executor = await _userRepository
           .GetOrDeafaultAsync(getUserInProject.ToExpression(), cancellationToken);

        return command.Executor is not null;
    }

    private async Task<bool> AuthorMustBeInProject(
        UpdateTaskCommand command,
        int userId,
        CancellationToken cancellationToken)
    {
        var getUserInProject = new GetUserOfRpojectSpec(userId, command.Task!.Project.Id);

        command.Author = await _userRepository
           .GetOrDeafaultAsync(getUserInProject.ToExpression(), cancellationToken);

        return command.Executor is not null;
    }

    private async Task<bool> TaskMustBeInDatabase(
        UpdateTaskCommand command,
        int taskId,
        CancellationToken cancellationToken)
    {
        command.Task = await _taskRepository
            .GetTaskIncludeProjectAsync(taskId, cancellationToken);

        return command.Task is not null;
    }
}
