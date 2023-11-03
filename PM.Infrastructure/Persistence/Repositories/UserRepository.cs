using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using PM.Domain.Entities;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.EmployeeContext.Dtos;
using PM.Application.Common.Models.Employee;

namespace PM.Infrastructure.Persistence.Repositories;

public sealed class UserRepository
    : BaseRepository<User>, IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(
        ApplicationDbContext context,
        IMapper mapper) : base(context, mapper)
    {
        _context = context;
    }

    public async Task<GetUserResult?> GetUserByIdAsync(
        int employeeId,
        CancellationToken cancellationToken)
    {
        return await DbSet
            .Where(e => e.Id == employeeId)
            .ProjectToType<GetUserResult>(Mapper.Config)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<GetUserResult>> GetUsersAsync(
        CancellationToken cancellationToken)
    {
        return await DbSet
            .ProjectToType<GetUserResult>(Mapper.Config)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<UserResult>> GetUserResultListByRoleAsync(
        string roleName,
        CancellationToken cancellationToken)
    {
        return await DbSet
            .Where(u => u.EmployeeRoles.Any(ur => ur.Role.Name == roleName))
            .ProjectToType<UserResult>(Mapper.Config)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<UserResult>> GetProjectUserResultListAsync(
        int projectId, 
        CancellationToken cancellationToken)
    {
        return await DbSet
            .Where(p => p.Projects.Any(e => e.Id == projectId))
            .ProjectToType<UserResult>(Mapper.Config)
            .ToListAsync(cancellationToken);
    }
}
