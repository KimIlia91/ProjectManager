using FluentValidation;
using PM.Domain.Common.Constants;
using PM.Application.Common.Interfaces.IRepositories;

namespace PM.Application.Features.ProjectContext.Commands.UpdateProject;

internal sealed class UpdateProjectCommandValidator
    : AbstractValidator<UpdateProjectCommand>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public UpdateProjectCommandValidator(
        ICompanyRepository companyRepository,
        IProjectRepository projectRepository, 
        IEmployeeRepository employeeRepository)
    {
        _companyRepository = companyRepository;
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

        RuleFor(command => command.CustomerCompanyId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .MustAsync(CustomerCompanyMustBeInDatabase);

        RuleFor(command => command.ExecutorCompanyId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .MustAsync(ExecutorCompanyMustBeInDatabase);

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
        var manager = await _employeeRepository
            .GetOrDeafaultAsync(e => e.Id == managerId, cancellationToken);

        return manager is not null;
    }

    private async Task<bool> ExecutorCompanyMustBeInDatabase(
        UpdateProjectCommand command,
        int executorCompanyId, 
        CancellationToken cancellationToken)
    {
        command.ExecutorCompany = await _companyRepository
            .GetOrDeafaultAsync(c => c.Id == command.ExecutorCompanyId, cancellationToken);

        return command.ExecutorCompany is not null;
    }

    private async Task<bool> CustomerCompanyMustBeInDatabase(
        UpdateProjectCommand command,
        int customerCompanyId, 
        CancellationToken cancellationToken)
    {
        command.CustomerCompany = await _companyRepository
            .GetOrDeafaultAsync(c => c.Id == customerCompanyId, cancellationToken);

        return command.CustomerCompany is not null;
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
