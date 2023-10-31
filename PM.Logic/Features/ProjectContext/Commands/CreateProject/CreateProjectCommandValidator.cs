using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.ProjectContext.Commands.UpdateProject;
using PM.Domain.Common.Constants;

namespace PM.Application.Features.ProjectContext.Commands.CreateProject;

public sealed class CreateProjectCommandValidator 
    : AbstractValidator<CreateProjectCommand>
{
    private readonly IProjectRepository _projectRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public CreateProjectCommandValidator(
        IProjectRepository projectRepository,
        ICompanyRepository companyRepository,
        IEmployeeRepository employeeRepository)
    {
        _projectRepository = projectRepository;
        _companyRepository = companyRepository;
        _employeeRepository = employeeRepository;

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

    private async Task<bool> ExecutorCompanyMustBeInDatabase(
       CreateProjectCommand command,
       int executorCompanyId,
       CancellationToken cancellationToken)
    {
        command.ExecutorCompany = await _companyRepository
            .GetOrDeafaultAsync(c => c.Id == command.ExecutorCompanyId, cancellationToken);

        return command.ExecutorCompany is not null;
    }

    private async Task<bool> CustomerCompanyMustBeInDatabase(
        CreateProjectCommand command,
        int customerCompanyId,
        CancellationToken cancellationToken)
    {
        command.CustomerCompany = await _companyRepository
            .GetOrDeafaultAsync(c => c.Id == customerCompanyId, cancellationToken);

        return command.CustomerCompany is not null;
    }

    private async Task<bool> ProjectNameMustBeUnique(
       CreateProjectCommand command,
       string name,
       CancellationToken cancellationToken)
    {
        var project = await _projectRepository
            .GetOrDeafaultAsync(c => c.Name == name, cancellationToken);

        return project is null;
    }

    private async Task<bool> ManagerMustBeInDatabase(
        CreateProjectCommand command,
        int managerId,
        CancellationToken cancellationToken)
    {
        command.Manager = await _employeeRepository
            .GetOrDeafaultAsync(e => e.Id == managerId, cancellationToken);

        return command.Manager is not null;
    }
}
