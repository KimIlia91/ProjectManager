﻿using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Resources;
using PM.Domain.Common.Constants;

namespace PM.Application.Features.ProjectContext.Commands.CreateProject;

/// <summary>
/// Validator for the CreateProjectCommand, responsible for validating project creation requests.
/// </summary>
public sealed class CreateProjectCommandValidator
    : AbstractValidator<CreateProjectCommand>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the CreateProjectCommandValidator.
    /// </summary>
    /// <param name="employeeRepository">The repository for employee-related operations.</param>
    public CreateProjectCommandValidator(
        IUserRepository employeeRepository)
    {
        _userRepository = employeeRepository;

        RuleFor(command => command.Name)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MaximumLength(EntityConstants.ProjectName)
            .WithMessage(string.Format(ErrorsResource.MaxLength, EntityConstants.ProjectName));

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
            .WithMessage(ErrorsResource.Required)
            .Must((command, endDate) => endDate >= command.StartDate)
            .WithMessage(ErrorsResource.InvalidDate);

        RuleFor(command => command.Priority)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .IsInEnum()
            .WithMessage(ErrorsResource.InvalidPriority);
    }

    private async Task<bool> ManagerMustBeInDatabase(
        CreateProjectCommand command,
        int managerId,
        CancellationToken cancellationToken)
    {
        command.Manager = await _userRepository
            .GetOrDeafaultAsync(u => u.Id == managerId, cancellationToken);

        return command.Manager is not null;
    }
}
