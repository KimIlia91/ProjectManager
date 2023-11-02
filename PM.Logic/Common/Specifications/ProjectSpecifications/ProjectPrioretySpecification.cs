﻿using PM.Application.Common.Specifications.ISpecifications;
using PM.Domain.Common.Enums;
using PM.Domain.Entities;
using System.Linq.Expressions;

namespace PM.Application.Common.Specifications.ProjectSpecifications;

internal class ProjectPrioretySpecification : ISpecification<Project>
{
    private readonly Priority? _priority;

    public ProjectPrioretySpecification(
        Priority? priority)
    {
        _priority = priority;
    }

    public Expression<Func<Project, bool>> ToExpression()
    {
        return p => (!_priority.HasValue || p.Priority == _priority);
    }
}