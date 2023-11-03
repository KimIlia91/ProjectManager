﻿using PM.Application.Common.Models.Employee;
using PM.Application.Common.Models.Project;
using PM.Domain.Common.Enums;
using Status = PM.Domain.Common.Enums.Status;

namespace PM.Application.Features.TaskContext.Dtos;

public class GetTaskResult
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public UserResult Author { get; set; } = new UserResult();

    public UserResult Executor { get; set; } = new UserResult();

    public ProjectResult Project { get; set; } = new ProjectResult();

    public string Comment { get; set; } = string.Empty;

    public Status Status { get; set; }

    public Priority Priority { get; set; }
}
