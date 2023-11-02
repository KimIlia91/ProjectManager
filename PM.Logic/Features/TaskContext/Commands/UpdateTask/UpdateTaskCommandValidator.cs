using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Domain.Common.Constants;

namespace PM.Application.Features.TaskContext.Commands.UpdateTask;

public sealed class UpdateTaskCommandValidator 
    : AbstractValidator<UpdateTaskCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public UpdateTaskCommandValidator(
        ITaskRepository taskRepository,
        IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
        _taskRepository = taskRepository;

        RuleFor(command => command.Id)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .MustAsync(TaskMustBeInDatabase);

        RuleFor(command => command.Name)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .MaximumLength(EntityConstants.TaskName);

        RuleFor(command => command.AuthorId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .MustAsync(AuthorMustBeInDatabase);

        RuleFor(command => command.ExecutorId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .MustAsync(ExecutorMustBeInDatabase)
            .When(command => command.ExecutorId != 0);

        RuleFor(command => command.Comment)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .MaximumLength(EntityConstants.Comment);

        RuleFor(command => command.Status)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .IsInEnum()
            .WithMessage("Invalid task status.");

        RuleFor(command => command.Priority)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .IsInEnum()
            .WithMessage("Invalid project priority.");
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
