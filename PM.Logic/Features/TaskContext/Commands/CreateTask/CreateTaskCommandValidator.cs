using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Resources;
using PM.Application.Common.Specifications.ProjectSpecifications;
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
    /// <param name="employeeRepository">The user repository.</param>
    /// <param name="projectRepository">The project repository.</param>
    public CreateTaskCommandValidator(
        IUserRepository employeeRepository, 
        IProjectRepository projectRepository,
        ICurrentUserService currentUserService)
    {
        _userRepository = employeeRepository;
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
            .MaximumLength(EntityConstants.TaskName);

        RuleFor(command => command.ExecutorId)
            .MustAsync(UserMustBeInProject)
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

    private async Task<bool> ManagerProjectMustBeInDatabase(
        CreateTaskCommand command,
        int id,
        CancellationToken cancellationToken)
    {
        var getManagerProject = new GetManagerProjectSpec(id, 
            _currentUserService.UserId);

        command.Project = await _projectRepository
            .GetOrDeafaultAsync(getManagerProject.ToExpression(), cancellationToken);

        return command.Project is not null;
    }

    private async Task<bool> UserMustBeInProject(
        CreateTaskCommand command,
        int id,
        CancellationToken cancellationToken)
    {
        command.Executor = await _userRepository
           .GetOrDeafaultAsync(u => u.Id == id &&
               (u.Projects.Any(p => p.Id == command.ProjectId) || 
               u.Projects.Any(p => p.Manager.Id == id)), cancellationToken);

        return command.Executor is not null;
    }
}
