using AppTask = PM.Domain.Entities.Task;

namespace PM.Application.Common.Interfaces.IRepositories;

public interface ITaskRepository : IBaseRepository<AppTask>
{
}
