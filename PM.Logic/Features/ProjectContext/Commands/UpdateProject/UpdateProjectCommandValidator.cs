using FluentValidation;
using PM.Domain.Common.Constants;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Resources;

namespace PM.Application.Features.ProjectContext.Commands.UpdateProject;

/// <summary>
/// Validator for validating the properties of an update project command.
/// </summary>
public sealed class UpdateProjectCommandValidator
    : AbstractValidator<UpdateProjectCommand>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _employeeRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProjectCommandValidator"/> class.
    /// </summary>
    /// <param name="projectRepository">The project repository to check for project existence.</param>
    /// <param name="employeeRepository">The user repository to check for manager existence.</param>
    public UpdateProjectCommandValidator(
        IProjectRepository projectRepository, 
        IUserRepository employeeRepository)
    {
        _projectRepository = projectRepository;
        _employeeRepository = employeeRepository;

        RuleFor(command => command.Id)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MustAsync(ProjectMustBeInDatabase)
            .WithMessage(ErrorsResource.NotFound);

        RuleFor(command => command.Name)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MaximumLength(EntityConstants.ProjectName)
            .WithMessage(string.Format(ErrorsResource.MaxLength, EntityConstants.ProjectName))
            .MustAsync(ProjectNameMustBeUnique)
            .WithMessage(ErrorsResource.NotFound);

        RuleFor(command => command.CustomerCompany)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MaximumLength(EntityConstants.CompanyName)
            .WithMessage(string.Format(ErrorsResource.MaxLength, EntityConstants.CompanyName));

        RuleFor(command => command.ExecutorCompany)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MaximumLength(EntityConstants.CompanyName)
            .WithMessage(string.Format(ErrorsResource.MaxLength, EntityConstants.CompanyName));

        RuleFor(command => command.ManagerId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MustAsync(ManagerMustBeInDatabase)
            .WithMessage(ErrorsResource.NotFound);

        RuleFor(command => command.StartDate)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .Must((command, startDate) => startDate <= command.EndDate)
            .WithMessage(ErrorsResource.InvalidDate);

        RuleFor(command => command.EndDate)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .Must((command, endDate) => endDate >= command.StartDate)
            .WithMessage("Дата окончания должна быть больше или равна дате начала")
             .WithMessage(ErrorsResource.InvalidDate);

        RuleFor(command => command.Priority)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .IsInEnum()
            .WithMessage(ErrorsResource.InvalidPriority);
    }

    private async Task<bool> ManagerMustBeInDatabase(
        UpdateProjectCommand command,
        int managerId, 
        CancellationToken cancellationToken)
    {
        command.Manager = await _employeeRepository
            .GetOrDeafaultAsync(e => e.Id == managerId, cancellationToken);

        return command.Manager is not null;
    }

    private async Task<bool> ProjectNameMustBeUnique(
        UpdateProjectCommand command,
        string name, 
        CancellationToken cancellationToken)
    {
        var project = await _projectRepository
            .GetOrDeafaultAsync(c => c.Name == name && c.Id != command.Id, cancellationToken);

        return project is null;
    }

    private async Task<bool> ProjectMustBeInDatabase(
        UpdateProjectCommand command,
        int id, 
        CancellationToken cancellationToken)
    {
        command.Project = await _projectRepository
            .GetOrDeafaultAsync(c => c.Id == id, cancellationToken);

        return command.Project is not null;
    }
}
