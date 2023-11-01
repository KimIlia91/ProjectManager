using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Domain.Common.Constants;

namespace PM.Application.Features.TaskContext.Commands.CreateTask;

public sealed class CreateTaskCommandValidator
    : AbstractValidator<CreateTaskCommand>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IProjectRepository _projectRepository;

    public CreateTaskCommandValidator(
        IEmployeeRepository employeeRepository, IProjectRepository projectRepository)
    {
        _employeeRepository = employeeRepository;
        _projectRepository = projectRepository;

        RuleFor(command => command.ProjectId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .MustAsync(ProjectMustBeInDatabase);

        RuleFor(command => command.Name)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .MaximumLength(EntityConstants.TaskName);

        RuleFor(command => command.AuthorId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .When(command => command.AuthorId > 0)
            .MustAsync(AuthorMustBeInDatabase);

        RuleFor(command => command.ExecutorId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .MustAsync(ExecutorMustBeInDatabase)
            .When(command => command.ExecutorId > 0);

        RuleFor(command => command.Commnet)
            .MaximumLength(EntityConstants.Comment)
            .When(command => !string.IsNullOrEmpty(command.Commnet));

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

    private async Task<bool> ProjectMustBeInDatabase(
        CreateTaskCommand command,
        int id,
        CancellationToken cancellationToken)
    {
        command.Project = await _projectRepository
          .GetOrDeafaultAsync(e => e.Id == id, cancellationToken);

        return command.Project is not null;
    }

    private async Task<bool> ExecutorMustBeInDatabase(
        CreateTaskCommand command,
        int id,
        CancellationToken cancellationToken)
    {
        command.Executor = await _employeeRepository
           .GetOrDeafaultAsync(e => e.Id == id, cancellationToken);

        return command.Executor is not null;
    }

    private async Task<bool> AuthorMustBeInDatabase(
        CreateTaskCommand command,
        int id, 
        CancellationToken cancellationToken)
    {
        command.Author = await _employeeRepository
            .GetOrDeafaultAsync(e => e.Id == id, cancellationToken);

        return command.Author is not null;
    }
}
