using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Domain.Common.Constants;

namespace PM.Application.Features.TaskContext.Commands.CreateTask;

internal sealed class CreateTaskCommandValidator
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
            .MustAsync(AuthorMustBeInDatabase);

        RuleFor(command => command.ExecutorId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .MustAsync(ExecutorMustBeInDatabase);

        RuleFor(command => command.Commnet)
            .MaximumLength(EntityConstants.Comment)
            .When(command => command.Commnet is not null);

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
