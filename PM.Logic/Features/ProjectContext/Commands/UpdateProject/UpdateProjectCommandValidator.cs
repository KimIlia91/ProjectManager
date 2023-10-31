using FluentValidation;
using PM.Domain.Common.Constants;
using PM.Application.Common.Interfaces.IRepositories;

namespace PM.Application.Features.ProjectContext.Commands.UpdateProject;

internal sealed class UpdateProjectCommandValidator
    : AbstractValidator<UpdateProjectCommand>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public UpdateProjectCommandValidator(
        IProjectRepository projectRepository, 
        IEmployeeRepository employeeRepository)
    {
        _projectRepository = projectRepository;
        _employeeRepository = employeeRepository;

        RuleFor(command => command.Id)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .MustAsync(ProjectMustBeInDatabase);

        RuleFor(command => command.Name)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .MaximumLength(EntityConstants.ProjectName)
            .MustAsync(ProjectNameMustBeUnique);

        RuleFor(command => command.CustomerCompany)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .MaximumLength(EntityConstants.CompanyName);

        RuleFor(command => command.ExecutorCompany)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .MaximumLength(EntityConstants.CompanyName);

        RuleFor(command => command.ManagerId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .MustAsync(ManagerMustBeInDatabase);

        RuleFor(command => command.StartDate)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .Must((command, startDate) => startDate <= command.EndDate)
            .WithMessage("Дата начала должна быть меньше или равна дате окончания");

        RuleFor(command => command.EndDate)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .Must((command, endDate) => endDate >= command.StartDate)
            .WithMessage("Дата окончания должна быть больше или равна дате начала");

        RuleFor(command => command.Priority)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .IsInEnum()
            .WithMessage("Выбран недопустимый приоритет");
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
