using Microsoft.EntityFrameworkCore;
using PM.Application.Common.Interfaces.ISercices;
using PM.Infrastructure.Persistence;
using Task = PM.Domain.Entities.Task;

namespace PM.Infrastructure.Services;

//TODO: Надо внедрить для бизнесовой логики авторизации
/// <summary>
/// A service for checking access to tasks.
/// </summary>
public class TaskAccessService : AccessService<Task>, ITaskAccessService
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="TaskAccessService"/> class.
    /// </summary>
    /// <param name="context">The application database context.</param>
    public TaskAccessService(
        ApplicationDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<bool> IsAccess(
        int userId, 
        Task entity, 
        CancellationToken cancellationToken)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == entity.Id &&
                                  (t.ExecutorId == userId ||
                                  t.AuthorId == userId ||
                                  t.Project.ManagerId == userId), cancellationToken);

        Access = task is not null;
        return Access;
    }
}
