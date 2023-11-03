using FluentValidation;
using PM.Application.Common.Enums;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Resources;
using PM.Domain.Common.Extensions;

namespace PM.Application.Features.EmployeeProjectsContext.Commands.AddEmployeeToProject;

public sealed class AddEmployeeToProjectCommandValidator
    : AbstractValidator<AddEmployeeToProjectCommand>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;

    public AddEmployeeToProjectCommandValidator(
        IProjectRepository projectRepository,
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _projectRepository = projectRepository;

        RuleFor(command => command.EmployeeId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MustAsync(EmployeeMustBeInDatabase)
            .WithMessage(ErrorsResource.NotFound);

        RuleFor(command => command.ProjectId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MustAsync(ProjectMustBeInDatabase)
            .WithMessage(ErrorsResource.NotFound);
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
        command.Employee = await _userRepository
            .GetOrDeafaultAsync(e => e.Id == id &&
                e.UserRoles
                    .Any(er => er.Role.Name == RoleEnum.Employee.GetDescription()), 
                cancellationToken);

        return command.Employee is not null;
    }
}
