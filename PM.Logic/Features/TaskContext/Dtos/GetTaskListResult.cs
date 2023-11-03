using PM.Application.Common.Models.Employee;
using PM.Domain.Common.Enums;

namespace PM.Application.Features.TaskContext.Dtos;

public class GetTaskListResult
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public UserResult Author { get; set; } = new UserResult();

    public UserResult Executor { get; set; } = new UserResult();

    public string Comment { get; set; } = string.Empty;

    public Status Status { get; set; }

    public Priority Priority { get; set; }
}