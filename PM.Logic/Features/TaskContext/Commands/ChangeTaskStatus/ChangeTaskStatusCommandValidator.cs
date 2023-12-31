﻿using FluentValidation;
using PM.Application.Common.Resources;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Specifications.TaskSpecifications.User;

namespace PM.Application.Features.TaskContext.Commands.ChangeTaskStatus;

/// <summary>
/// Validator for the ChangeTaskStatusCommand class.
/// </summary>
public sealed class ChangeTaskStatusCommandValidator
    : AbstractValidator<ChangeTaskStatusCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ICurrentUserService _currentUser;

    /// <summary>
    /// Initializes a new instance of the ChangeTaskStatusCommandValidator class.
    /// </summary>
    /// <param name="taskRepository">The task repository used for data access.</param>
    public ChangeTaskStatusCommandValidator(
        ITaskRepository taskRepository,
        ICurrentUserService currentUser)
    {
        _currentUser = currentUser;
        _taskRepository = taskRepository;

        RuleFor(command => command.TaskId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MustAsync(TaskMustBeOfUser)
            .WithMessage(ErrorsResource.NotFound);

        RuleFor(command => command.Status)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .IsInEnum()
            .WithMessage(ErrorsResource.InvalidTaskStatus);
    }

    private async Task<bool> TaskMustBeOfUser(
        ChangeTaskStatusCommand command,
        int taskId,
        CancellationToken cancellationToken)
    {
        var taskByUserSpec = new TaskByUserSpec(taskId, _currentUser);

        command.Task = await _taskRepository
            .GetOrDeafaultAsync(taskByUserSpec.ToExpression(), cancellationToken);

        return command.Task is not null;
    }
}
