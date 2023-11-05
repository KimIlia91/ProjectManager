using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using PM.Domain.Entities;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.EmployeeContext.Dtos;
using PM.Application.Common.Models.Employee;

namespace PM.Infrastructure.Persistence.Repositories;

/// <summary>
/// User repository for managing user entities.
/// </summary>
public sealed class UserRepository
    : BaseRepository<User>, IUserRepository
{
    /// <summary>
    /// Constructs a new instance of the UserRepository class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="mapper">The AutoMapper instance for mapping objects.</param>
    public UserRepository(
        ApplicationDbContext context,
        IMapper mapper) : base(context, mapper)
    {
    }

    /// <inheritdoc />
    public async Task<GetUserResult?> GetUserResultByIdAsync(
        int employeeId,
        CancellationToken cancellationToken)
    {
        return await DbSet
            .Where(e => e.Id == employeeId)
            .ProjectToType<GetUserResult>(Mapper.Config)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<List<GetUserResult>> GetUserResultListAsync(
        CancellationToken cancellationToken)
    {
        return await DbSet
            .ProjectToType<GetUserResult>(Mapper.Config)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<List<UserResult>> GetUserResultListByRoleAsync(
        string roleName,
        CancellationToken cancellationToken)
    {
        return await DbSet
            .Where(u => u.UserRoles.Any(ur => ur.Role.Name == roleName))
            .ProjectToType<UserResult>(Mapper.Config)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<List<UserResult>> GetUsersResultListByProjectIdAsync(
        int projectId, 
        CancellationToken cancellationToken)
    {
        return await DbSet
            .Where(u => u.Projects.Any(p => p.Id == projectId))
            .ProjectToType<UserResult>(Mapper.Config)
            .ToListAsync(cancellationToken);
    }
}
