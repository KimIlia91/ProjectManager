using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Resources;
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

    /// <summary>
    /// Initializes a new instance of the CreateTaskCommandValidator class.
    /// </summary>
    /// <param name="employeeRepository">The user repository.</param>
    /// <param name="projectRepository">The project repository.</param>
    public CreateTaskCommandValidator(
        IUserRepository employeeRepository, 
        IProjectRepository projectRepository)
    {
        _userRepository = employeeRepository;
        _projectRepository = projectRepository;

        RuleFor(command => command.ProjectId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MustAsync(ProjectMustBeInDatabase)
            .WithMessage(ErrorsResource.NotFound);

        RuleFor(command => command.Name)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .MaximumLength(EntityConstants.TaskName);

        RuleFor(command => command.ExecutorId)
            .MustAsync(ExecutorMustBeInDatabase)
            .When(command => command.ExecutorId > 0)
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
        command.Executor = await _userRepository
           .GetOrDeafaultAsync(e => e.Id == id, cancellationToken);

        return command.Executor is not null;
    }
}
