﻿using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Specifications.ISpecifications;
using System.Linq.Expressions;
using Task = PM.Domain.Entities.Task;

namespace PM.Application.Common.Specifications.TaskSpecifications;

internal class GetTaskByManagerSpec : ISpecification<Task>
{
    private readonly int _taskId;
    private readonly int _managerId;
    private readonly ICurrentUserService _currentUserService;

    public GetTaskByManagerSpec(
        int taskId,
        ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
        _managerId = _currentUserService.UserId;
        _taskId = taskId;
    }

    public Expression<Func<Task, bool>> ToExpression()
    {
        if (_currentUserService.IsSupervisor)
            return t => t.Id == _taskId;

        return t => t.Id == _taskId && t.Project.Manager != null && t.Project.Manager.Id == _managerId;
    }
}