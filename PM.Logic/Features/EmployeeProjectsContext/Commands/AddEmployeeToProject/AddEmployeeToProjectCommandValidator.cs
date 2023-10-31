﻿using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;

namespace PM.Application.Features.EmployeeProjectsContext.Commands.AddEmployeeToProject;

internal sealed class AddEmployeeToProjectCommandValidator
    : AbstractValidator<AddEmployeeToProjectCommand>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public AddEmployeeToProjectCommandValidator(
        IProjectRepository projectRepository,
        IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
        _projectRepository = projectRepository;

        RuleFor(command => command.EmployeeId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .MustAsync(EmployeeMustBeInDatabase);

        RuleFor(command => command.ProjectId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .MustAsync(ProjectMustBeInDatabase);
    }

    private async Task<bool> ProjectMustBeInDatabase(
        AddEmployeeToProjectCommand command,
        int id,
        CancellationToken cancellationToken)
    {
        command.Project = await _projectRepository
            .GetOrDeafaultAsync(e => e.Id == id, cancellationToken);

        return command.Project is not null;
    }

    private async Task<bool> EmployeeMustBeInDatabase(
        AddEmployeeToProjectCommand command,
        int id,
        CancellationToken cancellationToken)
    {
        command.Employee = await _employeeRepository
            .GetOrDeafaultAsync(e => e.Id == id, cancellationToken);

        return command.Employee is not null;
    }
}