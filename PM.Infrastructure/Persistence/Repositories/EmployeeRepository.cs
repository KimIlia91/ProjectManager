using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using PM.Domain.Entities;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.EmployeeContext.Dtos;
using PM.Application.Common.Models.Employee;

namespace PM.Infrastructure.Persistence.Repositories;

public sealed class EmployeeRepository
    : BaseRepository<User>, IUserRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(
        ApplicationDbContext context,
        IMapper mapper) : base(context, mapper)
    {
        _context = context;
    }

    public async Task<GetUserResult?> GetEmployeeByIdAsync(
        int employeeId,
        CancellationToken cancellationToken)
    {
        return await _context.Employees
            .Where(e => e.Id == employeeId)
            .ProjectToType<GetUserResult>(Mapper.Config)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<GetUserResult>> GetEmployeesAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Employees
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
}
