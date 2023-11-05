using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Resources;
using PM.Application.Common.Specifications.ProjectSpecifications;
using PM.Application.Features.TaskContext.Commands.CreateTask.UserSpec;
using PM.Domain.Entities;
using System.Threading;

namespace PM.Application.Features.EmployeeProjectsContext.Commands.AddEmployeeToProject;

/// <summary>
/// Validator for the command to add an employee to a project.
/// </summary>
public sealed class AddEmployeeToProjectCommandValidator
    : AbstractValidator<AddEmployeeToProjectCommand>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddEmployeeToProjectCommandValidator"/> class.
    /// </summary>
    /// <param name="projectRepository">The project repository used for database operations.</param>
    /// <param name="userRepository">The user repository used for database operations.</param>
    public AddEmployeeToProjectCommandValidator(
        IProjectRepository projectRepository,
        IUserRepository userRepository,
        ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
        _userRepository = userRepository;
        _projectRepository = projectRepository;

        RuleFor(command => command.EmployeeId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MustAsync(UserMustBeInDatebase)
            .WithMessage(ErrorsResource.NotFound)
            .MustAsync(UserMustNotBeInProject)
            .WithMessage(ErrorsResource.UserInProject);

        RuleFor(command => command.ProjectId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MustAsync(ManagerProjectMustBeInDatabase)
            .WithMessage(ErrorsResource.NotFound);
    }

    private async Task<bool> UserMustBeInDatebase(
        AddEmployeeToProjectCommand command,
        int userId, 
        CancellationToken cancellationToken)
    {
        command.Employee = await _userRepository
            .GetOrDeafaultAsync(u => u.Id == userId, cancellationToken);

        return command.Employee is null;
    }

    private async Task<bool> ManagerProjectMustBeInDatabase(
        AddEmployeeToProjectCommand command,
        int id,
        CancellationToken cancellationToken)
    {
        var managerProject = new GetProjectOfManagerSpec(id, _currentUserService.UserId);

        command.Project = await _projectRepository
            .GetOrDeafaultAsync(managerProject.ToExpression(), cancellationToken);

        return command.Project is not null;
    }

    private async Task<bool> UserMustNotBeInProject(
        AddEmployeeToProjectCommand command,
        int userId,
        CancellationToken cancellationToken)
    {
        var userProject = new GetUserOfRpojectSpec(userId, command.ProjectId);

        var user = await _userRepository
            .GetOrDeafaultAsync(userProject.ToExpression(), cancellationToken);

        return user is null;
    }
}
