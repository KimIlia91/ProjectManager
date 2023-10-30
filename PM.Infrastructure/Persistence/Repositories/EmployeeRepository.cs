using PM.Application.Common.Interfaces.IRepositories;
using PM.Domain.Entities;

namespace PM.Infrastructure.Persistence.Repositories;

public sealed class EmployeeRepository
    : BaseRepository<Employee>, IEmployeeRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}
