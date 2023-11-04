using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Resources;
using PM.Domain.Common.Constants;

namespace PM.Application.Features.TaskContext.Commands.UpdateTask;

/// <summary>
/// Validator for the update task command.
/// </summary>
public sealed class UpdateTaskCommandValidator 
    : AbstractValidator<UpdateTaskCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _employeeRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateTaskCommandValidator"/> class.
    /// </summary>
    /// <param name="taskRepository">The task repository.</param>
    /// <param name="employeeRepository">The employee repository.</param>
    public UpdateTaskCommandValidator(
        ITaskRepository taskRepository,
        IUserRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
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
            .MustAsync(AuthorMustBeInDatabase)
            .WithMessage(ErrorsResource.NotFound);

        RuleFor(command => command.ExecutorId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .MustAsync(ExecutorMustBeInDatabase)
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
            .MaximumLength(EntityConstants.EnumStatusLength)
            .WithMessage(string.Format(ErrorsResource.MaxLength, EntityConstants.EnumStatusLength));

        RuleFor(command => command.Priority)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .IsInEnum()
            .WithMessage(ErrorsResource.InvalidPriority);
    }

    private async Task<bool> ExecutorMustBeInDatabase(
        UpdateTaskCommand command,
        int id,
        CancellationToken cancellationToken)
    {
        command.Executor = await _employeeRepository
           .GetOrDeafaultAsync(e => e.Id == id, cancellationToken);

        return command.Executor is not null;
    }

    private async Task<bool> AuthorMustBeInDatabase(
        UpdateTaskCommand command,
        int id,
        CancellationToken cancellationToken)
    {
        command.Author = await _employeeRepository
            .GetOrDeafaultAsync(e => e.Id == id, cancellationToken);

        return command.Author is not null;
    }

    private async Task<bool> TaskMustBeInDatabase(
        UpdateTaskCommand command,
        int id, 
        CancellationToken cancellationToken)
    {
        command.Task = await _taskRepository
            .GetOrDeafaultAsync(t => t.Id == id, cancellationToken);

        return command.Task is not null;
    }
}
