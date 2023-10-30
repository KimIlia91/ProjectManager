using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.EmployeeContext.Dtos;
using PM.Domain.Entities;

namespace PM.Infrastructure.Persistence.Repositories;

public sealed class EmployeeRepository
    : BaseRepository<Employee>, IEmployeeRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public EmployeeRepository(
        ApplicationDbContext context,
        IMapper mapper) : base(context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<GetEmployeeResult?> GetEmployeeByIdAsync(
        int employeeId,
        CancellationToken cancellationToken)
    {
        return await _context.Employees
            .Where(e => e.Id == employeeId)
            .ProjectToType<GetEmployeeResult>(_mapper.Config)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
